using Telecom.Domain.Dtos;
using TelecomApps.Tests.Fixtures;

namespace TelecomApps.Tests.ServicesTest
{
    public class RechargeServiceTests : IClassFixture<RechargeServiceFixture>
    {
        private RechargeServiceFixture _fixture;

        public RechargeServiceTests(RechargeServiceFixture rechargeServiceFixture)
        {
            _fixture = rechargeServiceFixture;
        }

        [Fact]
        public async Task DoRechargeAsync_VerifiedUser_MaxRechargeThresholdExceptionTest()
        {
            // Arrange
            var rechargeRequest = new RechargeRequestDto()
            {
                Amount = 100,
                BeneficiaryId = 1

            };

            _fixture.SetupBeneficiaryDetails(1, 950, true, 3000, 1000);
            _fixture.SetupPayment(rechargeRequest.Amount);

            // Act
            async Task DoRechargeAsync() => await _fixture.RechargeService.DoRecharge(rechargeRequest);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(DoRechargeAsync);

            Assert.Equal("Maximum allowed recharge limit exceed for this month. Beneficiary: Test Beneficiary, Total Topup: 950,  Requested TopUp: 100.", exception.Message);

        }

        [Fact]
        public async Task DoRechargeAsync_UnVerifiedUser_MaxRechargeThresholdExceptionTest()
        {
            // Arrange
            var rechargeRequest = new RechargeRequestDto()
            {
                Amount = 100,
                BeneficiaryId = 1

            };

            _fixture.SetupBeneficiaryDetails(1, 450, false, 3000, 1000);
            _fixture.SetupPayment(rechargeRequest.Amount);

            // Act
            async Task DoRechargeAsync() => await _fixture.RechargeService.DoRecharge(rechargeRequest);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(DoRechargeAsync);

            Assert.Equal("Maximum allowed recharge limit exceed for this month. Beneficiary: Test Beneficiary, Total Topup: 450,  Requested TopUp: 100.", exception.Message);

        }

        [Fact]
        public async Task DoRechargeAsync_UnVerifiedUser_Success()
        {
            // Arrange
            var rechargeRequest = new RechargeRequestDto()
            {
                Amount = 100,
                BeneficiaryId = 1

            };

            _fixture.SetupBeneficiaryDetails(1, 400, false, 3000, 1000);
            _fixture.SetupPayment(rechargeRequest.Amount);

            // Act
            var result = await _fixture.RechargeService.DoRecharge(rechargeRequest);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Item2);
            Assert.Equal(rechargeRequest.Amount, result.Item1.Amount);

        }

        [Fact]
        public async Task DoRechargeAsync_VerifiedUser_Success()
        {
            // Arrange
            var rechargeRequest = new RechargeRequestDto()
            {
                Amount = 100,
                BeneficiaryId = 1

            };

            _fixture.SetupBeneficiaryDetails(1, 900, true, 3000, 1000);
            _fixture.SetupPayment(rechargeRequest.Amount);

            // Act
            var result = await _fixture.RechargeService.DoRecharge(rechargeRequest);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Item2);
            Assert.Equal(rechargeRequest.Amount, result.Item1.Amount);

        }

        [Fact]
        public async Task DoRechargeAsync_VerifiedUser_AccountThresholdExceededException()
        {
            // Arrange
            var rechargeRequest = new RechargeRequestDto()
            {
                Amount = 100,
                BeneficiaryId = 1

            };

            _fixture.SetupBeneficiaryDetails(1, 900, true, 3000, 50);
            _fixture.SetupPayment(rechargeRequest.Amount);

            // Act
            async Task DoRechargeAsync() => await _fixture.RechargeService.DoRecharge(rechargeRequest);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(DoRechargeAsync);

            Assert.Equal("Not enought credit. Requested TopUp: 100, Available Credit: 50", exception.Message);

        }
    }
}
