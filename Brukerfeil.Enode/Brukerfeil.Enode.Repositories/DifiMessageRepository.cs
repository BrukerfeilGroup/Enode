using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Brukerfeil.Enode.Common.Enums;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Common.Repositories;
using Brukerfeil.Enode.Common.Services;

namespace Brukerfeil.Enode.Repositories
{
    public class DifiMessageRepository : IDifiMessageRepository
    {
        public HttpClient Client;
        private IConfigService _configService;

        public DifiMessageRepository(HttpClient client, IConfigService configService)
        {
            Client = client;
            _configService = configService;
        }

        //Takes organizationId and returns the 100 latest Difi messages for that organization as IEnumerable of DifiMessage objects
        public async Task<IEnumerable<DifiMessage>> GetDifiMessagesAsync(string organizationId, Direction direction)
        {
            //Passes the orgId to the config cache in order to retrive information about the Organization's API's etc
            var orgConfig = await _configService.GetConfigAsync(organizationId);

            //Variable request does the api query and puts json response in variable response
            var request = new HttpRequestMessage(HttpMethod.Get, $"{orgConfig.IntegrationPoint}/api/conversations?size=200&direction={direction}");
            request.Headers.Add("Accept", "application/json");
            var response = await Client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //Read the response as stream and deserialize it into the variable difiMessages(a list) - return the difiMessages list
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var difiMessages = await JsonSerializer.DeserializeAsync<DifiMessageContent>(responseStream);
                return difiMessages.content;
            }
            else
            {
                var exx = response.StatusCode.ToString();
                throw new ArgumentException(exx);
            }
        }

        //Takes organizationId and msgId and returns the a specific DifiMessage based on msgId for that organization
        public async Task<DifiMessage> GetDifiMessageAsync(string organizationId, string msgId)
        {
            //Passes the orgId to the dictionary in order to retrive information about the Organization's API's etc
            var orgConfig = await _configService.GetConfigAsync(organizationId);
            //Variable request does the api query and puts json response in variable response
            var request = new HttpRequestMessage(HttpMethod.Get, $"{orgConfig.IntegrationPoint}/api/conversations?messageId={msgId}");
            request.Headers.Add("Accept", "application/json");
            var response = await Client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //Read the response as stream and deserialize it into the variable difiMessages(a list) - return the first and only entry in the list, a single DifiMessage
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var difiMessages = await JsonSerializer.DeserializeAsync<DifiMessageContent>(responseStream);
                return difiMessages.content.ToList()[0];
            }
            else
            {
                var exx = response.StatusCode.ToString();
                throw new ArgumentException(exx);
            }
        }
    }
}