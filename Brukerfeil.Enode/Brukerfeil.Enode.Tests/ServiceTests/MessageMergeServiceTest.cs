using System;
using System.Collections.Generic;
using Xunit;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Services;
using System.Linq;
using Brukerfeil.Enode.Common.Repositories;
using Moq;

namespace Brukerfeil.Enode.Tests.ServiceTests
{
    public class MessageMergeServiceTest
    {
        private DifiMessage _difiMessage1 { get; set; }
        private DifiMessage _difiMessage2 { get; set; }
        private DifiMessage _difiMessage3 { get; set; }
        private ElementsMessage _eleMessage1 { get; set; }
        private ElementsMessage _eleMessage2 { get; set; }

        ////////////////////////
        ///    CONSTRUCTOR   ///   
        ////////////////////////
        public MessageMergeServiceTest()
        {
            _difiMessage1 = new DifiMessage
            {
                messageId = "test1",
                conversationId = "test1",
                senderIdentifier = "sender1"
            };
            _difiMessage2 = new DifiMessage
            {
                messageId = "test2",
                conversationId = "test2",
                senderIdentifier = "sender2"
            };
            _difiMessage3 = new DifiMessage
            {
                messageId = "differentMessageId",
                conversationId = "differentConversationId",
                senderIdentifier = "something"
            };
            _eleMessage1 = new ElementsMessage
            {
                ConversationId = "test1",
                IsRead = "true"
            };
            _eleMessage2 = new ElementsMessage
            {
                ConversationId = "test2",
                IsRead = "false"
            };
        }

        /////////////////
        ///   STUBS   ///   
        /////////////////
        private IEnumerable<DifiMessage> GetDifiMessageObjectList()
        {
            var difiMessageArray = new List<DifiMessage>()
            {
                _difiMessage1, _difiMessage2, _difiMessage3
            };
            return difiMessageArray;
        }

        private IEnumerable<ElementsMessage> GetElementsMessageObjectList()
        {
            var eleMessageArray = new List<ElementsMessage>()
            {
                _eleMessage1, _eleMessage2
            };
            return eleMessageArray;
        }

        private MessageMergeService GetMessageMergeService()
        {
            var mockElementsMessageRepository = new Mock<IElementsMessageRepository>();
            mockElementsMessageRepository.Setup(repository => repository.GetElementsMessageAsync("922308055", "test1")).ReturnsAsync(_eleMessage1);
            var mockDifiMessageRepository = new Mock<IDifiMessageRepository>();
            mockDifiMessageRepository.Setup(repository => repository.GetDifiMessageAsync("922308055", "test1")).ReturnsAsync(_difiMessage1);
            var mergeService = new MessageMergeService(mockElementsMessageRepository.Object, mockDifiMessageRepository.Object);
            return mergeService;
        }


        //////////////////////
        ///   UNIT TESTS   ///   
        //////////////////////

        //Method MergeMessages()
        [Fact]
        public void TestMergeMessagesNotNull()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);
            Assert.NotNull(actual);
        }
        [Fact]
        public void TestMergeMessagesType()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);
            Assert.IsType<Message>(actual);
        }
        [Fact]
        public void TestMergeMessagesCorrectMatch()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);
            Assert.Equal(actual.DifiMessage.messageId, actual.ElementsMessage.ConversationId);
        }
        [Fact]
        public void TestMergeMessagesMergingFieldSenderIdentifier()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);
            Assert.Equal(actual.DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public void TestMergeMessagesMergingFieldIsRead()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);
            Assert.Equal(actual.ElementsMessage.IsRead, _eleMessage1.IsRead);
        }
        [Fact]
        public void TestMergingMessages()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);
            Assert.True(actual.ElementsMessage == _eleMessage1 && actual.DifiMessage == _difiMessage1);
        }


        //Method MergeMessagesListsIn()
        [Fact]
        public async void TestMergeMessagesListInNotNull()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestMergeMessagesListInHasEntries()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.NotNull(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestMergeMessagesListInType()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.IsType<List<Message>>(actual);
        }
        [Fact]
        public async void TestMergeMessagesListInEntriesType1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.IsType<Message>(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestMergeMessagesListInEntriesType2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.IsType<DifiMessage>(actual.ToList().ElementAt(0).DifiMessage);
        }
        [Fact]
        public async void TestMergeMessagesListInEntriesType3()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.IsType<ElementsMessage>(actual.ToList().ElementAt(0).ElementsMessage);
        }
        [Fact]
        public async void TestMergeMessagesListInCorrectMatch1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.messageId, actual.ToList().ElementAt(0).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestMergeMessagesListInCorrectMatch2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(1).DifiMessage.messageId, actual.ToList().ElementAt(1).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestMergeMessagesListInMergingFieldSenderIdentifier1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public async void TestMergeMessagesListInMergingFieldSenderIdentifier2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(1).DifiMessage.senderIdentifier, _difiMessage2.senderIdentifier);
        }
        [Fact]
        public async void TestMergeMessagesListInMergingFieldIsRead1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(0).ElementsMessage.IsRead, _eleMessage1.IsRead);
        }
        [Fact]
        public async void TestMergeMessagesListInMergingFieldIsRead2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(1).ElementsMessage.IsRead, _eleMessage2.IsRead);
        }
        [Fact]
        public async void TestMergeMessagesListInIfMergeWithNull1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), null);
            Assert.True(null == actual.ToList().ElementAt(0).ElementsMessage && _difiMessage1 == actual.ToList().ElementAt(0).DifiMessage);
        }
        [Fact]
        public async void TestMergeMessagesListInIfMergeWithNull2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), null);
            Assert.True(null == actual.ToList().ElementAt(1).ElementsMessage && _difiMessage2 == actual.ToList().ElementAt(1).DifiMessage);
        }
        [Fact]
        public async void TestMergeMessagesListInSkipAndRemoveIfDifferentId()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());
            //This asserts that it removed _difiMessage3 from the list - because this does not have matching messageId and conversationId
            Assert.True(actual.ToList().Count() == 2);
        }


        //Method MergeMessagesListsOut()
        [Fact]
        public async void TestMergeMessagesListOutNotNull()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestMergeMessagesListOutHasEntries()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.NotNull(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestMergeMessagesListOutType()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.IsType<List<Message>>(actual);
        }
        [Fact]
        public async void TestMergeMessagesListOutEntriesType1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.IsType<Message>(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestMergeMessagesListOutEntriesType2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.IsType<DifiMessage>(actual.ToList().ElementAt(0).DifiMessage);
        }
        [Fact]
        public async void TestMergeMessagesListOutEntriesType3()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.IsType<ElementsMessage>(actual.ToList().ElementAt(0).ElementsMessage);
        }
        [Fact]
        public async void TestMergeMessagesListOutCorrectMatch1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.messageId, actual.ToList().ElementAt(0).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestMergeMessagesListOutCorrectMatch2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(1).DifiMessage.messageId, actual.ToList().ElementAt(1).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestMergeMessagesListOutMergingFieldSenderIdentifier1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public async void TestMergeMessagesListOutMergingFieldSenderIdentifier2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(1).DifiMessage.senderIdentifier, _difiMessage2.senderIdentifier);
        }
        [Fact]
        public async void TestMergeMessagesListOutMergingFieldIsRead1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(0).ElementsMessage.IsRead, _eleMessage1.IsRead);
        }
        [Fact]
        public async void TestMergeMessagesListOutMergingFieldIsRead2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());
            Assert.Equal(actual.ToList().ElementAt(1).ElementsMessage.IsRead, _eleMessage2.IsRead);
        }
        [Fact]
        public async void TestMergeMessagesListOutIfMergeWithNull1()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), null);
            Assert.True(null == actual.ToList().ElementAt(0).DifiMessage && _eleMessage1 == actual.ElementAt(0).ElementsMessage);
        }
        [Fact]
        public async void TestMergeMessagesListOutIfMergeWithNull2()
        {
            var messageMergeService = GetMessageMergeService();
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), null);
            Assert.True(null == actual.ToList().ElementAt(1).DifiMessage && _eleMessage2 == actual.ElementAt(1).ElementsMessage);
        }
    }
}
