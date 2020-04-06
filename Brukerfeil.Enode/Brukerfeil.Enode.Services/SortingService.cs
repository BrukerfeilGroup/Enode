using Brukerfeil.Enode.Common.Models;
using System.Linq;
using Brukerfeil.Enode.Common.Services;
using System.Collections.Generic;

namespace Brukerfeil.Enode.Services
{
    public class SortingService : ISortingService
    {
        //Sorts newest to oldest, takes a (unsorted)list and returns a list sorted by datetime on lastUpdate field.
        public IEnumerable<DifiMessage> SortMessages(IEnumerable<DifiMessage> message)
        {
                var sortedList = message.OrderByDescending(x => x.lastUpdate);

                message = sortedList.ToList();

                return message; 
        }
    }
}
