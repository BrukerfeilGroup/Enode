using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Brukerfeil.Enode.Common.Models;

namespace Brukerfeil.Enode.Common.Services
{
    public interface IMessageMergeService
    {
        public Message MergeMessages(DifiMessage difiMessages, ElementsMessage elementsMessages);
        public Task<IEnumerable<Message>> MergeMessagesListsInAsync(string organizationId, IEnumerable<DifiMessage> difiMessages, IEnumerable<ElementsMessage> elementsMessages);
        public Task<IEnumerable<Message>> MergeMessagesListsOutAsync(string organizationId, IEnumerable<ElementsMessage> elementsMessages, IEnumerable<DifiMessage> difiMessages);

    }
}
