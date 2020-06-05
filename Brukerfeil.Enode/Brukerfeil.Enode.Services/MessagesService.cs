using Brukerfeil.Enode.Common.Models;
using System.Linq;
using Brukerfeil.Enode.Common.Services;
using System.Collections.Generic;

namespace Brukerfeil.Enode.Services
{
    public class MessagesService : IMessagesService
    {
        public IEnumerable<DifiMessage> AddLatestStatusOnList(IEnumerable<DifiMessage> messages)
        {
            foreach (var message in messages)
            {
                message.latestMessageStatus = message.messageStatuses.Last().status.ToString();
                message.created = message.messageStatuses.First().lastUpdate;
                message.messageStatuses.ToList().Reverse();
            }
            return messages;
        }


        public DifiMessage AddLatestStatusOnSingle(DifiMessage message)
        {
            message.latestMessageStatus = message.messageStatuses.Last().status.ToString();
            message.created = message.messageStatuses.First().lastUpdate;
            message.messageStatuses.ToList().Reverse();
            return message;
        }

    }
}

