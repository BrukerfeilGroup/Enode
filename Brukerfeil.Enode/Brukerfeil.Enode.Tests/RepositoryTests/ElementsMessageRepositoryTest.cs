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
using Brukerfeil.Enode.Schemas;
using Brukerfeil.Enode.Common.Enums;

namespace Brukerfeil.Enode.Tests.RepositoryTests
{
    public class ElementsMessageRepositoryTest
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

            return configList.FirstOrDefault(org => org.OrganizationId.Equals(Int32.Parse(orgId)));

        }

        private IEnumerable<ElementsMessage> GetElementsMessageListStub()
        {
            var elementsMessages = new List<ElementsMessage>
            {
                new ElementsMessage
                {
                    ExternalId = "989778471",
                },
                new ElementsMessage
                {
                    ExternalId = "922308055",
                }
            };
            return elementsMessages;
        }

        ///////////////////////////////////////////////
        ///   METHODS TO GET OBJECTS TO TEST WITH   ///   
        ///////////////////////////////////////////////

        public ElementsMessageRepository GetMessageRepository(string orgId)
        {
            //Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"value\": [{\"ConversationId\": \"aaed7220-2a0d-45a6-a2a4-3b24a069e08b\", \"ExternalId\": \"989778471\"}, {\"ExternalId\": \"922308055\"}]}")
                })
                .Verifiable();

            var mockConfigService = new Mock<IConfigService>();
            mockConfigService.Setup(service => service.GetConfigAsync(orgId)).ReturnsAsync(GetConfigStub(orgId));

            var baseURI = GetConfigStub(orgId).IntegrationPoint;
            var httpClient = new HttpClient(mockMessageHandler.Object)
            {
                BaseAddress = new Uri(baseURI)
            };
            var messageRepository = new ElementsMessageRepository(httpClient, mockConfigService.Object);
            return messageRepository;
        }



        //////////////////////
        ///   UNIT TESTS   ///   
        //////////////////////

        //Method GetElementsMessagesAsync()
        [Fact]
        public async void TestGetElementsMessagesNotNullAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetElementsMessagesAsync("922308055", Direction.INCOMING);
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetElementsMessagesHasEntriesAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetElementsMessagesAsync("922308055", Direction.INCOMING);
            Assert.NotNull(actual.ToList()[0]);
        }
        [Fact]
        public async void TestGetElementsMessagesTypeAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetElementsMessagesAsync("922308055", Direction.INCOMING);
            Assert.IsType<List<ElementsMessage>>(actual);
        }


        //Method GetElementsMessageAsync()
        [Fact]
        public async void TestGetElementsMessageNotNullAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetElementsMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.NotNull(actual);
        }
        [Fact]
        public async void TestGetElementsMessageHasIdAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetElementsMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.NotNull(actual.ConversationId);
        }
        [Fact]
        public async void TestGetElementsMessageCorrectIdAsync()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetElementsMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
            Assert.Equal("aaed7220-2a0d-45a6-a2a4-3b24a069e08b", actual.ConversationId);
        }

        //Method GetElementsMessagesBySenderIdAsync()
        [Fact]
        public async void TestGetElementsMessagesBySenderIdAsyncNotNull()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetElementsMessagesBySenderIdAsync("922308055", "989778471");
            Assert.NotNull(actual);
        }

        [Fact]
        public async void TestGetElementsMessagesBySenderIdAsyncType()
        {
            var messageRepository = GetMessageRepository("922308055");
            var actual = await messageRepository.GetElementsMessagesBySenderIdAsync("922308055", "989778471");
            Assert.IsType<ElementsMessage>(actual.ToList().ElementAt(0));
        }

        [Fact]
        public async void TestGetElementsMessagesBySenderIdAsyncCorrectSenderId()
        {
            var messageRepository = GetMessageRepository("922308055");
            var expected = GetElementsMessageListStub();
            var actual = await messageRepository.GetElementsMessagesBySenderIdAsync("922308055", "989778471");
            Assert.Equal(actual.ToList().ElementAt(0).ExternalId, expected.ToList().ElementAt(0).ExternalId);
        }

        //Method GetElementsMessagesByReceiverIdAsync()

        [Fact]
        public async void TestGetElementsMessagesByReceiverIdAsyncNotNull()
        {
            var messageRepository = GetMessageRepository("989778471");
            var actual = await messageRepository.GetElementsMessagesByReceiverIdAsync("989778471", "922308055");
            Assert.NotNull(actual);
        }

        [Fact]
        public async void TestGetElementsMessagesByReceiverIdAsyncType()
        {
            var messageRepository = GetMessageRepository("989778471");
            var actual = await messageRepository.GetElementsMessagesByReceiverIdAsync("989778471", "922308055");
            Assert.IsType<ElementsMessage>(actual.ToList().ElementAt(0));
        }

        [Fact]
        public async void TestGetElementsMessagesByReceiverIdAsyncCorrectReceiverId()
        {
            var messageRepository = GetMessageRepository("989778471");
            var expected = GetElementsMessageListStub();
            var actual = await messageRepository.GetElementsMessagesByReceiverIdAsync("989778471", "922308055");
            Assert.Equal(actual.ToList().ElementAt(1).ExternalId, expected.ToList().ElementAt(1).ExternalId);
        }
    }
}
