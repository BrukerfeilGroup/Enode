using Brukerfeil.Enode.Common.Models;
using System;
using System.Collections.Generic;
using Brukerfeil.Enode.Common.Services;
using System.Linq;

namespace Brukerfeil.Enode.Services
{
    public class MessageMergeService : IMessageMergeService
    {
        //Takes a single difi and elements message object and returns them in a single message object 
        public Message MergeMessages(DifiMessage difiMessages, ElementsMessage elementsMessages)
        {
            return new Message
            {
                DifiMessage = difiMessages,
                ElementsMessage = elementsMessages
            };
         //Same code typed more explicitly, kept for learning purposes
            //var finalMsg = new Message();
            //finalMsg.DifiMessage = difiMessage;
            //finalMsg.ElementsMessage = elementsMessage;
            //return finalMsg;
        }

        // Merging based on Difi, should be used to merge incoming messages
        //Takes two lists (list of difi messages and a list of elements messages). 
        //Match each difi message with the corresponding elements message, 
        //if there is no match it merges the difi message with NULL.
        //Merging is based on DifiMessage.messageId and ElementsMessage.ConversationId
        //Returns a list of merged messages into Message-object
        public IEnumerable<Message> MergeMessagesListsIn(IEnumerable<DifiMessage> difiMessages, IEnumerable<ElementsMessage> elementsMessages)
        {
            //The final list of Message objects that the method returns
            List<Message> listOfMessages = new List<Message>();

            //The foreach loop iterates the list of difiMessages
            foreach (DifiMessage dmsg in difiMessages) {     
                //We are only interested in Difi entries where messageId and conversationId matches, this if statements skips difi entry if there is no match
                if (!dmsg.messageId.Equals(dmsg.conversationId))
                {
                    continue;
                }
                try
                {
                //elementsMatch = a single elementsMessages entry where its ConversationId matches the iterated difiMessages.messageId
                var elementsMatch = elementsMessages.Single(eleMessage => eleMessage.ConversationId.Equals(dmsg.messageId));
                var message = MergeMessages(dmsg, elementsMatch);
                //Append the merged message to the list of merged messages to be returned
                listOfMessages.Add(message);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex + " Error caught and thrown in Class(MessageMergeService), method(MergeMessagesList)");
                    Console.WriteLine("Detailed info: ");
                    Console.WriteLine("Difimessage " + dmsg.messageId + " does not have a match in the provided Elements message list");
                } 
                //This is triggered when there is no elements match, and the difimessage is merged with null and added to the list.
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex + " Custom: Error caught in Class(MessageMergeService), method(MergeMessagesList)");
                    Console.WriteLine("This error is caught every time a Difi message does not match an Elements message.");
                    var message = MergeMessages(dmsg, null);
                    listOfMessages.Add(message);
                }
            }
            return listOfMessages;
        }

        // Merging based on Elements, should be used to merge outgoing messages
        //Takes two lists (list of elements messages and a list of difi messages). 
        //Match each Elements message with the corresponding difi message, 
        //if there is no match it merges the Elements message with NULL.
        //Merging is based on DifiMessage.messageId and ElementsMessage.ConversationId
        //Returns a list of merged messages into Message-object
        public IEnumerable<Message> MergeMessagesListsOut(IEnumerable<ElementsMessage> elementsMessages, IEnumerable<DifiMessage> difiMessages)
        {
            //The final list of Message objects that the method returns
            List<Message> listOfMessages = new List<Message>();

            //The foreach loop iterates the list of difiMessages
            foreach (ElementsMessage emsg in elementsMessages)
            {
                try
                {
                    //difiMatch = a single difiMessages entry where its messageId matches the iterated ElementsMessages.ConversationId
                    var difiMatch = difiMessages.Single(difMessage => difMessage.messageId.Equals(emsg.ConversationId));
                    var message = MergeMessages(difiMatch, emsg);
                    //Append the merged message to the list of merged messages to be returned
                    listOfMessages.Add(message);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex + " Custom: Error caught and thrown in Class(MessageMergeService), method(MergeMessagesList)");
                    throw;
                }
                //This is triggered when there is no match, and the message is merged with null and added to the list.
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex + " Custom: Error caught in Class(MessageMergeService), method(MergeMessagesList)");
                    Console.WriteLine("This error is caught every time an Elements message does not match an Difi message.");
                    var message = MergeMessages(null, emsg);
                    listOfMessages.Add(message);
                }
            }
            return listOfMessages;
        }
    }
}
