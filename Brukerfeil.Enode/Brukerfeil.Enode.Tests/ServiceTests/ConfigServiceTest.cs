using System.Collections.Generic;
using Xunit;
using Moq;
using Brukerfeil.Enode.Common.Configurations;
using Brukerfeil.Enode.Services;
using Microsoft.Extensions.Caching.Memory;
using Brukerfeil.Enode.Schemas;
using Brukerfeil.Enode.Common.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Brukerfeil.Enode.Tests.ServiceTests
{
    public class ConfigServiceTest
    {
        [Fact]
        public async void TestGetConfigAsyncNotNull()
        {
            //Arrange
            var configService = new ConfigService(GetConfigProviderMock(), GetMemoryCache());

            //Act
            var actual = await configService.GetConfigAsync("989778471");

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public async void TestGetConfigAsyncType()
        {
            //Arrange
            var configService = new ConfigService(GetConfigProviderMock(), GetMemoryCache());

            //Act
            var actual = await configService.GetConfigAsync("989778471");

            //Assert
            Assert.IsType<OrganizationSchema>(actual);
        }

        [Fact]
        public async void TestGetConfigAsync()
        {
            //Arrange
            var configService = new ConfigService(GetConfigProviderMock(), GetMemoryCache());
            var expextedConfig = new OrganizationSchema
            {
                OrganizationId = 989778471,
                OrganizationName = "Gecko Eiendom AS",
                Database = "MASTER_ORA"
            };
            //Act
            var actual = await configService.GetConfigAsync("989778471");

            //Assert
            Assert.True(actual.OrganizationId == expextedConfig.OrganizationId && 
                actual.OrganizationName == expextedConfig.OrganizationName);
        }

        [Fact] 
        public async void TestGetOrganizationsAsyncNotNull()
        {
            //Arrange
            var configService = new ConfigService(GetConfigProviderMock(), GetMemoryCache());

            //Act
            var actual = await configService.GetOrganizationsAsync();

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public async void TestGetOrganizationsAsyncType()
        {
            //Arrange
            var configService = new ConfigService(GetConfigProviderMock(), GetMemoryCache());

            //Act
            var actual = await configService.GetOrganizationsAsync();

            //Assert
            Assert.IsType<List<Organization>>(actual);
        }

        [Fact]
        public async void TestGetOrganizatonAsync()
        {
            //Arrange
            var configService = new ConfigService(GetConfigProviderMock(), GetMemoryCache());
            var expected = new List<Organization>
            {
                new Organization(989778471, "Gecko Eiendom AS"),
                new Organization(922308055, "Sikri AS"),

            };
            //Act
            var actual = await configService.GetOrganizationsAsync();

            //Assert
            Assert.True(actual.ToList().ElementAt(0).OrgId == expected.ToList().ElementAt(0).OrgId);
        }

        private OrganizationSchema GetOrganizationSchemaStub()
        {
            var organizationSchema = new OrganizationSchema
            {
                OrganizationId = 989778471,
                OrganizationName = "Gecko Eiendom AS",
                Database = "MASTER_ORA",
            };
            return organizationSchema;
        }

        private IEnumerable<OrganizationSchema> GetOrganizationSchemaListStub()
        {
            var organizations = new List<OrganizationSchema>
            {
                new OrganizationSchema
                {
                    OrganizationId = 989778471,
                    OrganizationName = "Gecko Eiendom AS",
                    Database = "MASTER_ORA",
                },
            };
            
            return organizations;
        }

        private IMemoryCache GetMemoryCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            memoryCache.Set("Organizations", GetOrganizationSchemaListStub());
            return memoryCache;
        }

        private IConfigProvider GetConfigProviderMock()
        {
            var configProviderMock = new Mock<IConfigProvider>();
            configProviderMock.Setup(configProvider => 
            configProvider.GetOrgConfigAsync("MASTER_ORA"))
                .ReturnsAsync(GetOrganizationSchemaStub());
            return configProviderMock.Object;
        }
    }
}
