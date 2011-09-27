

namespace DirectOnTime.Messages
{
    using System;
    public interface IMessage
    {
        Guid MessageId { get; set; }
        string BusinessUnit { get; set; }
        string UserName { get; set; }
        string RequestTime { get; set; } 
    }
}