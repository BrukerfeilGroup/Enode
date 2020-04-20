using System.Collections.Generic;
using System.Threading.Tasks;
using Brukerfeil.Enode.Common.Enums;
using Brukerfeil.Enode.Common.Models;

namespace Brukerfeil.Enode.Common.Repositories
{
    public interface IElementsMessageRepository
    {
        Task<IEnumerable<ElementsMessage>> GetElementsMessagesAsync(string organizationId, Direction direction);
        Task<ElementsMessage> GetElementsMessageAsync(string organizationId, string msgId);
        Task<IEnumerable<ElementsMessage>> GetElementsMessagesBySenderIdAsync(string orgId, string senderId);
        Task<IEnumerable<ElementsMessage>> GetElementsMessagesByReceiverIdAsync(string orgId, string receiverId);
    }
}
