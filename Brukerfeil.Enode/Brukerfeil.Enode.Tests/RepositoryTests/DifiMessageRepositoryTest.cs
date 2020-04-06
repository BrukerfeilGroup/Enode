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
//     public class DifiMessageRepositoryTest
//     {

//         ///////////////////////////////////////////////
//         ///   METHODS TO GET OBJECTS TO TEST WITH   ///   
//         ///////////////////////////////////////////////

//         public DifiMessageRepository GetMessageRepository()
//         {
//             //Arrange
//             var mockMessageHandler = new Mock<HttpMessageHandler>();
//             mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
//                 ItExpr.IsAny<HttpRequestMessage>(),
//                 ItExpr.IsAny<CancellationToken>())
//                 .ReturnsAsync(new HttpResponseMessage()
//                 {
//                     StatusCode = HttpStatusCode.OK,
//                     Content = new StringContent("{\"content\": [{\"messageId\": \"aaed7220-2a0d-45a6-a2a4-3b24a069e08b\"}]}")
//                 })
//                 .Verifiable();

//             var httpClient = new HttpClient(mockMessageHandler.Object)
//             {
//                 BaseAddress = new Uri("http://svc01common.elements-ecm.no:9095/api/conversations?size=200")
//             };
//             var messageRepository = new DifiMessageRepository(httpClient);
//             return messageRepository;
//         }


//         //////////////////////
//         ///   UNIT TESTS   ///   
//         //////////////////////

//         //Method GetDifiMessagesAsync()
//         [Fact]
//         public async void TestGetDifiMessagesNotNullAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetDifiMessagesAsync("922308055");
//             Assert.NotNull(actual);
//         }
//         [Fact]
//         public async void TestGetDifiMessagesHasEntriesAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetDifiMessagesAsync("922308055");
//             Assert.NotNull(actual.ToList()[0]);
//         }
//         [Fact]
//         public async void TestGetDifiMessagesTypeAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetDifiMessagesAsync("922308055");
//             Assert.IsType<List<DifiMessage>>(actual);
//         }


//         //Method GetDifiMessageAsync()
//         [Fact]
//         public async void TestGetDifiMessageNotNullAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
//             Assert.NotNull(actual);
//         }
//         [Fact]
//         public async void TestGetDifiMessageHasIdAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
//             Assert.NotNull(actual.messageId);
//         }
//         [Fact]
//         public async void TestGetDifiMessageCorrectIdAsync()
//         {
//             var messageRepository = GetMessageRepository();
//             var actual = await messageRepository.GetDifiMessageAsync("922308055", "aaed7220-2a0d-45a6-a2a4-3b24a069e08b");
//             Assert.Equal("aaed7220-2a0d-45a6-a2a4-3b24a069e08b", actual.messageId);
//         }


//     }
// }
