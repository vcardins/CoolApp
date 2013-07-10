namespace CoolApp.Models.PayPal
{
    public class Preapproval
    {
        public string CancelURL { get; set; }

        public string ReturnURL { get; set; }

        public string PreapprovalKey { get; set; }

        public string IPNHost { get; set; }
    }
}