using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Common.Repositories;
using System.Linq;
using System.Text.Json;
using Brukerfeil.Enode.Common.Services;
using Brukerfeil.Enode.Common.Enums;

namespace Brukerfeil.Enode.Repositories
{
    public class ElementsMessageRepository : IElementsMessageRepository
    {
        public HttpClient Client;
        private IConfigService _configService;
        public ElementsMessageRepository(HttpClient client, IConfigService configService)
        {
            Client = client;
            _configService = configService;
        }


        //Takes organizationId and returns the top 200 latest Elements messages for that organization as IEnumerable of ElementsMessage objects
        public async Task<IEnumerable<ElementsMessage>> GetElementsMessagesAsync(string organizationId, Direction direction)
        {
            //Passes the orgId to the dictionary in order to retrive information about the Organization's API's etc
            var orgConfig = await _configService.GetConfigAsync(organizationId);
            var currentDate = DateTime.UtcNow.ToString("O");

            var recipientValue = direction.Equals(Direction.INCOMING) ? "false" : "true";
            //Authentication for the organizations Elements api
            var authValueByteArray = Encoding.ASCII.GetBytes($"{orgConfig.Username}:{orgConfig.Password}");
            Client.DefaultRequestHeaders.Add("Accept", "application/json;odata.metadata=none");
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authValueByteArray));

            //Variable request does the api query and puts json response in variable response
            var request = new HttpRequestMessage(HttpMethod.Get, $"{orgConfig.ElementsNcore}/SenderRecipient?$filter=CreatedDate le {currentDate} and ConversationId ne null and IsRecipient eq {recipientValue} &$expand=SendingMethod,SendingStatus&$top=200&$orderby=CreatedDate desc&Database={orgConfig.Database}&ExternalSystemName={orgConfig.ExternalSystemName}");
            var response = await Client.SendAsync(request);
            using HttpContent content = response.Content;

            if (response.IsSuccessStatusCode)
            {
                //Read the response as stream and deserialize it into the variable elementsMessages(a list) - return the elementsMessages list
                var responseStream = await response.Content.ReadAsStreamAsync();
                var elementsMessages = await JsonSerializer.DeserializeAsync<ElementsMessageContent>(responseStream);
                return elementsMessages.value;
            }
            else
            {
                var exx = response.StatusCode.ToString();
                throw new ArgumentException(exx);
            }
        }

        //Takes organizationId and msgId and returns the a specific ElementsMessage based on msgId for that organization
        public async Task<ElementsMessage> GetElementsMessageAsync(string organizationId, string msgId)
        {
            //Passes the orgId to the dictionary in order to retrive information about the Organization's API's etc
            var orgConfig = await _configService.GetConfigAsync(organizationId);

            //Authentication for the organizations Elements api
            var authValueByteArray = Encoding.ASCII.GetBytes($"{orgConfig.Username}:{orgConfig.Password}");
            Client.DefaultRequestHeaders.Add("Accept", "application/json;odata.metadata=none");
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authValueByteArray));

            //Variable request does the api query and puts json response in variable response
            var request = new HttpRequestMessage(HttpMethod.Get, $"{orgConfig.ElementsNcore}/SenderRecipient?$filter=ConversationId eq '{msgId}' &$expand=SendingMethod,SendingStatus&Database={orgConfig.Database}&ExternalSystemName={orgConfig.ExternalSystemName}");
            var response = await Client.SendAsync(request);
            using HttpContent content = response.Content;

            if (response.IsSuccessStatusCode)
            {
                //Read the response as stream and deserialize it into the variable elementsMessages(a list) - return the first and only entry in the list, a single ElementsMessage
                var responseStream = await response.Content.ReadAsStreamAsync();
                var elementsMessages = await JsonSerializer.DeserializeAsync<ElementsMessageContent>(responseStream);
                return elementsMessages.value.ToList()[0];
            }
            else
            {
                var exx = response.StatusCode.ToString();
                throw new ArgumentException(exx);
            }
        }
    }
}


