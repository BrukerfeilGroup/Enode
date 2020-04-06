using System;
using System.Collections.Generic;
using Xunit;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Services;
using System.Linq;

namespace Brukerfeil.Enode.Tests.ServiceTests
{
    public class MessageMergeServiceTest
    {
        private DifiMessage _difiMessage1 { get; set; }
        private DifiMessage _difiMessage2 { get; set; }
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

        ////////////////////////////////////////////////
        ///   METHODS TO GET OBJECTS TO TEST WITH   ///   
        ///////////////////////////////////////////////
        private IEnumerable<DifiMessage> GetDifiMessageObjectList()
        {
            var difiMessageArray = new List<DifiMessage>()
            {
                _difiMessage1, _difiMessage2
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
        private Message GetMessageMergeService()
        {
            //Arrange
            var mergeService = new MessageMergeService();
            //Act
            var actual = mergeService.MergeMessages(_difiMessage1, _eleMessage1);
            return actual;
        }
        private List<Message> GetMessageMergeListInService()
        {
            //Arrange
            var mergeService = new MessageMergeService();
            //Act
            var difiMessageList = GetDifiMessageObjectList();
            var elementsMessageList = GetElementsMessageObjectList();
            var actual = mergeService.MergeMessagesListsIn(difiMessageList, elementsMessageList).ToList();
            return actual;
        }
        private List<Message> GetMessageMergeListOutService()
        {
            //Arrange
            var mergeService = new MessageMergeService();
            //Act
            var difiMessageList = GetDifiMessageObjectList();
            var elementsMessageList = GetElementsMessageObjectList();
            var actual = mergeService.MergeMessagesListsOut(elementsMessageList, difiMessageList).ToList();
            return actual;
        }



        //////////////////////
        ///   UNIT TESTS   ///   
        //////////////////////

        //Method MergeMessages()
        [Fact]
        public void TestMergeMessagesNotNull()
        {
            var actual = GetMessageMergeService();
            Assert.NotNull(actual);
        }
        [Fact]
        public void TestMergeMessagesType()
        {
            var actual = GetMessageMergeService();
            Assert.IsType<Message>(actual);
        }
        [Fact]
        public void TestMergeMessagesCorrectMatch()
        {
            var actual = GetMessageMergeService();
            Assert.Equal(actual.DifiMessage.messageId, actual.ElementsMessage.ConversationId);
        }
        [Fact]
        public void TestMergeMessagesMergingFieldSenderIdentifier()
        {
            var actual = GetMessageMergeService();
            Assert.Equal(actual.DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public void TestMergeMessagesMergingFieldIsRead()
        {
            var actual = GetMessageMergeService();
            Assert.Equal(actual.ElementsMessage.IsRead, _eleMessage1.IsRead);
        }




        //Method MergeMessagesListsIn()
        [Fact]
        public void TestMergeMessagesListInNotNull()
        {
            var actual = GetMessageMergeListInService();
            Assert.NotNull(actual);
        }
        [Fact]
        public void TestMergeMessagesListInHasEntries()
        {
            var actual = GetMessageMergeListInService();
            Assert.NotNull(actual[0]);
        }
        [Fact]
        public void TestMergeMessagesListInType()
        {
            var actual = GetMessageMergeListInService();
            Assert.IsType<List<Message>>(actual);
        }
        [Fact]
        public void TestMergeMessagesListInEntriesType1()
        {
            var actual = GetMessageMergeListInService();
            Assert.IsType<Message>(actual[0]);
        }
        [Fact]
        public void TestMergeMessagesListInEntriesType2()
        {
            var actual = GetMessageMergeListInService();
            Assert.IsType<DifiMessage>(actual[0].DifiMessage);
        }
        [Fact]
        public void TestMergeMessagesListInEntriesType3()
        {
            var actual = GetMessageMergeListInService();
            Assert.IsType<ElementsMessage>(actual[0].ElementsMessage);
        }
        [Fact]
        public void TestMergeMessagesListInCorrectMatch1()
        {
            var actual = GetMessageMergeListInService();
            Assert.Equal(actual[0].DifiMessage.messageId, actual[0].ElementsMessage.ConversationId);
        }
        [Fact]
        public void TestMergeMessagesListInCorrectMatch2()
        {
            var actual = GetMessageMergeListInService();
            Assert.Equal(actual[1].DifiMessage.messageId, actual[1].ElementsMessage.ConversationId);
        }
        [Fact]
        public void TestMergeMessagesListInMergingFieldSenderIdentifier1()
        {
            var actual = GetMessageMergeListInService();
            Assert.Equal(actual[0].DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public void TestMergeMessagesListInMergingFieldSenderIdentifier2()
        {
            var actual = GetMessageMergeListInService();
            Assert.Equal(actual[1].DifiMessage.senderIdentifier, _difiMessage2.senderIdentifier);
        }
        [Fact]
        public void TestMergeMessagesListInMergingFieldIsRead1()
        {
            var actual = GetMessageMergeListInService();
            Assert.Equal(actual[0].ElementsMessage.IsRead, _eleMessage1.IsRead);
        }
        [Fact]
        public void TestMergeMessagesListInMergingFieldIsRead2()
        {
            var actual = GetMessageMergeListInService();
            Assert.Equal(actual[1].ElementsMessage.IsRead, _eleMessage2.IsRead);
        }




        //Method MergeMessagesListsOut()
        [Fact]
        public void TestMergeMessagesListOutNotNull()
        {
            var actual = GetMessageMergeListOutService();
            Assert.NotNull(actual);
        }
        [Fact]
        public void TestMergeMessagesListOutHasEntries()
        {
            var actual = GetMessageMergeListOutService();
            Assert.NotNull(actual[0]);
        }
        [Fact]
        public void TestMergeMessagesListOutType()
        {
            var actual = GetMessageMergeListOutService();
            Assert.IsType<List<Message>>(actual);
        }
        [Fact]
        public void TestMergeMessagesListOutEntriesType1()
        {
            var actual = GetMessageMergeListOutService();
            Assert.IsType<Message>(actual[0]);
        }
        [Fact]
        public void TestMergeMessagesListOutEntriesType2()
        {
            var actual = GetMessageMergeListOutService();
            Assert.IsType<DifiMessage>(actual[0].DifiMessage);
        }
        [Fact]
        public void TestMergeMessagesListOutEntriesType3()
        {
            var actual = GetMessageMergeListOutService();
            Assert.IsType<ElementsMessage>(actual[0].ElementsMessage);
        }
        [Fact]
        public void TestMergeMessagesListOutCorrectMatch1()
        {
            var actual = GetMessageMergeListOutService();
            Assert.Equal(actual[0].DifiMessage.messageId, actual[0].ElementsMessage.ConversationId);
        }
        [Fact]
        public void TestMergeMessagesListOutCorrectMatch2()
        {
            var actual = GetMessageMergeListOutService();
            Assert.Equal(actual[1].DifiMessage.messageId, actual[1].ElementsMessage.ConversationId);
        }
        [Fact]
        public void TestMergeMessagesListOutMergingFieldSenderIdentifier1()
        {
            var actual = GetMessageMergeListOutService();
            Assert.Equal(actual[0].DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public void TestMergeMessagesListOutMergingFieldSenderIdentifier2()
        {
            var actual = GetMessageMergeListOutService();
            Assert.Equal(actual[1].DifiMessage.senderIdentifier, _difiMessage2.senderIdentifier);
        }
        [Fact]
        public void TestMergeMessagesListOutMergingFieldIsRead1()
        {
            var actual = GetMessageMergeListOutService();
            Assert.Equal(actual[0].ElementsMessage.IsRead, _eleMessage1.IsRead);
        }
        [Fact]
        public void TestMergeMessagesListOutMergingFieldIsRead2()
        {
            var actual = GetMessageMergeListOutService();
            Assert.Equal(actual[1].ElementsMessage.IsRead, _eleMessage2.IsRead);
        }
    }
}
