namespace CoolApp.Models.PayPal
{
    public class Payment
    {
        public string Receiver { get; set; }
        public decimal Amount { get; set; }
        public string PayKey { get; set; }
        public string ReturnURL { get; set; }
        public string ReturnCancelURL { get; set; }
    }
}
