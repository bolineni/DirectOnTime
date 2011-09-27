namespace DirectOnTime.Messages.Payment
{
    public interface IPayment : IMessage
    {
        string ReceiptId { get; set; }
    }
}