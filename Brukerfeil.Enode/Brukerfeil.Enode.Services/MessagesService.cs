using Brukerfeil.Enode.Common.Models;
using System.Linq;
using Brukerfeil.Enode.Common.Services;
using System.Collections.Generic;
using System;

namespace Brukerfeil.Enode.Services
{
    public class MessagesService : IMessagesService
    {
        public IEnumerable<DifiMessage> AddLatestStatus(IEnumerable<DifiMessage> messages)
        {
            foreach (var message in messages)
            {
                var lastStatus = message.messageStatuses.Last().status.ToString();
                message.latestMessageStatus = lastStatus;
                DateTime firstDateTime = message.messageStatuses.First().lastUpdate;
                message.created = firstDateTime;
                message.messageStatuses.ToList().Reverse();
            }
            return messages;
        }

    }

}

