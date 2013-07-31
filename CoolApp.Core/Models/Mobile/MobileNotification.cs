using System.ComponentModel.DataAnnotations;

namespace CoolApp.Core.Models.Mobile
{
    /// <summary>
    /// Object for send push notifications to one or more users who are subscribed to a channel.
    /// </summary>
    public class MobileNotification
    {
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// Name of the channel.
        /// </value>
        [Required] 
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the friends.
        /// </summary>
        /// <value>
        /// If this parameter is specified (regardless of the parameter's value), 
        /// push notifications are sent to any of the user's Friends who are subscribed to the identified channel.
        /// </value>
        public int Friends { get; set; }

        /// <summary>
        /// Gets or sets the user ids.
        /// </summary>
        /// <value>
        /// Comma separated list of user IDs. Sends push notification to the specified users 
        /// who are subscribed to the specified channel.
        /// </value>
        public string UserIds { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// (Android Only) Title of the notification item to be displayed in notification center.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the badge.
        /// </summary>
        /// <value>
        /// The number to display as the badge of the application icon.
        /// </value>
        public int Badge { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// Content of the notification item to be displayed in notification center.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the sound.
        /// </summary>
        /// <value>
        /// The sound's file name. In Titanium IDE The file should be located under app's "assets/sound" directory. 
        /// If the sound file is not reachable, no sound will be played.
        /// </value>
        public string Sound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MobileNotification"/> is vibrate.
        /// </summary>
        /// <value>
        ///   (Android Only) If the value is true, then the device will vibrate for 1 second.
        /// </value>
        public bool Vibrate { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// (Android Only) The icon's file name without extension. 
        /// In the Titanium IDE the file should be located under app's "res/drawable" directory. 
        /// This icon will be used to show in notification bar and be used as icon of 
        /// notification item in the notification center. If no icon is provided or the 
        /// provided one is unreachable, the app's icon will be used by default.
        /// </value>
        public string Icon { get; set; }
    }
}