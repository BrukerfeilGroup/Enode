using Xunit;
using Moq;
using Moq.Protected;
using Brukerfeil.Enode.Repositories;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System;
using Brukerfeil.Enode.Common.Models;
using System.Collections.Generic;
using System.Linq;
using Brukerfeil.Enode.Common.Services;
using Brukerfeil.Enode.Common.Enums;
using Brukerfeil.Enode.Schemas;
using Brukerfeil.Enode.Services;

namespace Brukerfeil.Enode.Tests.RepositoryTests
{
    public class DifiMessageRepositoryTest
    {

        //Stubs

        private OrganizationSchema GetConfigStub(string orgId)
        {
            var configList = new List<OrganizationSchema>
            {
                new OrganizationSchema
                {
                    Database = "MASTER_ORA",
                    ElementsNcore = "https://svc01master.elements-ecm.no/ncore_master/odata",
                    Enabled = true,
                    ExternalSystemName = "ephorte5DocDelivery",
                    IntegrationPoint = "http://svc01common.elements-ecm.no:9093",
                    OrganizationId = 989778471,
                    OrganizationName = "Gecko Eiendom AS",
                    Password = "kpedersen",
                    Username = "kpedersen"
                },
               new OrganizationSchema
                {
                    Database = "MASTER_SQL",
                    ElementsNcore = "https://svc01master.elements-ecm.no/ncore_master/odata",
                    Enabled = true,
                    ExternalSystemName = "ephorte5DocDelivery",
                    IntegrationPoint = "http://svc01common.elements-ecm.no:9095",
                    OrganizationId = 922308055,
                    OrganizationName = "Sikri AS",
                    Password = "kpedersen",
                    Username = "kpedersen"
                },
            };

            return configList.Single(org => org.OrganizationId.Equals(Int32.Parse(orgId)));
        }

        private IEnumerable<DifiMessage> GetDifiMessageListWithStatusStub()
        {
            var difiMessages = new List<DifiMessage>
            {
                new DifiMessage
                {
                    id = 178,
                    messageTitle = "Title 1",
                    expiry = DateTime.Now,
                    conversationId = "wrongConvId",
                    messageId = "wrongMsgId",
                    senderIdentifier = "989778471",
                    receiverIdentifier = "922308055",
                    lastUpdate = DateTime.Now,
                    direction = "INCOMING",
                    serviceIdentifier = "DPO",
                    latestMessageStatus = "Status 1.2",
                    created = DateTime.Now,
                    messageStatuses = new List<MessageStatuses>
                    {
                        new MessageStatuses
                        {
                            id = 7700,
                            lastUpdate = DateTime.Parse("2020-04-22T13:34:44.645+02:00"),
                            status = "OPPRETTET"
                        },
                        new MessageStatuses
                        {
                            id = 7702,
                            lastUpdate = DateTime.Parse("2020-04-22T13:34:44.848+02:00"),
                            status = "SENDT"
                        }
                    }
                },
                //Used by GetMessageRepositorySingle method
                new DifiMessage
                {
                    id = 179,
                    messageTitle = "Title 2",
                    expiry = DateTime.Now,
                    conversationId = "aaed7220-2a0d-45a6-a2a4-3b24a069e08b",
                    messageId = "aaed7220-2a0d-45a6-a2a4-3b24a069e08b",
                    senderIdentifier = "989778471",
                    receiverIdentifier = "922308055",
                    lastUpdate = DateTime.Now,
                    direction = "INCOMING",
                    serviceIdentifier = "DPO",
                    latestMessageStatus = "Status 2.2",
                    created = DateTime.Now,
                    messageStatuses = new List<MessageStatuses>
                    {
                        new MessageStatuses
                        {
                            id = 7700,
                            lastUpdate = DateTime.Parse("2020-04-22T13:34:44.645+02:00"),
                            status = "OPPRETTET"
                        },
                        new MessageStatuses
                        {
                            id = 7702,
                            lastUpdate = DateTime.Parse("2020-04-22T13:34:44.848+02:00"),
                            status = "SENDT"
                        }
                    }
                }
            };

            return difiMessages;
        }
        

        ///////////////////////////////////////////////
        ///   METHODS TO GET OBJECTS TO TEST WITH   ///   
        ///////////////////////////////////////////////

        public DifiMessageRepository GetMessageRepository(string orgId)
        {
            //Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"content\": [{\"messageId\": \"aaed7220-2a0d-45a6-a2a4-3b24a069e08b\", \"senderIdentifier\": \"989778471\", \"receiverIdentifier\": \"922308055\", \"messageStatuses\": " +
                    "[{\"id\": 7700,\"lastUpdate\": \"2020-04-22T13:34:44.645+02:00\",\"status\": \"OPPRETTET\"}, " +
                    "{\"id\": 7702, \"lastUpdate\": \"2020-04-22T13:34:44.848+02:00\",\"status\": \"SENDT\"}]}" +
                    "]}")
                })
                .Verifiable();

            var mockConfigService = new Mock<IConfigService>();
            mockConfigService.Setup(service => service.GetConfigAsync(orgId)).ReturnsAsync(GetConfigStub(orgId));

            var baseURI = GetConfigStub(orgId).IntegrationPoint;
            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri(baseURI)
            };

            var mockMessagesService = new Mock<IMessagesService>();
            mockMessagesService.Setup(service => service.AddLatestStatusOnList(It.IsAny<IEnumerable<DifiMessage>>())).Returns(GetDifiMessageListWithStatusStub());

            var messageRepository = new DifiMessageRepository(httpClient, mockConfigService.Object, mockMessagesService.Object);
            return messageRepository;
        }
        
        public DifiMessageRepository GetMessageRepositorySingle(string orgId)
        {
            //Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"content\": [{\"messageId\": \"aaed7220-2a0d-45a6-a2a4-3b24a069e08b\", \"senderIdentifier\": \"989778471\", \"receiverIdentifier\": \"922308055\", \"messageStatuses\": " +
                    "[{\"id\": 7700,\"lastUpdate\": \"2020-04-22T13:34:44.645+02:00\",\"status\": \"OPPRETTET\"}, " +
                    "{\"id\": 7702, \"lastUpdate\": \"2020-04-22T13:34:44.848+02:00\",\"status\": \"SENDT\"}]}" +
                    "]}")
                })
                .Verifiable();

            var mockConfigService = new Mock<IConfigService>();
            mockConfigService.Setup(service => service.GetConfigAsync(orgId)).ReturnsAsync(GetConfigStub(orgId));

            var baseURI = GetConfigStub(orgId).IntegrationPoint;
            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri(baseURI)
            };

            var mockMessagesService = new Mock<IMessagesService>();
            mockMessagesService.Setup(service => service.AddLatestStatusOnSingle(It.IsAny<DifiMessage>())).Returns(GetDifiMessageListWithStatusStub().ElementAt(1));

            var messageRepository = new DifiMessageRepository(httpClient, mockConfigService.Object, mockMessagesService.Object);
            return messageRepository;
        }


        //////////////////////
        ///   UNIT TESTS   ///   
        //////////////////////

        //Method GetDifiMessagesAsync()
        [Fact]
        public async void TestGetDifiMessagesNotNullAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessagesAsync("922308055", Direction.INCOMING);
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetDifiMessagesHasEntriesAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessagesAsync("922308055", Direction.INCOMING);
            Assert.NotNull(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestGetDifiMessagesTypeAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessagesAsync("922308055", Direction.INCOMING);
            Assert.IsType<List<DifiMessage>>(actual);
        }
        [Fact]
        public async void TestGetDifiMessagesFieldCheckAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessagesAsync("922308055", Direction.INCOMING);
            Assert.True(
                GetDifiMessageListWithStatusStub().ToList().ElementAt(0).conversationId ==      actual.ToList().ElementAt(0).conversationId &&
                GetDifiMessageListWithStatusStub().ToList().ElementAt(0).messageTitle ==        actual.ToList().ElementAt(0).messageTitle &&
                GetDifiMessageListWithStatusStub().ToList().ElementAt(0).latestMessageStatus == actual.ToList().ElementAt(0).latestMessageStatus);
        }
        [Fact]
        public async void TestGetDifiMessagesFieldCheck2Async()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessagesAsync("922308055", Direction.INCOMING);
            Assert.True(
                GetDifiMessageListWithStatusStub().ToList().ElementAt(1).conversationId ==      actual.ToList().ElementAt(1).conversationId &&
                GetDifiMessageListWithStatusStub().ToList().ElementAt(1).messageTitle ==        actual.ToList().ElementAt(1).messageTitle &&
                GetDifiMessageListWithStatusStub().ToList().ElementAt(1).latestMessageStatus == actual.ToList().ElementAt(1).latestMessageStatus);
        }


        //Method GetDifiMessageAsync()
        [Fact]
        public async void TestGetDifiMessageNotNullAsync()
        {
            var messageRepository = GetMessageRepositorySingle("922308055");
            var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetDifiMessageHasIdAsync()
        {
            var messageRepository = GetMessageRepositorySingle("922308055");
            var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.NotNull(actual.messageId);
        }
        [Fact]
        public async void TestGetDifiMessageCorrectIdAsync()
        {
            var messageRepository = GetMessageRepositorySingle("922308055");
            var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.Equal(GetDifiMessageListWithStatusStub().ToList().ElementAt(1).messageId, actual.messageId);
        }


        //Method GetMessagesBySenderIdAsync()
        [Fact]
        public async void TestGetMessagesBySenderIdAsyncNotNull()
        {
            var messageRepository = GetMessageRepository("989778471");
            var actual = await messageRepository.GetMessagesBySenderIdAsync("989778471", "922308055");
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetMessagesBySenderIdAsyncType()
        {
            var messageRepository = GetMessageRepository("989778471");
            var actual = await messageRepository.GetMessagesBySenderIdAsync("989778471", "922308055");
            Assert.IsType<DifiMessage>(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestGetMessagesBySenderIdAsyncCorrectSenderId()
        {
            var messageRepository = GetMessageRepository("989778471");
            var expected = GetDifiMessageListWithStatusStub();
            var actual = await messageRepository.GetMessagesBySenderIdAsync("989778471", "922308055");
            Assert.Equal(actual.ToList().ElementAt(0).senderIdentifier, expected.ToList().ElementAt(0).senderIdentifier);
        }


        //Method GetMessagesByReceiverIdAsync()
        [Fact]
        public async void TestGetMessagesByReceiverIdAsyncNotNull()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetMessagesByReceiverIdAsync("922308055", "989778471");
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetMessagesByReceiverIdAsyncType()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetMessagesByReceiverIdAsync("922308055", "989778471");
            Assert.IsType<DifiMessage>(actual.ToList().ElementAt(0));
        }
        [Fact]
        public async void TestGetMessagesByReceiverIdAsyncCorrectReceiverId()
        {
            var messageRepository = GetMessageRepository("922308055");
            var expected = GetDifiMessageListWithStatusStub();
            var actual = await messageRepository.GetMessagesByReceiverIdAsync("922308055", "989778471");
            Assert.Equal(actual.ToList().ElementAt(0).receiverIdentifier, expected.ToList().ElementAt(0).receiverIdentifier);
        }
    }
}
