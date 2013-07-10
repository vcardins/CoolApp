using System;
using System.Linq;
using System.Web.Mvc;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Infraestructure.Profiles;
using CoolApp.Models.PayPal;

namespace CoolApp.Controllers
{
    public class PayPalController : Controller
    {
        private IPreApprovalService _preApprovalService;

        public ActionResult Index()
        {
            var payment = new Payment
                {
                    Receiver = "bush@email.com.br",
                    Amount = 20.00M,
                    Fee = 2,
                    FeeReceiver = "tfm@email.com.br",
                    PreapprovalKey = "PA-7RR891314Y878192Y",
                    ReturnCancelURL = "http://localhost:26927/PayPal",
                    ReturnURL = "http://localhost:26927/PayPal/Success"
                };
            return View(payment);
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Pay(Payment payment)
        {
            var payPal = new PayPalHelper.PayPalHelper();
            var responsePay = payPal.GeneratePayment(payment);
            if (responsePay != null)
            {
                // # Success values
                if (responsePay.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    payment.PayKey = responsePay.payKey;
                    return View(payment);
                }
                // # Error Values 
                //List<ErrorData> errorMessages = responsePay.error;
                return View("Error");
            }
            return View("Error");
        }

        public ActionResult ConfigurePreappoval(Payment payment)
        {
            var payPal = new PayPalHelper.PayPalHelper();
            var preapproval = new Preapproval { CancelURL = payment.ReturnCancelURL, ReturnURL = payment.ReturnURL, IPNHost = "http://localhost:26927/PayPal/IPN" };
            var responsePreapproval = payPal.GeneratePreapproval(preapproval);
            if (responsePreapproval != null)
            {
                // # Success values
                if (responsePreapproval.responseEnvelope != null && responsePreapproval.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    preapproval.PreapprovalKey = responsePreapproval.preapprovalKey;
                    return View(preapproval);                  
                }
                // # Error Values
                //List<ErrorData> errorMessages = responsePreapproval.error;
                return View("Error");
            }
            return View("Error");
        }

        public ActionResult IPN()
        {
            return null;
        }

        public ActionResult PayPreapproved(Payment payment)
        {
            var userId = UserProfile.Current.UserId;
            _preApprovalService = DependencyResolver.Current.GetService<IPreApprovalService>();

            var userPreApproval = _preApprovalService.Find(x => x.Userd == userId && x.ContractEndDate >= DateTime.Now && x.ContractStartDate <= DateTime.Now).FirstOrDefault();

            var payPal = new PayPalHelper.PayPalHelper();

            if (userPreApproval != null)
            {
                var userActualPreApproval = payPal.PreapprovalDetailsAPIOperation(userPreApproval.PreApprovalKey);
                userPreApproval.Approved = userActualPreApproval.approved != null && userActualPreApproval.approved.Value;
                userPreApproval.ContractStartDate = DateTime.Parse(userActualPreApproval.startingDate);
                userPreApproval.ContractEndDate = DateTime.Parse(userActualPreApproval.endingDate);
                _preApprovalService.SaveOrUpdate(userPreApproval);
                
                if(!userPreApproval.Approved)
                {
                    return View("Error");
                }
            }
            else
            {
                return View("Error");   
            }

            payment.PreapprovalKey = userPreApproval.PreApprovalKey;

            var responsePay = payPal.GeneratePreapprovalPayment(payment);
            if (responsePay != null)
            {
                // # Success values
                if (responsePay.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    payment.PayKey = responsePay.payKey;
                    return View("Success");
                }
                // # Error Values 
                //List<ErrorData> errorMessages = responsePay.error;
                return View("Error");
            }
            return View("Error");
        }
    }
}
