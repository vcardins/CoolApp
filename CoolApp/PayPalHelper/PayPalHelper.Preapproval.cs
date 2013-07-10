using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Models;
using CoolApp.Infraestructure.Profiles;
using CoolApp.Models.PayPal;
using PayPal.AdaptivePayments;
using PayPal.AdaptivePayments.Model;

namespace CoolApp.PayPalHelper
{
    public partial class PayPalHelper
    {
        IPreApprovalService _preApprovalService;

        // # Preapproval API Operation 
        // Use the Preapproval API operation to set up an agreement between yourself and a sender for making payments on the sender’s behalf. You set up a preapprovals for a specific maximum amount over a specific period of time and, optionally, by any of the following constraints: the number of payments, a maximum per-payment amount, a specific day of the week or the month, and whether or not a PIN is required for each payment request. 
        public PreapprovalResponse GeneratePreapproval(Preapproval preapproval)
        {
            // Create the PreapprovalResponse object
            var responsePreapproval = new PreapprovalResponse();

            try
            {
                // # PreapprovalRequest
                // The code for the language in which errors are returned
                var envelopeRequest = new RequestEnvelope {errorLanguage = "en_US"};

                const string pattern = "yyyy-MM-dd";
                
                var requestPreapproval = new PreapprovalRequest(envelopeRequest, preapproval.CancelURL, "USD", preapproval.ReturnURL, DateTime.Now.ToString(pattern))
                    {ipnNotificationUrl = preapproval.IPNHost};

                // IPN URL
                //  
                // * PayPal Instant Payment Notification is a call back system that is initiated when a transaction is completed        
                // * The transaction related IPN variables will be received on the call back URL specified in the request       
                // * The IPN variables have to be sent back to the PayPal system for validation, upon validation PayPal will send a response string "VERIFIED" or "INVALID"     
                // * PayPal would continuously resend IPN if a wrong IPN is sent        

                // Create the service wrapper object to make the API call
                var service = new AdaptivePaymentsService();

                // # API call
                // Invoke the Preapproval method in service wrapper object
                responsePreapproval = service.Preapproval(requestPreapproval);

                if (responsePreapproval != null)
                {
                    // Response envelope acknowledgement
                    string acknowledgement = "Preapproval API Operation - ";
                    acknowledgement += responsePreapproval.responseEnvelope.ack.ToString();
                    Console.WriteLine(acknowledgement + "\n");

                    // # Success values
                    if (responsePreapproval.responseEnvelope != null && responsePreapproval.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                    {
                        var preApprovalDetails = PreapprovalDetailsAPIOperation(responsePreapproval.preapprovalKey);
                        _preApprovalService = DependencyResolver.Current.GetService<IPreApprovalService>();
                        _preApprovalService.SaveOrUpdate(new PreApproval
                            {
                                Approved =  preApprovalDetails.approved != null && preApprovalDetails.approved.Value,
                                ContractStartDate = DateTime.Parse(preApprovalDetails.startingDate),
                                ContractEndDate = DateTime.Parse(preApprovalDetails.endingDate),
                                PreApprovalKey = responsePreapproval.preapprovalKey,
                                Userd = UserProfile.Current.UserId
                            });                   
                    }
                    // # Error Values
                    else
                    {
                        List<ErrorData> errorMessages = responsePreapproval.error;
                        foreach (ErrorData error in errorMessages)
                        {
                            Console.WriteLine("API Error Message : " + error.message + "\n");
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
            return responsePreapproval;
        }

        // # PreapprovalDetails API Operation
        // Use the PreapprovalDetails API operation to obtain information about an agreement between you and a sender for making payments on the sender’s behalf. 
        public PreapprovalDetailsResponse PreapprovalDetailsAPIOperation(string preapprovalKey)
        {
            // Create the PreapprovalDetailsResponse object
            var responsePreapprovalDetails = new PreapprovalDetailsResponse();

            try
            {
                // # PreapprovaDetailslRequest
                // The code for the language in which errors are returned
                var envelopeRequest = new RequestEnvelope {errorLanguage = "en_US"};

                // PreapprovalDetailsRequest object which takes mandatory params:
                //
                // * `Request Envelope` - Information common to each API operation, such
                // as the language in which an error message is returned.
                // * `Preapproval Key` - A preapproval key that identifies the
                // preapproval for which you want to retrieve details. The preapproval
                // key is returned in the PreapprovalResponse message.
                var preapprovDetailsRequest = new PreapprovalDetailsRequest(
                envelopeRequest, preapprovalKey);

                // Create the service wrapper object to make the API call
                var service = new AdaptivePaymentsService();

                // # API call
                // Invoke the PreapprovalDetails method in service wrapper object
                responsePreapprovalDetails = service.PreapprovalDetails(preapprovDetailsRequest);

                if (responsePreapprovalDetails != null)
                {

                    // # Success values
                    if (responsePreapprovalDetails.responseEnvelope != null && responsePreapprovalDetails.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                    {
                        // First date for which the preapproval is valid.
                        Console.WriteLine("Starting Date : " + responsePreapprovalDetails.startingDate + "\n");
                    }
                    // # Error Values
                    else
                    {
                        List<ErrorData> errorMessages = responsePreapprovalDetails.error;
                        foreach (ErrorData error in errorMessages)
                        {
                            Console.WriteLine("API Error Message : " + error.message + "\n");
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
            return responsePreapprovalDetails;
        }
    }
}