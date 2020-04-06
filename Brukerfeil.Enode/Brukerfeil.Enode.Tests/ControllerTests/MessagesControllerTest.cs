using Xunit;
using Moq;
using Brukerfeil.Enode.API.Controllers;
using Brukerfeil.Enode.Common.Repositories;
using Brukerfeil.Enode.Common.Models;
using System.Collections.Generic;
using Brukerfeil.Enode.Common.Services;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Brukerfeil.Enode.Common.Enums;

namespace Brukerfeil.Enode.Tests
{
    public class MessagesControllerTest
    {
        private IEnumerable<DifiMessage> GetIncomingMessageObject()
        {
            var incomingMessage = new List<DifiMessage>
            {
                new DifiMessage
                {
                    lastUpdate = DateTime.Now,
                    receiverIdentifier = "987464291",
                    direction = "INCOMING",
                }
            };

            return incomingMessage;
        }

        private IEnumerable<DifiMessage> GetOutgoingMessageObject()
        {

            var outgoingMessage = new List<DifiMessage>
            {
                new DifiMessage
                {
                    lastUpdate = DateTime.Now,
                    senderIdentifier = "989778471",
                    direction = "OUTGOING",
                }
            };
            return outgoingMessage;
        }

        private List<DifiMessage> GetMessageObject()
        {

            var messageObject = new List<DifiMessage>
            {
                new DifiMessage
                {
                    id = 11,
                    conversationId = "40c8ccb8-aed8-4e1f-b87e-e27c0895813d",
                    senderIdentifier = "987464291",
                    receiverIdentifier = "989778471",
                },
                new DifiMessage
                {
                    id = 22,
                    conversationId = "40c8ccb8-aed8-4e1f-b87e-e27c0895813d",
                    senderIdentifier = "987464291",
                    receiverIdentifier = "989778471",
                }
            };
            return messageObject;
        }


        //////////////////////
        ///  UPDATED STUFF ///   
        //////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////
        ///   METHODS TO GET OBJECTS TO TEST WITH   ///   
        ///////////////////////////////////////////////

        private List<DifiMessage> GetDifiMessageList()
        {
            var difiMessages = new List<DifiMessage>
            {
                new DifiMessage
                {
                    id = 178,
                    conversationId = "aaed7220-2a0d-45a6-a2a4-3b24a069e08b",
                    messageId = "aaed7220-2a0d-45a6-a2a4-3b24a069e08b",
                    senderIdentifier = "989778471",
                    receiverIdentifier = "922308055",
                    lastUpdate = DateTime.Now,
                    direction = "INCOMING",
                    serviceIdentifier = "DPO",
                    latestMessageStatus = null,
                    created = DateTime.Now,
                },
                new DifiMessage
                {
                    id = 179,
                    conversationId = "wrong Id",
                    messageId = "wrong Id",
                    senderIdentifier = "989778471",
                    receiverIdentifier = "922308055",
                    lastUpdate = DateTime.Now,
                    direction = "INCOMING",
                    serviceIdentifier = "DPO",
                    latestMessageStatus = null,
                    created = DateTime.Now,
                }
            };
            return difiMessages;
        }

        private List<ElementsMessage> GetElementsMessageList()
        {
            var elementsMessages = new List<ElementsMessage>
            {
                new ElementsMessage
                {
                    Id = 9282,
                    CreatedDate = DateTime.Now,
                    IsRead = null,
                    ConversationId = "aaed7220-2a0d-45a6-a2a4-3b24a069e08b",
                    SendingMethod = null,
                    SendingStatus = null,
                },
                new ElementsMessage
                {
                    Id = 9283,
                    CreatedDate = DateTime.Now,
                    IsRead = null,
                    ConversationId = "wrong Id",
                    SendingMethod = null,
                    SendingStatus = null,
                }
            };
            return elementsMessages;
        }

        private List<Message> GetMessageList()
        {
            var messages = new List<Message>
            {
                new Message
                {
                    ElementsMessage = new ElementsMessage
                    {
                        Id = 9282,
                        CreatedDate = DateTime.Now,
                        IsRead = null,
                        ConversationId = "aaed7220-2a0d-45a6-a2a4-3b24a069e08b",
                        SendingMethod = null,
                        SendingStatus = null,
                    },
                    DifiMessage = new DifiMessage
                    {
                        id = 178,
                        conversationId = "aaed7220-2a0d-45a6-a2a4-3b24a069e08b",
                        messageId = "aaed7220-2a0d-45a6-a2a4-3b24a069e08b",
                        senderIdentifier = "989778471",
                        receiverIdentifier = "922308055",
                        lastUpdate = DateTime.Now,
                        direction = "INCOMING",
                        serviceIdentifier = "DPO",
                        latestMessageStatus = null,
                        created = DateTime.Now,
                    }
                },
                new Message
                {
                    ElementsMessage = new ElementsMessage
                    {
                        Id = 9283,
                        CreatedDate = DateTime.Now,
                        IsRead = null,
                        ConversationId = "wrong Id",
                        SendingMethod = null,
                        SendingStatus = null,
                    },
                    DifiMessage = new DifiMessage
                    {
                        id = 179,
                        conversationId = "wrong Id",
                        messageId = "wrong Id",
                        senderIdentifier = "989778471",
                        receiverIdentifier = "922308055",
                        lastUpdate = DateTime.Now,
                        direction = "INCOMING",
                        serviceIdentifier = "DPO",
                        latestMessageStatus = null,
                        created = DateTime.Now,
                    }
                }
            };
            return messages;
        }

        private async Task<ActionResult<Message>> GetMessageAsyncActualAsync()
        {
            var message = GetMessageList().ElementAt(0);
            var mockElementsMessageRepository = new Mock<IElementsMessageRepository>();
            mockElementsMessageRepository.Setup(repository => repository.GetElementsMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b")).ReturnsAsync(message.ElementsMessage);

            var mockDifiMessageRepository = new Mock<IDifiMessageRepository>();
            mockDifiMessageRepository.Setup(repository => repository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b")).ReturnsAsync(message.DifiMessage);

            var mockMessageMergeService = new Mock<IMessageMergeService>();

            mockMessageMergeService.Setup(service => service.MergeMessages(message.DifiMessage, message.ElementsMessage)).Returns(message);

            var messageController = new MessagesController();
            var actual = await messageController.GetMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b", mockMessageMergeService.Object, mockDifiMessageRepository.Object, mockElementsMessageRepository.Object);
            return actual;
        }

        private async Task<ActionResult<IEnumerable<Message>>> GetIncomingMessagesActualAsync()
        {
            var messages = GetMessageList();
            var mockElementsMessageRepository = new Mock<IElementsMessageRepository>();
            var elementsList = new List<ElementsMessage> { messages.ElementAt(0).ElementsMessage, messages.ElementAt(1).ElementsMessage };
            mockElementsMessageRepository.Setup(repository => repository.GetElementsMessagesAsync("922308055", Direction.INCOMING)).ReturnsAsync(elementsList);

            var mockDifiMessageRepository = new Mock<IDifiMessageRepository>();
            var difiList = new List<DifiMessage> { messages.ElementAt(0).DifiMessage, messages.ElementAt(1).DifiMessage };
            mockDifiMessageRepository.Setup(repository => repository.GetDifiMessagesAsync("922308055", Direction.INCOMING)).ReturnsAsync(difiList);

            var mockMessageMergeService = new Mock<IMessageMergeService>();
            mockMessageMergeService.Setup(service => service.MergeMessagesListsIn(difiList, elementsList)).Returns(messages);

            var mockMessagesService = new Mock<IMessagesService>();
            mockMessagesService.Setup(service => service.AddLatestStatus(difiList)).Returns(difiList);

            var messageController = new MessagesController();
            var actual = await messageController.GetIncomingMessagesAsync("922308055", mockMessageMergeService.Object, mockDifiMessageRepository.Object, mockElementsMessageRepository.Object, mockMessagesService.Object);
            return actual;
        }

        private async Task<ActionResult<IEnumerable<Message>>> GetOutgoingMessagesAsyncActualAsync()
        {
            var messages = GetMessageList();
            var mockElementsMessageRepository = new Mock<IElementsMessageRepository>();
            var elementsList = new List<ElementsMessage> { messages.ElementAt(0).ElementsMessage, messages.ElementAt(1).ElementsMessage };
            mockElementsMessageRepository.Setup(repository => repository.GetElementsMessagesAsync("922308055", Direction.OUTGOING)).ReturnsAsync(elementsList);

            var mockDifiMessageRepository = new Mock<IDifiMessageRepository>();
            var difiList = new List<DifiMessage> { messages.ElementAt(0).DifiMessage, messages.ElementAt(1).DifiMessage };
            mockDifiMessageRepository.Setup(repository => repository.GetDifiMessagesAsync("922308055", Direction.OUTGOING)).ReturnsAsync(difiList);

            var mockMessageMergeService = new Mock<IMessageMergeService>();
            mockMessageMergeService.Setup(service => service.MergeMessagesListsOut(elementsList, difiList)).Returns(messages);

            var mockMessagesService = new Mock<IMessagesService>();
            mockMessagesService.Setup(service => service.AddLatestStatus(difiList)).Returns(difiList);

            var messageController = new MessagesController();
            var actual = await messageController.GetOutgoingMessagesAsync("922308055", mockMessageMergeService.Object, mockElementsMessageRepository.Object, mockDifiMessageRepository.Object, mockMessagesService.Object);
            return actual;
        }

        //////////////////////
        ///   UNIT TESTS   ///   
        //////////////////////

        //Test for GetMessageAsync Controller
        [Fact]
        public async void TestGetMessageAsyncActualNotNullAsync()
        {
            var actual = await GetMessageAsyncActualAsync();
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetMessageAsyncObjectNotNullAsync()
        {
            var actual = await GetMessageAsyncActualAsync();
            Assert.NotNull(actual.Value);
        }
        [Fact]
        public async void TestGetMessageAsyncObjectTypeAsync()
        {
            var actual = await GetMessageAsyncActualAsync();
            Assert.IsType<Message>(actual.Value);
        }
        [Fact]
        public async void TestGetMessageAsyncObjectFieldTypeAsync()
        {
            var actual = await GetMessageAsyncActualAsync();
            Assert.IsType<String>(actual.Value.DifiMessage.messageId);
        }
        [Fact]
        public async void TestGetMessageAsyncObjectCorrectMergeAsync()
        {
            var actual = await GetMessageAsyncActualAsync();
            Assert.Equal(actual.Value.DifiMessage.messageId, actual.Value.ElementsMessage.ConversationId);
        }



        //Test for GetIncomingMessagesAsync Controller
        [Fact]
        public async void TestGetIncomingMessagesActualNotNullAsync()
        {
            var actual = await GetIncomingMessagesActualAsync();
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetIncomingMessagesObjectNotNullAsync()
        {
            var actual = await GetIncomingMessagesActualAsync();
            Assert.NotNull(actual.Value);
        }
        [Fact]
        public async void TestGetIncomingMessagesObjectTypeAsync()
        {
            var actual = await GetIncomingMessagesActualAsync();
            Assert.IsType<Message>(actual.Value.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestGetIncomingMessagesObjectFieldTypeAsync()
        {
            var actual = await GetIncomingMessagesActualAsync();
            Assert.IsType<DifiMessage>(actual.Value.ToList().ElementAt(0).DifiMessage);
        }
        [Fact]
        public async void TestGetIncomingMessagesObjectFieldType2Async()
        {
            var actual = await GetIncomingMessagesActualAsync();
            Assert.IsType<ElementsMessage>(actual.Value.ToList().ElementAt(1).ElementsMessage);
        }
        [Fact]
        public async void TestGetIncomingMessagesObjectCorrectMergeAsync()
        {
            var actual = await GetIncomingMessagesActualAsync();
            Assert.Equal(actual.Value.ToList().ElementAt(0).DifiMessage.messageId, actual.Value.ToList().ElementAt(0).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestGetIncomingMessagesObjectCorrectMerge2Async()
        {
            var actual = await GetIncomingMessagesActualAsync();
            Assert.Equal(actual.Value.ToList().ElementAt(1).DifiMessage.messageId, actual.Value.ToList().ElementAt(1).ElementsMessage.ConversationId);
        }



        //Test for GetCombinedMessagesOutAsync Controller
        [Fact]
        public async void TestGetMessageAsyncsOutActualNotNullAsync()
        {
            var actual = await GetOutgoingMessagesAsyncActualAsync();
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetMessageAsyncsOutObjectNotNullAsync()
        {
            var actual = await GetOutgoingMessagesAsyncActualAsync();
            Assert.NotNull(actual.Value);
        }
        [Fact]
        public async void TestGetMessageAsyncsOutObjectTypeAsync()
        {
            var actual = await GetOutgoingMessagesAsyncActualAsync();
            Assert.IsType<Message>(actual.Value.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestGetMessageAsyncsOutObjectFieldTypeAsync()
        {
            var actual = await GetOutgoingMessagesAsyncActualAsync();
            Assert.IsType<DifiMessage>(actual.Value.ToList().ElementAt(0).DifiMessage);
        }
        [Fact]
        public async void TestGetMessageAsyncsOutObjectFieldType2Async()
        {
            var actual = await GetOutgoingMessagesAsyncActualAsync();
            Assert.IsType<ElementsMessage>(actual.Value.ToList().ElementAt(1).ElementsMessage);
        }
        [Fact]
        public async void TestGetMessageAsyncsOutObjectCorrectMergeAsync()
        {
            var actual = await GetOutgoingMessagesAsyncActualAsync();
            Assert.Equal(actual.Value.ToList().ElementAt(0).DifiMessage.messageId, actual.Value.ToList().ElementAt(0).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestGetMessageAsyncsOutObjectCorrectMerge2Async()
        {
            var actual = await GetOutgoingMessagesAsyncActualAsync();
            Assert.Equal(actual.Value.ToList().ElementAt(1).DifiMessage.messageId, actual.Value.ToList().ElementAt(1).ElementsMessage.ConversationId);
        }

    }
}
