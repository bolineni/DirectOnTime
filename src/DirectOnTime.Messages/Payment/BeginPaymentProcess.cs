
namespace DirectOnTime.Messages.Payment
{
    using System;
    using MassTransit;

    [Serializable]
    public class BeginPaymentProcess : CorrelatedBy<Guid>, IPayment
    {
        public Guid CorrelationId { get; set; }
        public Guid MessageId { get; set; }
        public string BusinessUnit { get; set; }
        public string UserName { get; set; }
        public string RequestTime { get; set; }
        public string ReceiptId { get; set; }
    }
}