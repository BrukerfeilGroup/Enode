using System.Collections.Generic;
using Brukerfeil.Enode.Common.Models;

namespace Brukerfeil.Enode.Common.Services
{
    public interface IMessagesService
    {
        public IEnumerable<DifiMessage> AddLatestStatusOnList(IEnumerable<DifiMessage> messages);
        public DifiMessage AddLatestStatusOnSingle(DifiMessage message);
    }
}