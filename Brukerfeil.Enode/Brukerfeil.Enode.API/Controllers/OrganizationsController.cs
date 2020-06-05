using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Brukerfeil.Enode.Common.Services;
using Brukerfeil.Enode.Common.Models;
using System;
using System.Linq;

namespace Brukerfeil.Enode.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrgConfigAsync([FromServices] IConfigService service)
        {
            try
            {
                var orgList = await service.GetOrganizationsAsync();
                if (orgList == null)
                {
                    return StatusCode(500);
                }
                return orgList.ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(503, ex.Message);
            }
        }
    }
}