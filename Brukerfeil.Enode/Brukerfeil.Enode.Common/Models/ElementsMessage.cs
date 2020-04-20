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
        public DateTime LastUpdated { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public bool IsPerson { get; set; }
        public int RegistryEntryId { get; set; }
        public bool IsRecipient { get; set; }
        public object IsRead { get; set; }
        public string ConversationId { get; set; }
        public SendingMethod SendingMethod { get; set; }
        public SendingStatus SendingStatus { get; set; }
        public RegistryEntry RegistryEntry { get; set; }
    }

    public class SendingMethod
    {
        public string Description { get; set; }
    }

    public class SendingStatus
    {
        public string Description { get; set; }
    }

    public class RegistryEntry
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public int DocumentNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public String Title { get; set; }
        public String SenderRecipient { get; set; }
        public bool IsSenderRecipientRestricted { get; set; }
        public Case Case { get; set; }
    }

    public class Case
    {
        public int Id { get; set; }
        public int CountOfRegistryEntries { get; set; }
        public String PublicTitle { get; set; }
        public String CaseNumber { get; set; }
        public bool IsRestricted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CaseDate { get; set; }
    }
}