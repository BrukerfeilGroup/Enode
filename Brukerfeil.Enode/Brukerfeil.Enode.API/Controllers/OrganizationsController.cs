﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Brukerfeil.Enode.Common.Configurations;
using Brukerfeil.Enode.Schemas;
using Brukerfeil.Enode.Common.Services;
using Brukerfeil.Enode.Common.Models;

namespace Brukerfeil.Enode.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        public OrganizationsController()
        {

        }
        [HttpGet]
        public async Task<IEnumerable<Organization>> GetOrgConfigAsync([FromServices] IConfigService service)
        {
            return await service.GetOrganizationsAsync();
        }
    }
}