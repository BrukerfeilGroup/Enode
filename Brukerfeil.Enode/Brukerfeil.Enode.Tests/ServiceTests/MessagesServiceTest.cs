using System;
using System.Collections.Generic;
using Xunit;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Services;
using System.Linq;

namespace Brukerfeil.Enode.Tests.ServiceTests
{
    public class MessagesServiceTest
    {

        /////////////////
        ///   STUBS   ///   
        /////////////////

        private List<DifiMessage> GetDifiMessageListWithStatusStub()
        {
            var difiMessages = new List<DifiMessage>
            {
                new DifiMessage
                {
                    id = 178,
                    messageTitle = "Title 1",
                    latestMessageStatus = null,
                    created = DateTime.Parse("2019-04-22T13:34:44.645+12:11"),
                    messageStatuses = new List<MessageStatuses>
                    {
                        new MessageStatuses
                        {
                            id = 7700,
                            lastUpdate = DateTime.Parse("2020-02-22T13:34:44.645+02:00"),
                            status = "OPPRETTET"
                        },
                        new MessageStatuses
                        {
                            id = 7702,
                            lastUpdate = DateTime.Parse("2020-03-23T13:34:44.848+02:00"),
                            status = "SENDT"
                        }
                    }
                },
                new DifiMessage
                {
                    id = 179,
                    messageTitle = "Title 2",
                    latestMessageStatus = null,
                    created = DateTime.Parse("2019-04-22T13:34:44.645+12:11"),
                    messageStatuses = new List<MessageStatuses>
                    {
                        new MessageStatuses
                        {
                            id = 7700,
                            lastUpdate = DateTime.Parse("2020-02-21T13:34:44.645+02:00"),
                            status = "OPPRETTET"
                        },
                        new MessageStatuses
                        {
                            id = 7702,
                            lastUpdate = DateTime.Parse("2020-03-23T13:34:44.848+02:00"),
                            status = "SENDT2"
                        }
                    }
                }
            };
            return difiMessages;
        }

        private MessagesService GetMessagesService()
        {
            var messagesService = new MessagesService();
            return messagesService;
        }



        //////////////////////
        ///   UNIT TESTS   ///   
        //////////////////////
        [Fact]
        public void TestMessageServiceAddLatestStatusNotNull()
        {
            var messageService = GetMessagesService();
            var actual = messageService.AddLatestStatusOnList(GetDifiMessageListWithStatusStub());
            Assert.NotNull(actual);
        }
        [Fact]
        public void TestMessageServiceAddLatestStatusType()
        {
            var messageService = GetMessagesService();
            var actual = messageService.AddLatestStatusOnList(GetDifiMessageListWithStatusStub());
            Assert.IsType<List<DifiMessage>>(actual);
        }
        [Fact]
        public void TestMessageServiceAddLatestStatusIsUpdated()
        {
            var messagesService = GetMessagesService();
            var actual = messagesService.AddLatestStatusOnList(GetDifiMessageListWithStatusStub());
            Assert.Equal("SENDT", actual.ToList().ElementAt(0).latestMessageStatus);
        }
        [Fact]
        public void TestMessageServiceAddLatestStatusIsUpdated2()
        {
            var messagesService = GetMessagesService();
            var actual = messagesService.AddLatestStatusOnList(GetDifiMessageListWithStatusStub());
            Assert.Equal("SENDT2", actual.ToList().ElementAt(1).latestMessageStatus);
        }
        [Fact]
        public void TestMessageServiceAddLatestStatusCreatedField()
        {
            var messagesService = GetMessagesService();
            var actual = messagesService.AddLatestStatusOnList(GetDifiMessageListWithStatusStub());
            Assert.Equal(DateTime.Parse("2020-02-22T13:34:44.645+02:00"), actual.ToList().ElementAt(0).created);
        }
        [Fact]
        public void TestMessageServiceAddLatestStatusCreatedField2()
        {
            var messagesService = GetMessagesService();
            var actual = messagesService.AddLatestStatusOnList(GetDifiMessageListWithStatusStub());
            Assert.Equal(DateTime.Parse("2020-02-21T13:34:44.645+02:00"), actual.ToList().ElementAt(1).created);
        }
    }
}