
    var windowFocus = true, username, originalTitle, blinkOrder = 0,
        chatHub, chatboxtitle,
        chatboxFocus = [], newMessages = [], newMessagesWin = [], chatBoxes = [];

    $(document).ready(function () {

        chatHub = $.connection.chatHub;
    
        chatHub.client.StartChatSessionServerCallback = function (data) {
            startChatSessionCallback(data);
        };

        chatHub.client.ReceiveMessage = function (data) {

            if (data.message != '') {
                chatWith(data.userSource);
                data.message = data.message.replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\"/g, "&quot;");
                $("#chatbox_" + data.userSource + " .chatboxcontent").append('<div class="chatboxmessage"><span class="chatboxmessagefrom">' + data.userSource + ':&nbsp;&nbsp;</span><span class="chatboxmessagecontent">' + data.message + '</span></div>');
            }
        };

        chatHub.client.SignalOnline = function (userName) {
            $("#online-message-" + userName).show();
        };

        chatHub.client.SignalOffline = function (userName) {
            $("#online-message-" + userName).hide();
        };
    
        $('[data-action="chat"]').on("click", function () {
            chatWith($(this).data("username"));
        });
       
        originalTitle = document.title;
    
        $.connection.hub.start()
            .done(function () {
                console.log('Now connected, connection ID=' + $.connection.hub.id);
                startChatSession();
            })
            .fail(function () { console.log('Could not Connect!'); });

        $([window, document]).blur(function(){
            windowFocus = false;
        }).focus(function(){
            windowFocus = true;
            document.title = originalTitle;
        });
    });

    function verifyConnectedUsers() {
        $('[data-action="chat"]').each(function () {
            chatHub.server.verifyPartnersOnline($(this).data("username"));
        });
    }

    function restructureChatBoxes() {
        align = 0;
        for (x in chatBoxes) {
            chatboxtitle = chatBoxes[x];

            if ($("#chatbox_"+chatboxtitle).css('display') != 'none') {
                if (align == 0) {
                    $("#chatbox_"+chatboxtitle).css('right', '20px');
                } else {
                    width = (align)*(225+7)+20;
                    $("#chatbox_"+chatboxtitle).css('right', width+'px');
                }
                align++;
            }
        }
    }

    function chatWith(user) {

        var chatuser = user;
        createChatBox(chatuser);
        $("#chatbox_"+chatuser+" .chatboxtextarea").focus();
    }

    function createChatBox(chatboxtitle,minimizeChatBox) {
        if ($("#chatbox_"+chatboxtitle).length > 0) {
            if ($("#chatbox_"+chatboxtitle).css('display') == 'none') {
                $("#chatbox_"+chatboxtitle).css('display','block');
                restructureChatBoxes();
            }
            $("#chatbox_"+chatboxtitle+" .chatboxtextarea").focus();
            return;
        }

        $("<div />").attr("id","chatbox_"+chatboxtitle)
            .addClass("chatbox")
            .html('<div class="chatboxhead"><div class="chatboxtitle">'+chatboxtitle+'</div><div class="chatboxoptions"><a href="javascript:void(0)" onclick="javascript:toggleChatBoxGrowth(\''+chatboxtitle+'\')">-</a> <a href="javascript:void(0)" onclick="javascript:closeChatBox(\''+chatboxtitle+'\')">&times;</a></div><br clear="all"/></div><div class="chatboxcontent"></div><div class="chatboxinput"><textarea class="chatboxtextarea" onkeydown="javascript:return checkChatBoxInputKey(event,this,\''+chatboxtitle+'\');"></textarea></div>')
            .appendTo($( "body" ));
			   
        $("#chatbox_"+chatboxtitle).css('bottom', '0px');
	
        chatBoxeslength = 0;

        for (x in chatBoxes) {
            if ($("#chatbox_"+chatBoxes[x]).css('display') != 'none') {
                chatBoxeslength++;
            }
        }

        if (chatBoxeslength == 0) {
            $("#chatbox_"+chatboxtitle).css('right', '20px');
        } else {
            width = (chatBoxeslength)*(225+7)+20;
            $("#chatbox_"+chatboxtitle).css('right', width+'px');
        }
	
        chatBoxes.push(chatboxtitle);

        if (minimizeChatBox == 1) {
            minimizedChatBoxes = new Array();

            if ($.cookie('chatbox_minimized')) {
                minimizedChatBoxes = $.cookie('chatbox_minimized').split(/\|/);
            }
            minimize = 0;
            for (j=0;j<minimizedChatBoxes.length;j++) {
                if (minimizedChatBoxes[j] == chatboxtitle) {
                    minimize = 1;
                }
            }

            if (minimize == 1) {
                $('#chatbox_'+chatboxtitle+' .chatboxcontent').css('display','none');
                $('#chatbox_'+chatboxtitle+' .chatboxinput').css('display','none');
            }
        }

        chatboxFocus[chatboxtitle] = false;

        $("#chatbox_"+chatboxtitle+" .chatboxtextarea").blur(function(){
            chatboxFocus[chatboxtitle] = false;
            $("#chatbox_"+chatboxtitle+" .chatboxtextarea").removeClass('chatboxtextareaselected');
        }).focus(function(){
            chatboxFocus[chatboxtitle] = true;
            newMessages[chatboxtitle] = false;
            $('#chatbox_'+chatboxtitle+' .chatboxhead').removeClass('chatboxblink');
            $("#chatbox_"+chatboxtitle+" .chatboxtextarea").addClass('chatboxtextareaselected');
        });

        $("#chatbox_"+chatboxtitle).click(function() {
            if ($('#chatbox_'+chatboxtitle+' .chatboxcontent').css('display') != 'none') {
                $("#chatbox_"+chatboxtitle+" .chatboxtextarea").focus();
            }
        });

        $("#chatbox_"+chatboxtitle).show();
    }

    function closeChatBox(chatboxtitle) {
        $('#chatbox_'+chatboxtitle).css('display','none');
        restructureChatBoxes();

        $.post("Chat/Close", { chatbox: chatboxtitle} , function(data){	
        });

    }

    function toggleChatBoxGrowth(chatboxtitle) {
        if ($('#chatbox_'+chatboxtitle+' .chatboxcontent').css('display') == 'none') {  
		
            var minimizedChatBoxes = new Array();
		
            if ($.cookie('chatbox_minimized')) {
                minimizedChatBoxes = $.cookie('chatbox_minimized').split(/\|/);
            }

            var newCookie = '';

            for (i=0;i<minimizedChatBoxes.length;i++) {
                if (minimizedChatBoxes[i] != chatboxtitle) {
                    newCookie += chatboxtitle+'|';
                }
            }

            newCookie = newCookie.slice(0, -1);


            $.cookie('chatbox_minimized', newCookie);
            $('#chatbox_'+chatboxtitle+' .chatboxcontent').css('display','block');
            $('#chatbox_'+chatboxtitle+' .chatboxinput').css('display','block');
            $("#chatbox_"+chatboxtitle+" .chatboxcontent").scrollTop($("#chatbox_"+chatboxtitle+" .chatboxcontent")[0].scrollHeight);
        } else {
		
            var newCookie = chatboxtitle;

            if ($.cookie('chatbox_minimized')) {
                newCookie += '|'+$.cookie('chatbox_minimized');
            }


            $.cookie('chatbox_minimized',newCookie);
            $('#chatbox_'+chatboxtitle+' .chatboxcontent').css('display','none');
            $('#chatbox_'+chatboxtitle+' .chatboxinput').css('display','none');
        }
	
    }

    function checkChatBoxInputKey(event,chatboxtextarea,chatboxtitle) {
	 
        if(event.keyCode == 13 && event.shiftKey == 0)  {
            message = $(chatboxtextarea).val();
            message = message.replace(/^\s+|\s+$/g,"");

            $(chatboxtextarea).val('');
            $(chatboxtextarea).focus();
            $(chatboxtextarea).css('height','44px');
            if (message != '') {
                message = message.replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\"/g, "&quot;");
                $("#chatbox_" + chatboxtitle + " .chatboxcontent").append('<div class="chatboxmessage"><span class="chatboxmessagefrom">' + username + ':&nbsp;&nbsp;</span><span class="chatboxmessagecontent">' + message + '</span></div>');
                $("#chatbox_" + chatboxtitle + " .chatboxcontent").scrollTop($("#chatbox_" + chatboxtitle + " .chatboxcontent")[0].scrollHeight);
                chatHub.server.chatSendMessage(chatboxtitle, message);
            }
		
            return false;
        }

        var adjustedHeight = chatboxtextarea.clientHeight;
        var maxHeight = 94;

        if (maxHeight > adjustedHeight) {
            adjustedHeight = Math.max(chatboxtextarea.scrollHeight, adjustedHeight);
            if (maxHeight)
                adjustedHeight = Math.min(maxHeight, adjustedHeight);
            if (adjustedHeight > chatboxtextarea.clientHeight)
                $(chatboxtextarea).css('height',adjustedHeight+8 +'px');
        } else {
            $(chatboxtextarea).css('overflow','auto');
        }
	 
    }

    function startChatSession(){  
        chatHub.server.startChatSession();
        verifyConnectedUsers();
    }

    function startChatSessionCallback(data)
    {
        username = data.username;

        $.each(data.items, function(i,item){
            if (item)	{ // fix strange ie bug

                chatboxtitle = item.f;

                if ($("#chatbox_"+chatboxtitle).length <= 0) {
                    createChatBox(chatboxtitle,1);
                }
				
                if (item.s == 1) {
                    item.f = username;
                }

                if (item.s == 2) {
                    $("#chatbox_"+chatboxtitle+" .chatboxcontent").append('<div class="chatboxmessage"><span class="chatboxinfo">'+item.m+'</span></div>');
                } else {
                    $("#chatbox_"+chatboxtitle+" .chatboxcontent").append('<div class="chatboxmessage"><span class="chatboxmessagefrom">'+item.f+':&nbsp;&nbsp;</span><span class="chatboxmessagecontent">'+item.m+'</span></div>');
                }
            }
        });
		
        for (i=0;i<chatBoxes.length;i++) {
            chatboxtitle = chatBoxes[i];
            $("#chatbox_"+chatboxtitle+" .chatboxcontent").scrollTop($("#chatbox_"+chatboxtitle+" .chatboxcontent")[0].scrollHeight);
            setTimeout('$("#chatbox_"+chatboxtitle+" .chatboxcontent").scrollTop($("#chatbox_"+chatboxtitle+" .chatboxcontent")[0].scrollHeight);', 100); // yet another strange ie bug
        }
	
    }
