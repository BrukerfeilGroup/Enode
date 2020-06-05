using Brukerfeil.Enode.Common.Configurations;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Common.Services;
using Brukerfeil.Enode.Schemas;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brukerfeil.Enode.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigProvider _configProvider;
        private readonly IMemoryCache _memoryCache;

        public ConfigService(IConfigProvider configProvider, IMemoryCache memoryCache)
        {
            _configProvider = configProvider;
            _memoryCache = memoryCache;
        }

        public async Task<OrganizationSchema> GetConfigAsync(string orgId)
        {
            var orgs = await GetCachedOrganizationsAsync();
            var id = int.Parse(orgId);
            return orgs.ToList().FirstOrDefault(o => o.OrganizationId.Equals(id));

        }  

        private async Task<IEnumerable<OrganizationSchema>> GetCachedOrganizationsAsync()
        {
            return await _memoryCache.GetOrCreateAsync("Organizations", async cacheEntry => {
                var gecko = await _configProvider.GetOrgConfigAsync("FEATURES_MASTER-ORA");
                var sikri = await _configProvider.GetOrgConfigAsync("FEATURES_MASTER-SQL");

                return new List<OrganizationSchema> { gecko, sikri };
            });
        } 

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync()
        {
            //Maps tenantConfig into Organization-object to be consumed by frontend
            var organizations = new List<Organization>();
            var tenantConfigs = await GetCachedOrganizationsAsync();

            tenantConfigs.ToList().ForEach(tC => organizations.Add(new Organization(tC.OrganizationId, tC.OrganizationName)));
            return organizations;
        }
    }
}
