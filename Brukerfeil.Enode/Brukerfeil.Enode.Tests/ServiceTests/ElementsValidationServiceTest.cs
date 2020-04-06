//using System.Collections.Generic;
//using Xunit;
//using Brukerfeil.Enode.Common.Models;
//using Brukerfeil.Enode.Services;

//namespace Brukerfeil.Enode.Tests.ServiceTests
//{
//    public class ElementsValidationServiceTest
//    {
//        [Fact]
//        public void TestValidateIncomingElementsMessagesNotNull()
//        {
//            //Arrange
//            var elementsValidationService = new ElementsValidationService();

//            //Act
//            var actual = elementsValidationService.ValidateElementsIncomingMessages(GetSenderRecipientObject());

//            //Assert
//            Assert.NotNull(actual);
//        }

//        [Fact]
//        public void TestValidateOutgoingElementsMessagesNotNull()
//        {
//            //Arrange
//            var elementsValidationService = new ElementsValidationService();

//            //Act
//            var actual = elementsValidationService.ValidateElementsOutgoingMessages(GetSenderRecipientObject());

//            //Assert
//            Assert.NotNull(actual);
//        }

//        [Fact]
//        public void TestSortIncomingElementsMessagesMatchingType()
//        {
//            //Arrange
//            var elementsValidationService = new ElementsValidationService();

//            //Act
//            var actual = elementsValidationService.ValidateElementsOutgoingMessages(GetSenderRecipientObject());

//            //Assert
//            Assert.IsType<List<SenderRecipient>>(actual);
//        }

//        [Fact]
//        public void TestValidateOutgoingElementsMessagesMatchingType()
//        {
//            //Arrange
//            var elementsValidationService = new ElementsValidationService();

//            //Act
//            var actual = elementsValidationService.ValidateElementsOutgoingMessages(GetSenderRecipientObject());

//            //Assert
//            Assert.IsType<List<SenderRecipient>>(actual);
//        }

//        private List<SenderRecipient> GetSenderRecipientObject()
//        {
//            var sendingStatus = new SendingStatus
//            {
//                Id = "K",
//                Description = "Klar for sending"
//            };

//            var sendingMethod = new SendingMethod
//            {
//                Id = "SVARUT-P",
//                Description = "SvarUt - sendt til utskrift"
//            };

//            var senderRecpient = new List<SenderRecipient>
//            {
//                new SenderRecipient
//                {
//                    Id = 1081,
//                    IsRecipient = true,
//                    sendingStatuses = new List<SendingStatus>
//                    {
//                        sendingStatus
//                    },
//                    sendingMethod = new List<SendingMethod>
//                    {
//                        sendingMethod
//                    }
//                },
//                new SenderRecipient
//                {
//                    Id = 1082,
//                    IsRecipient = false,
//                    sendingStatuses = new List<SendingStatus>
//                    {
//                        sendingStatus
//                    },
//                    sendingMethod = new List<SendingMethod>
//                    {
//                        sendingMethod
//                    },
//                }
//            };  
//            return senderRecpient;
//        }
//    }
//}
