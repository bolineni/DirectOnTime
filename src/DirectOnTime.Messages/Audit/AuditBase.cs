

namespace DirectOnTime.Messages.Audit
{
    using System;
    public abstract class AuditBase : IAudit
    {
        public Guid MessageId { get; set; }
        public string BusinessUnit { get; set; }
        public string UserName { get; set; }
        public string RequestTime { get; set; } 
        public string ClientId { get; set; }
        public string CompnayNumber { get; set; }
        public string PolicyPrefix { get; set; }
        public string PolicySequenceNumber { get; set; }
    }
}