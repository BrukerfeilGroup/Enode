using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Common.Repositories;
using Brukerfeil.Enode.Common.Services;
using System.Collections.Generic;
using System.Linq;
using Brukerfeil.Enode.Common.Enums;
using System;
using Brukerfeil.Enode.Common.Exceptions;

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
                var combinedMessagesList = await messageMergeService.MergeMessagesListsInAsync(organizationId, difiMessagesList, elementsMessagesList);
                if (combinedMessagesList.ToList().Count() == 0)
                {
                    return NotFound();
                }
                return combinedMessagesList.ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(400, ex.Message);
            }
            catch (InvalidMessageQueryParameterException ex)
            {
                //Fix statuscode
                return this.StatusCode(400, ex.Message);
            }
            catch (Exception)
            {
                var difiMessagesList = await difiMessageRepository.GetDifiMessagesAsync(organizationId, Direction.INCOMING);
                var combinedMessagesList = await messageMergeService.MergeMessagesListsInAsync(organizationId, difiMessagesList, null);
                return combinedMessagesList.ToList();
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
                var combinedMessagesList = await messageMergeService.MergeMessagesListsOutAsync(organizationId, elementsMessagesList, difiMessagesList);
                if (combinedMessagesList.ToList().Count() == 0)
                {
                    return NotFound();
                }
                return combinedMessagesList.ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(400, ex.Message);
            }
            catch (Exception)
            {
                var elementsMessagesList = await elementsMessageRepository.GetElementsMessagesAsync(organizationId, Direction.OUTGOING);
                var combinedMessagesList = await messageMergeService.MergeMessagesListsOutAsync(organizationId, elementsMessagesList, null);
                return combinedMessagesList.ToList();
            }
        }

        [HttpGet("sender/{senderId}", Name = "GetMessagesBySenderIdAsync")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBySenderIdAsync(string organizationId, string senderId, [FromServices] IMessageMergeService messageMergeService, [FromServices] IDifiMessageRepository difiMessageRepository, [FromServices] IElementsMessageRepository elementsMessageRepository, [FromServices] IMessagesService messagesService)
        {
            try
            {
                var difiMessagesList = await difiMessageRepository.GetMessagesBySenderIdAsync(organizationId, senderId);
                difiMessagesList = messagesService.AddLatestStatus(difiMessagesList);
                var elementsMessagesList = await elementsMessageRepository.GetElementsMessagesBySenderIdAsync(organizationId, senderId);
                var combinedMessages = await messageMergeService.MergeMessagesListsInAsync(organizationId, difiMessagesList, elementsMessagesList);
                if (combinedMessages.ToList().Count() == 0)
                {
                    return NotFound();
                }
                return combinedMessages.ToList();

            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(400, ex.Message);
            }
        }

        [HttpGet("receiver/{receiverId}", Name = "GetMessagesByReceiverIdAsync")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByReceiverIdAsync(string organizationId, string receiverId, [FromServices] IDifiMessageRepository difiMessageRepository, [FromServices] IMessageMergeService messageMergeService, [FromServices] IElementsMessageRepository elementsMessageRepository, [FromServices] IMessagesService messagesService)
        {
            try
            {
                var difiMessagesList = await difiMessageRepository.GetMessagesByReceiverIdAsync(organizationId, receiverId);
                difiMessagesList = messagesService.AddLatestStatus(difiMessagesList);
                var elementsMessagesList = await elementsMessageRepository.GetElementsMessagesByReceiverIdAsync(organizationId, receiverId);
                var combinedMessagesList = await messageMergeService.MergeMessagesListsOutAsync(organizationId, elementsMessagesList, difiMessagesList);
                if (combinedMessagesList.ToList().Count() == 0)
                {
                    return NotFound();
                }
                return combinedMessagesList.ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(400, ex.Message);
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
                if (difiMessage == null && elementsMessage == null || combinedMessage == null)
                {
                    return NotFound();
                }
                return combinedMessage;
            }
            catch (System.InvalidOperationException ex)
            {
                return this.StatusCode(400, ex.Message);
            }
        }
    }
}