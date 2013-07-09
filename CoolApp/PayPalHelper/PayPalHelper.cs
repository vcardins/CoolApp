using System;
using System.Collections.Generic;
using CoolApp.Models.PayPal;
using PayPal.AdaptivePayments;
using PayPal.AdaptivePayments.Model;

namespace CoolApp.PayPalHelper
{
    public class PayPalHelper
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
            var envelopeRequest = new PayPal.AdaptivePayments.Model.RequestEnvelope {errorLanguage = "en_US"};

            var listReceiver = new List<PayPal.AdaptivePayments.Model.Receiver>();

            // Amount to be credited to the receiver's account
            var amountPrimary = payment.Amount * 0.98M;
            var amountSecundary = payment.Amount * 0.02M;

            var receivePrimary = new PayPal.AdaptivePayments.Model.Receiver(amountPrimary) {email = payment.Receiver};
            var receiveSecundary = new PayPal.AdaptivePayments.Model.Receiver(amountSecundary) { email = "tfm@email.com.br" };

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
            var requestPay = new PayPal.AdaptivePayments.Model.PayRequest(envelopeRequest, "PAY", payment.ReturnCancelURL, "USD", listOfReceivers, payment.ReturnURL);
            return requestPay;
        }

        // # Pay API Operations
        // Use the Pay API operations to transfer funds from a sender’s PayPal account to one or more receivers’ PayPal accounts. You can use the Pay API operation to make simple payments, chained payments, or parallel payments; these payments can be explicitly approved, preapproved, or implicitly approved. 
        private PayPal.AdaptivePayments.Model.PayResponse PayApiOperations(PayPal.AdaptivePayments.Model.PayRequest reqPay)
        {
            // Create the PayResponse object
            var responsePay = new PayPal.AdaptivePayments.Model.PayResponse();

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
                    if (responsePay.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
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
            catch (System.Exception ex)
            {
                // Log the exception message
                Console.WriteLine("Error Message : " + ex.Message);
            }
            return responsePay;
        }
    }
}