using System;
using System.Collections.Generic;
using System.Text;

namespace Brukerfeil.Enode.Common.Models
{
    public class Organization
    {
        public int OrgId { get; set; }
        public string OrgName { get; set; }

        public Organization(int orgId, string orgName)
        {
            OrgId = orgId;
            OrgName = orgName;
        }
    }
}
