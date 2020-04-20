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

        private IEnumerable<DifiMessage> GetDifiMessageListStub()
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
                    Content = new StringContent("{\"content\": [{\"messageId\": \"aaed7220-2a0d-45a6-a2a4-3b24a069e08b\"," +
                    "\"senderIdentifier\": \"989778471\", \"receiverIdentifier\": \"922308055\"}]}")
                })
                .Verifiable();

            var mockConfigService = new Mock<IConfigService>();
            mockConfigService.Setup(service => service.GetConfigAsync(orgId)).ReturnsAsync(GetConfigStub(orgId));

            var baseURI = GetConfigStub(orgId).IntegrationPoint;
            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri(baseURI)
            };
            var messageRepository = new DifiMessageRepository(httpClient, mockConfigService.Object);
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
            Assert.NotNull(actual.ToList()[0]);
        }
        [Fact]
        public async void TestGetDifiMessagesTypeAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessagesAsync("922308055", Direction.INCOMING);
            Assert.IsType<List<DifiMessage>>(actual);
        }


        //Method GetDifiMessageAsync()
        [Fact]
        public async void TestGetDifiMessageNotNullAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetDifiMessageHasIdAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.NotNull(actual.messageId);
        }
        [Fact]
        public async void TestGetDifiMessageCorrectIdAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.Equal("aaed7220-2a0d-45a6-a2a4-3b24a069e08b", actual.messageId);
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
            var expected = GetDifiMessageListStub();
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
            var expected = GetDifiMessageListStub();
            var actual = await messageRepository.GetMessagesByReceiverIdAsync("922308055", "989778471");
            Assert.Equal(actual.ToList().ElementAt(0).receiverIdentifier, expected.ToList().ElementAt(0).receiverIdentifier);
        }
    }
}
