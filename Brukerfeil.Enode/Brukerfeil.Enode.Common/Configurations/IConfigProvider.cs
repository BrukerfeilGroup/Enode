using System.Collections.Generic;
using System.Threading.Tasks;
using Brukerfeil.Enode.Common.Models;
using Brukerfeil.Enode.Schemas;

namespace Brukerfeil.Enode.Common.Configurations
{
    public interface IConfigProvider
    {
        Task<OrganizationSchema> GetOrgConfigAsync(string tenant);
    }
}