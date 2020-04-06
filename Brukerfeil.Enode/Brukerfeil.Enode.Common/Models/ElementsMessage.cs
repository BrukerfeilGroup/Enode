using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Brukerfeil.Enode.Common.Models
{
    public class ElementsMessageContent
    {
        public string odatacontext { get; set; }
        public IEnumerable<ElementsMessage> value { get; set; }
    }

    public class ElementsMessage
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRecipient { get; set; }
        public object IsRead { get; set; }
        public string ConversationId { get; set; }
        public SendingMethod SendingMethod { get; set; }
        public SendingStatus SendingStatus { get; set; }
    }

    public class SendingMethod
    {
        public string Description { get; set; }
    }

    public class SendingStatus
    {
        public string Description { get; set; }
    }
}