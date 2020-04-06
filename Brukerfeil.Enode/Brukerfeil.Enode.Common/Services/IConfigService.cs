using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Schemas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brukerfeil.Enode.Common.Services
{
    public interface IConfigService
    {
        public Task<OrganizationSchema> GetConfigAsync(string orgId);
        public Task<IEnumerable<Organization>> GetOrganizationsAsync();
    }
}
