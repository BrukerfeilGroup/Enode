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

// namespace Brukerfeil.Enode.Tests.RepositoryTests
// {
//     public class ElementsMessageRepositoryTest
//     {
//         ///////////////////////////////////////////////
//         ///   METHODS TO GET OBJECTS TO TEST WITH   ///   
//         ///////////////////////////////////////////////

//         public ElementsMessageRepository GetMessageRepository()
//         {
//             //Arrange
//             var mockMessageHandler = new Mock<HttpMessageHandler>();
//             mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
//                 ItExpr.IsAny<HttpRequestMessage>(),
//                 ItExpr.IsAny<CancellationToken>())
//                 .ReturnsAsync(new HttpResponseMessage()
//                 {
//                     StatusCode = HttpStatusCode.OK,
//                     Content = new StringContent("{\"value\": [{\"ConversationId\": \"aaed7220-2a0d-45a6-a2a4-3b24a069e08b\"}]}")
//                 })
//                 .Verifiable();

//             var httpClient = new HttpClient(mockMessageHandler.Object)
//             {
//                 BaseAddress = new Uri("http://svc01common.elements-ecm.no:9095/api/conversations?size=200")
//             };
//             var messageRepository = new ElementsMessageRepository(httpClient);
//             return messageRepository;
//         }



//         //////////////////////
//         ///   UNIT TESTS   ///   
//         //////////////////////

//         //Method GetElementsMessagesAsync()
//         [Fact]
//         public async void TestGetElementsMessagesNotNullAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetElementsMessagesAsync("922308055");
//             Assert.NotNull(actual);
//         }
//         [Fact]
//         public async void TestGetElementsMessagesHasEntriesAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetElementsMessagesAsync("922308055");
//             Assert.NotNull(actual.ToList()[0]);
//         }
//         [Fact]
//         public async void TestGetElementsMessagesTypeAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetElementsMessagesAsync("922308055");
//             Assert.IsType<List<ElementsMessage>>(actual);
//         }



//         //Method GetElementsMessageAsync()
//         [Fact]
//         public async void TestGetElementsMessageNotNullAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetElementsMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
//             Assert.NotNull(actual);
//         }
//         [Fact]
//         public async void TestGetElementsMessageHasIdAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetElementsMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
//             Assert.NotNull(actual.ConversationId);
//         }
//         [Fact]
//         public async void TestGetElementsMessageCorrectIdAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetElementsMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
//             Assert.Equal("aaed7220-2a0d-45a6-a2a4-3b24a069e08b", actual.ConversationId);
//         }

//     }
// }
