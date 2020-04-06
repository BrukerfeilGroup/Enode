using System.Collections.Generic;
using Brukerfeil.Enode.Common.Models;

namespace Brukerfeil.Enode.Common.Services
{
    public interface ISortingService
    {
        public IEnumerable<DifiMessage> SortMessages(IEnumerable<DifiMessage> message);
    }
}
