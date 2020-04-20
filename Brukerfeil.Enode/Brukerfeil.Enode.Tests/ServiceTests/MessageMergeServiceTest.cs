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
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);

            //Assert
            Assert.NotNull(actual);
        }
        [Fact]
        public void TestMergeMessagesType()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);

            //Assert
            Assert.IsType<Message>(actual);
        }
        [Fact]
        public void TestMergeMessagesCorrectMatch()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);
            
            //Assert
            Assert.Equal(actual.DifiMessage.messageId, actual.ElementsMessage.ConversationId);
        }
        [Fact]
        public void TestMergeMessagesMergingFieldSenderIdentifier()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);

            //Assert
            Assert.Equal(actual.DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public void TestMergeMessagesMergingFieldIsRead()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = messageMergeService.MergeMessages(_difiMessage1, _eleMessage1);

            //Assert
            Assert.Equal(actual.ElementsMessage.IsRead, _eleMessage1.IsRead);
        }


        //Method MergeMessagesListsIn()
        [Fact]
        public async void TestMergeMessagesListInNotNull()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestMergeMessagesListInHasEntries()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.NotNull(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestMergeMessagesListInType()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.IsType<List<Message>>(actual);
        }
        [Fact]
        public async void TestMergeMessagesListInEntriesType1()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.IsType<Message>(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestMergeMessagesListInEntriesType2()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.IsType<DifiMessage>(actual.ToList().ElementAt(0).DifiMessage);
        }
        [Fact]
        public async void TestMergeMessagesListInEntriesType3()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.IsType<ElementsMessage>(actual.ToList().ElementAt(0).ElementsMessage);
        }
        [Fact]
        public async void TestMergeMessagesListInCorrectMatch1()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.messageId, actual.ToList().ElementAt(0).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestMergeMessagesListInCorrectMatch2()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.messageId, actual.ToList().ElementAt(0).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestMergeMessagesListInMergingFieldSenderIdentifier1()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public async void TestMergeMessagesListInMergingFieldSenderIdentifier2()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(1).DifiMessage.senderIdentifier, _difiMessage2.senderIdentifier);
        }
        [Fact]
        public async void TestMergeMessagesListInMergingFieldIsRead1()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(0).ElementsMessage.IsRead, _eleMessage1.IsRead);
        }
        [Fact]
        public async void TestMergeMessagesListInMergingFieldIsRead2()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsInAsync("922308055", GetDifiMessageObjectList(), GetElementsMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(1).ElementsMessage.IsRead, _eleMessage2.IsRead);
        }


        //Method MergeMessagesListsOut()
        [Fact]
        public async void TestMergeMessagesListOutNotNull()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestMergeMessagesListOutHasEntries()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.NotNull(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestMergeMessagesListOutType()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.IsType<List<Message>>(actual);
        }
        [Fact]
        public async void TestMergeMessagesListOutEntriesType1()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.IsType<Message>(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestMergeMessagesListOutEntriesType2()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.IsType<DifiMessage>(actual.ToList().ElementAt(0).DifiMessage);
        }
        [Fact]
        public async void TestMergeMessagesListOutEntriesType3()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.IsType<ElementsMessage>(actual.ToList().ElementAt(0).ElementsMessage);
        }
        [Fact]
        public async void TestMergeMessagesListOutCorrectMatch1()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.messageId, actual.ToList().ElementAt(0).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestMergeMessagesListOutCorrectMatch2()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(1).DifiMessage.messageId, actual.ToList().ElementAt(1).ElementsMessage.ConversationId);
        }
        [Fact]
        public async void TestMergeMessagesListOutMergingFieldSenderIdentifier1()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(0).DifiMessage.senderIdentifier, _difiMessage1.senderIdentifier);
        }
        [Fact]
        public async void TestMergeMessagesListOutMergingFieldSenderIdentifier2()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(1).DifiMessage.senderIdentifier, _difiMessage2.senderIdentifier);
        }
        [Fact]
        public async void TestMergeMessagesListOutMergingFieldIsRead1()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(0).ElementsMessage.IsRead, _eleMessage1.IsRead);
        }
        [Fact]
        public async void TestMergeMessagesListOutMergingFieldIsRead2()
        {
            //Arrange
            var messageMergeService = GetMessageMergeService();

            //Act
            var actual = await messageMergeService.MergeMessagesListsOutAsync("922308055", GetElementsMessageObjectList(), GetDifiMessageObjectList());

            //Assert
            Assert.Equal(actual.ToList().ElementAt(1).ElementsMessage.IsRead, _eleMessage2.IsRead);
        }
    }
}
