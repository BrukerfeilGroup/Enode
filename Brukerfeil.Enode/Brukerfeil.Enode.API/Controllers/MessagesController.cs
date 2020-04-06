using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Common.Repositories;
using Brukerfeil.Enode.Common.Services;
using System.Collections.Generic;
using System.Linq;
using Brukerfeil.Enode.Common.Enums;

namespace Brukerfeil.Enode.API.Controllers
{
    [ApiController]
    [Route("{organizationId}/[controller]")]
    public class MessagesController : ControllerBase
    {
        [HttpGet("in", Name = "GetIncomingMessagesAsync")]
        public async Task<ActionResult<IEnumerable<Message>>> GetIncomingMessagesAsync(string organizationId, [FromServices] IMessageMergeService messageMergeService, [FromServices] IDifiMessageRepository difiMessageRepository, [FromServices] IElementsMessageRepository elementsMessageRepository, [FromServices] IMessagesService messagesService)
        {
            try
            {
                var difiMessagesList = await difiMessageRepository.GetDifiMessagesAsync(organizationId, Direction.INCOMING);
                difiMessagesList = messagesService.AddLatestStatus(difiMessagesList);
                var elementsMessagesList = await elementsMessageRepository.GetElementsMessagesAsync(organizationId, Direction.INCOMING);
                var combinedMessagesList = messageMergeService.MergeMessagesListsIn(difiMessagesList, elementsMessagesList);
                if (combinedMessagesList.ToList().Count() == 0)
                {
                    return NotFound();
                }
                return combinedMessagesList.ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(402, ex.Message);
            }
        }

        [HttpGet("out", Name = "GetOutgoingMessagesAsync")]
        public async Task<ActionResult<IEnumerable<Message>>> GetOutgoingMessagesAsync(string organizationId, [FromServices] IMessageMergeService messageMergeService, [FromServices] IElementsMessageRepository elementsMessageRepository, [FromServices] IDifiMessageRepository difiMessageRepository, [FromServices] IMessagesService messagesService)
        {
            try
            {
                var difiMessagesList = await difiMessageRepository.GetDifiMessagesAsync(organizationId, Direction.OUTGOING);
                difiMessagesList = messagesService.AddLatestStatus(difiMessagesList);
                var elementsMessagesList = await elementsMessageRepository.GetElementsMessagesAsync(organizationId, Direction.OUTGOING);
                var combinedMessagesList = messageMergeService.MergeMessagesListsOut(elementsMessagesList, difiMessagesList);
                if (combinedMessagesList.ToList().Count() == 0)
                {
                    return NotFound();
                }
                return combinedMessagesList.ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(402, ex.Message);
            }
        }

        //Todo
        [HttpGet("sender/{senderId}", Name = "GetOrgMessagesBySenderIdAsync")]
        public async Task<ActionResult<IEnumerable<Message>>> GetOrgMessagesBySenderIdAsync(string organizationId, [FromServices] IMessageMergeService messageMergeService, [FromServices] IDifiMessageRepository difiMessageRepository, [FromServices] IElementsMessageRepository elementsMessageRepository)
        {
            try
            {
                var difiMessagesList = await difiMessageRepository.GetDifiMessagesAsync(organizationId, Direction.INCOMING);
                var elementsMessagesList = await elementsMessageRepository.GetElementsMessagesAsync(organizationId, Direction.INCOMING);
                var combinedMessagesList = messageMergeService.MergeMessagesListsIn(difiMessagesList, elementsMessagesList);
                if (combinedMessagesList.ToList().Count() == 0)
                {
                    return NotFound();
                }
                return combinedMessagesList.ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(402, ex.Message);
            }
        }

        //Todo
        [HttpGet("receiver/{receiverId}", Name = "GetOrgMessagesByReceiverIdAsync")]
        public async Task<ActionResult<IEnumerable<Message>>> GetOrgMessagesByReceiverIdAsync(string organizationId, string receiverId, [FromServices] IDifiMessageRepository difiMessageRepository, [FromServices] IMessageMergeService messageMergeService, [FromServices] IElementsMessageRepository elementsMessageRepository)
        {
            try
            {
                var difiMessagesList = await difiMessageRepository.GetDifiMessagesAsync(organizationId, Direction.OUTGOING);
                var elementsMessagesList = await elementsMessageRepository.GetElementsMessagesAsync(organizationId, Direction.OUTGOING);
                var combinedMessagesList = messageMergeService.MergeMessagesListsOut(elementsMessagesList, difiMessagesList);
                if (combinedMessagesList.ToList().Count() == 0)
                {
                    return NotFound();
                }
                return combinedMessagesList.ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(402, ex.Message);
            }
        }

        [HttpGet("{msgId}", Name = "GetMessageAsync")]
        public async Task<ActionResult<Message>> GetMessageAsync(string organizationId, string msgId, [FromServices] IMessageMergeService messageMergeService, [FromServices] IDifiMessageRepository difiMessageRepository, [FromServices] IElementsMessageRepository elementsMessageRepository)
        {
            try
            {
                var difiMessage = await difiMessageRepository.GetDifiMessageAsync(organizationId, msgId);
                var elementsMessage = await elementsMessageRepository.GetElementsMessageAsync(organizationId, msgId);
                var combinedMessage = messageMergeService.MergeMessages(difiMessage, elementsMessage);
                if (combinedMessage == null)
                {
                    return NotFound();
                }
                return combinedMessage;
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(402, ex.Message);
            }
        }
    }
}

