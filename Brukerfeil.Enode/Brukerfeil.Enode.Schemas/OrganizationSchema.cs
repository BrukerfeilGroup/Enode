using System.ComponentModel;
using System.Runtime.Serialization;

namespace Brukerfeil.Enode.Schemas
{
    [Description("Configuration for organizations")]
    public class OrganizationSchema
    {
        public const string Schema = "organization";

        [DataMember(Name = "OrganizationId")]
        [DefaultValue(1)]
        [Description("Organization Identifier")]
        public int OrganizationId { get; set; }

        [DataMember(Name = "OrganizationName")]
        [DefaultValue("OrgName")]
        [Description("Organization name")]
        public string OrganizationName { get; set; }

        [DataMember(Name = "Username")]
        [DefaultValue("user")]
        [Description("Username")]
        public string Username { get; set; }

        [DataMember(Name = "Password")]
        [DefaultValue("1234")]
        [Description("Password")]
        public string Password { get; set; }

        [DataMember(Name = "IntegrationPoint")]
        [DefaultValue("http://url-til-ip.no")]
        [Description("URL to an organization's Difi integrationpoint")]
        public string IntegrationPoint { get; set; }

        [DataMember(Name = "ElementsNcore")]
        [DefaultValue("http://url-til-ip.no")]
        [Description("URL to ELements NCore integrationpoint")]
        public string ElementsNcore { get; set; }

        [DataMember(Name = "Enabled")]
        [DefaultValue(false)]
        [Description("Enable reading values")]
        public bool Enabled { get; set; }

        [DataMember(Name = "Database")]
        [DefaultValue("MASTER")]
        [Description("Organization's database")]
        public string Database { get; set; }

        [DataMember(Name = "ExternalSystemName")]
        [DefaultValue("ephorte5DocDelivery")]
        [Description("Organization's external system name")]
        public string ExternalSystemName { get; set; }

    }
}