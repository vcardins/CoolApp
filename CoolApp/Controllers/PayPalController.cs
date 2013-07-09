using System.Web.Mvc;
using CoolApp.Models.PayPal;

namespace CoolApp.Controllers
{
    public class PayPalController : Controller
    {
        public ActionResult Index()
        {
            return View(new Payment { Receiver = "bush@email.com.br", Amount = 20.00M, ReturnCancelURL = "http://localhost:26927/PayPal", ReturnURL = "http://localhost:26927/PayPal/Success" });
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

    }
}
