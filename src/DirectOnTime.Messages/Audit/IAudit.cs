namespace DirectOnTime.Messages.Audit
{
    public interface IAudit : IMessage
    {
        string ClientId { get; set; }
        string CompnayNumber { get; set; }
        string PolicyPrefix { get; set; }
        string PolicySequenceNumber { get; set; }
    }
}