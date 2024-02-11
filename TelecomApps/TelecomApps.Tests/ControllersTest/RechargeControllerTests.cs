using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Telecom.Api.Controllers;
using Telecom.Domain.Configuration;
using Telecom.Domain.Dtos;
using Telecom.Domain.Interfaces;



namespace TelecomApps.Tests.ControllersTest
{
    public class RechargeControllerTests
    {

        [Fact]
        public async Task TopUp_InvalidRecharge_ReturnsBadRequest()
        {
            // Arrange
            var rechargeServiceMock = new Mock<IRechargeService>();
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.Setup(x => x.Value)
                         .Returns(new AppSettings { AllowedRechargePlans = new int[] { 5, 10, 20, 30, 50, 75, 100 } });

            var controller = new RechargeController(rechargeServiceMock.Object, appSettingsMock.Object);

            var invalidRecharge = new RechargeRequestDto
            {
                Amount = 15 // Adjust the amount based on your invalid plans
            };


            // Act
            var result = await controller.TopUp(invalidRecharge);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Enter a valid recharge amount. Available Plans: 5,10,20,30,50,75,100", badRequestResult.Value);
        }

    }
}
