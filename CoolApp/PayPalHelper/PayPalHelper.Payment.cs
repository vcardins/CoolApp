using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using CoolApp.Models.PayPal;
using PayPal.AdaptivePayments;
using PayPal.AdaptivePayments.Model;
using RestSharp;
using System.Linq;

namespace CoolApp.PayPalHelper
{
    public partial class PayPalHelper
    {
        public PayResponse GeneratePayment(Payment payment)
        {
            var request = CreatePayment(payment);
            return PayApiOperations(request);
        }        

        private PayRequest CreatePayment(Payment payment)
        {
            // # PayRequest
            // The code for the language in which errors are returned
            var envelopeRequest = new RequestEnvelope {errorLanguage = "en_US"};

            var listReceiver = new List<Receiver>();

            // Amount to be credited to the receiver's account
            var amountPrimary = payment.Amount * 0.98M;
            var amountSecundary = payment.Amount * 0.02M;

            var receivePrimary = new Receiver(amountPrimary) {email = payment.Receiver};
            var receiveSecundary = new Receiver(amountSecundary) { email = "tfm@email.com.br" };

            // A receiver's email address
            listReceiver.Add(receivePrimary);
            listReceiver.Add(receiveSecundary);
            var listOfReceivers = new ReceiverList(listReceiver);

            // PayRequest which takes mandatory params:
            //  
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Action Type` - The action for this request. Possible values are:
            // * PAY - Use this option if you are not using the Pay request in
            // combination with ExecutePayment.
            // * CREATE - Use this option to set up the payment instructions with
            // SetPaymentOptions and then execute the payment at a later time with
            // the ExecutePayment.
            // * PAY_PRIMARY - For chained payments only, specify this value to delay
            // payments to the secondary receivers; only the payment to the primary
            // receiver is processed.
            // * `Cancel URL` - URL to redirect the sender's browser to after
            // canceling the approval for a payment; it is always required but only
            // used for payments that require approval (explicit payments)
            // * `Currency Code` - The code for the currency in which the payment is
            // made; you can specify only one currency, regardless of the number of
            // receivers
            // * `Recevier List` - List of receivers
            // * `Return URL` - URL to redirect the sender's browser to after the
            // sender has logged into PayPal and approved a payment; it is always
            // required but only used if a payment requires explicit approval
            var requestPay = new PayRequest(envelopeRequest, "PAY", payment.ReturnCancelURL, "USD", listOfReceivers, payment.ReturnURL);
            return requestPay;
        }

        // # Pay API Operations
        // Use the Pay API operations to transfer funds from a sender’s PayPal account to one or more receivers’ PayPal accounts. You can use the Pay API operation to make simple payments, chained payments, or parallel payments; these payments can be explicitly approved, preapproved, or implicitly approved. 
        private PayResponse PayApiOperations(PayRequest reqPay)
        {
            // Create the PayResponse object
            var responsePay = new PayResponse();

            try
            {
                // Create the service wrapper object to make the API call
                var service = new AdaptivePaymentsService();

                // # API call
                // Invoke the Pay method in service wrapper object
                responsePay = service.Pay(reqPay);

                if (responsePay != null)
                {
                    // Response envelope acknowledgement
                    string acknowledgement = "Pay API Operation - ";
                    acknowledgement += responsePay.responseEnvelope.ack.ToString();
                    Console.WriteLine(acknowledgement + "\n");

                    // # Success values
                    if (responsePay.responseEnvelope!= null && responsePay.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                    {
                        // The pay key, which is a token you use in other Adaptive
                        // Payment APIs (such as the Refund Method) to identify this
                        // payment. The pay key is valid for 3 hours; the payment must
                        // be approved while the pay key is valid.
                        Console.WriteLine("Pay Key : " + responsePay.payKey + "\n");
                    }
                    // # Error Values 
                    else
                    {
                        List<ErrorData> errorMessages = responsePay.error;
                        foreach (ErrorData error in errorMessages)
                        {
                            Console.WriteLine(error.message + "\n");
                        }
                    }
                }
            }
            // # Exception log    
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine("Error Message : " + ex.Message);
            }
            return responsePay;
        }

        public PayResponse GeneratePreapprovalPayment(Payment payment)
        {
            // API endpoint for the Refund call in the Sandbox
            const string sAPIEndpoint = "https://svcs.sandbox.paypal.com/AdaptivePayments/Pay";

            // Version that you are coding against
            const string sVersion = "1.1.0";

            // Error Langugage
            const string sErrorLangugage = "en_US";

            // Detail Level
            //string sDetailLevel = "ReturnAll";

            // Request Data Binding
            const string sRequestDataBinding = "NV";

            // Response Data Binding
            const string sResponseDataBinding = "NV";

            // Application ID
            const string sAppId = "APP-80W284485P519543T";

            //PayPal credentials
            const string userId = "daniel.smoreira-facilitator_api1.outlook.com";
            const string pass = "1373049699";
            const string signature = "AQU0e5vuZCvSg-XJploSa.sGUDlpAT7Ecw66HFdR0xOK6mL90N8vFFp3";


            const string sCurrencyCode = "USD";                                         // Currency Code
            const string sActionType = "PAY";                                           // Action Type
            string sReturnURL = payment.ReturnURL;                                      // ReturnURL and CancelURL used for approval flow
            string sCancelURL = payment.ReturnCancelURL;                                // ReturnURL and CancelURL used for approval flow
            const string sFeesPayer = "EACHRECEIVER";                                   // who pays the fees
            string sMemo = payment.Memo;                                                // memo field
            string receiverAmount = ((1 - payment.Fee / 100.0M) * payment.Amount).ToString(CultureInfo.InvariantCulture);       // transaction amount
            string feeReceiverAmount = ((payment.Fee / 100.0M) * payment.Amount).ToString(CultureInfo.InvariantCulture);        // transaction amount
            string receiver = payment.Receiver;                                         //transaction receive
            string feeReceiver = payment.FeeReceiver;                                   //transaction receive
            string sPreapprovalKey = payment.PreapprovalKey;                            //preapprovalKey
            string sender = payment.Sender;                                             //transaction sender

            var client = new RestClient(sAPIEndpoint);

            var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};

            request.AddHeader("X-PAYPAL-SECURITY-USERID", userId);
            request.AddHeader("X-PAYPAL-SECURITY-PASSWORD", pass);
            request.AddHeader("X-PAYPAL-SECURITY-SIGNATURE", signature);
            request.AddHeader("X-PAYPAL-DEVICE-IPADDRESS", sVersion);
            request.AddHeader("X-PAYPAL-APPLICATION-ID", sAppId);
            request.AddHeader("X-PAYPAL-REQUEST-DATA-FORMAT", sRequestDataBinding);
            request.AddHeader("X-PAYPAL-RESPONSE-DATA-FORMAT", sResponseDataBinding);

            request.AddParameter("actionType", sActionType);
            request.AddParameter("currencyCode", sCurrencyCode);
            request.AddParameter("feesPayer", sFeesPayer);
            request.AddParameter("memo", sMemo);
            request.AddParameter("preapprovalKey", sPreapprovalKey);
            request.AddParameter("receiverList.receiver(0).amount", receiverAmount);
            request.AddParameter("receiverList.receiver(0).email", receiver);
            request.AddParameter("receiverList.receiver(1).amount", feeReceiverAmount);
            request.AddParameter("receiverList.receiver(1).email", feeReceiver);
            request.AddParameter("senderEmail", sender);
            request.AddParameter("returnUrl", sReturnURL);
            request.AddParameter("cancelUrl", sCancelURL);
            request.AddParameter("requestEnvelope.errorLanguage", sErrorLangugage);

            var result = client.Execute(request);

            if (result.ErrorException != null)
            {
                var twilioException = new ApplicationException(result.ErrorMessage, result.ErrorException);
                throw twilioException;
            }

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(result.Content);

            var paymentReturnKeys = new List<string>
                {
                    "responseEnvelope.timestamp",
                    "responseEnvelope.ack",
                    "responseEnvelope.correlationId",
                    "responseEnvelope.build",
                    "payKey",
                    "paymentExecStatus"
                };

            var listAckCodes = new List<AckCode>
                {
                    AckCode.FAILUREWITHWARNING,
                    AckCode.WARNING,
                    AckCode.FAILURE,
                    AckCode.SUCCESS,
                    AckCode.SUCCESSWITHWARNING
                };

            var payResponse = new PayResponse
                {
                    responseEnvelope = new ResponseEnvelope
                        {
                            timestamp = queryString[paymentReturnKeys[0]],
                            ack = listAckCodes.FirstOrDefault(x => x.ToString() == queryString[paymentReturnKeys[1]]),
                            correlationId = queryString[paymentReturnKeys[2]],
                            build = queryString[paymentReturnKeys[3]]
                        },
                    payKey = queryString[paymentReturnKeys[4]],
                    paymentExecStatus = queryString[paymentReturnKeys[5]]
                };

            return payResponse;
        }
    }
}