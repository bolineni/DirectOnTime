namespace DirectOnTime.Messages.Payment
{
    using System;
    public abstract class PaymentBase : IPayment
    {
        public Guid MessageId { get; set; }
        public string BusinessUnit { get; set; }
        public string UserName { get; set; }
        public string RequestTime { get; set; }
        public string ReceiptId { get; set; }
    }
}