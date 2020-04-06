using System;
using System.Collections.Generic;

namespace Brukerfeil.Enode.Common.Models
{
    public class MessageStatusContent
    {
        public IEnumerable<MessageStatus> content { get; set; }
        
    }

    public class MessageStatus
    {
        public int id { get; set; }
        public DateTime lastUpdate { get; set; }
        public string status { get; set; }
        public string conversationId { get; set; }
        public string messageId { get; set; }
        public int convId { get; set; }

    }
}