using System;
using System.Collections.Generic;
using Xunit;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Services;
using System.Linq;

namespace Brukerfeil.Enode.Tests.ServiceTests
{
    public class SortingServiceTest
    {
        private DifiMessage _message1 { get; set; }
        private DifiMessage _message2 { get; set; }

        public SortingServiceTest()
        {
            _message1 = new DifiMessage
            {
                lastUpdate = DateTime.Parse("2020-02-04T22:22:25.061+01:00")
            };

            _message2 = new DifiMessage
            {
                lastUpdate = DateTime.Parse("2020-02-03T22:22:25.061+01:00")
            };
        }

        [Fact]
        public void TestSortingServiceNotNull()
        {
            //Arrange
            var sortingService = new SortingService();

            //Act
            var actual = sortingService.SortMessages(GetMessageObject());

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void TestSortMessageType()
        {
            //Arrange
            var sortingService = new SortingService();

            //Act
            var actual = sortingService.SortMessages(GetMessageObject());

            //Assert
            Assert.IsType<List<DifiMessage>>(actual);
        }

        [Fact]
        public void TestSortMessagesByDescending()
        {
            //Arrange
            var sortingService = new SortingService();

            var expected = new List<DifiMessage>()
            {
                _message1, _message2
            };

            //Act
            var actual = sortingService.SortMessages(GetMessageObject());

            //Assert
            Assert.Equal(actual.ToList()[0], expected[0]);
            //This works as well -->  Assert.Equal(actual.content.ToList()[0], message2);

        }

        private IEnumerable<DifiMessage> GetMessageObject()
        {
            var messageArray = new List<DifiMessage>()
            {
                _message2, _message1
            };
            return messageArray;
        }
    }
}
