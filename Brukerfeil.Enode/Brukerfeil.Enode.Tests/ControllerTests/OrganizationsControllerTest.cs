using Brukerfeil.Enode.API.Controllers;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Brukerfeil.Enode.Common.Services;
using Brukerfeil.Enode.Common.Models;

namespace Brukerfeil.Enode.Test.OrganizationsControllerTests
{

    public class ConfigServiceTest
    {
        [Fact]
        public async void TestGetOrgConfigNotNullAsync()
        {
            //Arrange
            var mockConfigService = new Mock<IConfigService>();
            mockConfigService.Setup(service => service.GetOrganizationsAsync()).ReturnsAsync(GetOrganizationObject());

            var orgController = new OrganizationsController();

            //Act
            var actual = await orgController.GetOrgConfigAsync(mockConfigService.Object);

            //Assert
            mockConfigService.VerifyAll();
            Assert.NotNull(actual);
        }

        [Fact]
        public async void TestGetOrgConfigTypeAsync()
        {
            //Arrange
            var mockConfigService = new Mock<IConfigService>();
            mockConfigService.Setup(service => service.GetOrganizationsAsync()).ReturnsAsync(GetOrganizationObject());

            var orgController = new OrganizationsController();

            //Act
            var actual = await orgController.GetOrgConfigAsync(mockConfigService.Object);

            //Assert
            Assert.IsType<Organization>(actual.Value.ElementAt(0));

        }

        [Fact]
        public async void TestOrgNameAndOrgIdAsync()
        {
            //Arrange
            var mockConfigService = new Mock<IConfigService>();
            mockConfigService.Setup(service => service.GetOrganizationsAsync()).ReturnsAsync(GetOrganizationObject());

            var orgController = new OrganizationsController();
            var expected = GetOrganizationObject();

            //Act
            var actual = await orgController.GetOrgConfigAsync(mockConfigService.Object);

            //Assert
            Assert.True(actual.Value.ElementAt(0).OrgId == expected.ElementAt(0).OrgId &&
                actual.Value.ElementAt(0).OrgName == expected.ElementAt(0).OrgName);

        }

        private List<Organization> GetOrganizationObject()
        {

            var organizations = new List<Organization> 
            { new Organization(989778471, "Gecko Eiendom AS"),
              new Organization(922308055, "Sikri AS") };
            return organizations;
        }
    }
}