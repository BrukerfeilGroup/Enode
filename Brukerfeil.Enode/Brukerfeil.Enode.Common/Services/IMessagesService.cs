using System.Collections.Generic;
using Brukerfeil.Enode.Common.Models;

namespace Brukerfeil.Enode.Common.Services
{
    public interface IMessagesService
    {
        public IEnumerable<DifiMessage> AddLatestStatus(IEnumerable<DifiMessage> message);
    }
}