using System;
using System.Collections.Generic;
using Xunit;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Services;

namespace Brukerfeil.Enode.Tests.ServiceTests
{
    public class MessagesServiceTest
    {
        [Fact]
        public void TestMessageServiceAddLatestStatusNotNull()
        {
            //Arrange
            var messageService = new MessagesService();

            //Act
            var actual = messageService.AddLatestStatus(messageServiceTestObject());

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void TestMessageServiceAddLatestStatusType()
        {
            //Arrange
            var messageService = new MessagesService();

            //Act
            var actual = messageService.AddLatestStatus(messageServiceTestObject());

            //Assert
            Assert.IsType<List<DifiMessage>>(actual);
        }

        [Fact]
        public void TestMessageServiceAddLatestStatusIsUpdated()
        {
            //Arrange
            var messageService = new MessagesService();

            //Act
            var actual = messageService.AddLatestStatus(messageServiceTestObject());

            //Assert

            foreach (var content in actual)
            {
                Assert.Equal("LEVETID_UTLOPT", content.latestMessageStatus);
            }
        }

        public IEnumerable<DifiMessage> messageServiceTestObject()
        {
            var message = new List<DifiMessage>
            {
                new DifiMessage
                {
                    latestMessageStatus = null,
                    messageStatuses = new List<MessageStatuses>
                    {
                        new MessageStatuses
                        {
                            id = 57343,
                            lastUpdate = Convert.ToDateTime("2020-02-14T12:17:08.214+01:00"),
                            status = "OPPRETTET"
                        },
                        new MessageStatuses
                        {
                            id = 57344,
                            lastUpdate = Convert.ToDateTime("2020-02-14T12:17:08.216+01:00"),
                            status = "INNKOMMENDE_MOTTATT"
                        },
                        new MessageStatuses
                        {
                            id = 57348,
                            lastUpdate = Convert.ToDateTime("2020-02-14T14:17:25.362+01:00"),
                            status = "LEVETID_UTLOPT"
                        }
                    }
                }
            };
            return message;
        }
    }
}