using System.ComponentModel.DataAnnotations;
using Telecom.Domain.Dtos;
using TelecomApps.Tests.Fixtures;

namespace TelecomApps.Tests
{
    public class BeneficiaryServiceTests : IClassFixture<BeneficiaryServiceTestFixture>
    {
        private readonly BeneficiaryServiceTestFixture _fixture;

        public BeneficiaryServiceTests(BeneficiaryServiceTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateBeneficiaryAsync_WithValidData_ReturnsBeneficiaryDto()
        {
            // Arrange
            _fixture.CommonFixtureSetup(accountId: 1, existingBeneficiariesCount: 3);


            var model = new BeneficiaryDto { NickName = "TestBeneficiary", PhoneNumber = "1234567890" };
            int accountId = 1;

            // Act
            var result = await _fixture.BeneficiaryService.CreateBeneficiaryAsync(model, accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.NickName, result.NickName);
            Assert.Equal(model.PhoneNumber, result.PhoneNumber);
        }

        [Fact]
        public async Task CreateBeneficiaryAsync_WhenAccountExceedsLimit_ReturnsError()
        {
            // Arrange
            _fixture.CommonFixtureSetup(accountId: 1, existingBeneficiariesCount: 5);

            var model = new BeneficiaryDto { NickName = "TestBeneficiary", PhoneNumber = "1234567890" };
            int accountId = 1;


            // Act
            async Task CreateBeneficiary() => await _fixture.BeneficiaryService.CreateBeneficiaryAsync(model, accountId);

            // Assert
            await Assert.ThrowsAsync<Exception>(CreateBeneficiary);
        }

        [Fact]
        public void CreateBeneficiaryAsync_WhenNickNameExceeds_Twenty_Character_ReturnsError()
        {
            // Arrange
            _fixture.CommonFixtureSetup(accountId: 1, existingBeneficiariesCount: 4);

            var model = new BeneficiaryDto { NickName = "TestBeneficiaryMoreThanTwentyCharacter", PhoneNumber = "1234567890" };

            // Act
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

            // Assert
            Assert.False(isValid, "the model should be invalid");

            // Assert specific validation errors
            Assert.NotEmpty(validationResults);

            // Assert that there is a validation error specifically related to the NickName property
            Assert.Single(validationResults, vr => vr.MemberNames.Contains("NickName"));

        }
    }
}
