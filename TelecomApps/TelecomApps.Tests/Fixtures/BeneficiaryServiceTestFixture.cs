

using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using Telecom.Application.Services;
using Telecom.Domain.Configuration;
using Telecom.Domain.Dtos;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Domain.Models;

namespace TelecomApps.Tests.Fixtures
{
    public class BeneficiaryServiceTestFixture : IDisposable
    {
        public Mock<IBeneficiaryRepository> BeneficiaryRepoMock { get; set; }
        public Mock<IMapper> MapperMock { get; set; }
        public Mock<IOptions<AppSettings>> AppSettingsMock { get; set; }
        public BeneficiaryService BeneficiaryService { get; set; }

        public BeneficiaryServiceTestFixture()
        {
            // Common setup for all tests
            BeneficiaryRepoMock = new Mock<IBeneficiaryRepository>();
            MapperMock = new Mock<IMapper>();

            // Configure AppSettings for the service

            AppSettingsMock = new Mock<IOptions<AppSettings>>();

            AppSettingsMock.Setup(x => x.Value)
                             .Returns(new AppSettings { MaxBeneficiaryPerAccount = 5 });

            BeneficiaryService = new BeneficiaryService(BeneficiaryRepoMock.Object, AppSettingsMock.Object, MapperMock.Object);
        }

        public void CommonFixtureSetup(int accountId, int existingBeneficiariesCount)
        {

           

            BeneficiaryRepoMock.Setup(repo => repo.GetBeneficiariesCoundForAccount(accountId))
                              .ReturnsAsync(existingBeneficiariesCount);

            BeneficiaryRepoMock.Setup(repo => repo.CreateBeneficiary(It.IsAny<Beneficiary>(), It.IsAny<int>()))
                               .ReturnsAsync((Beneficiary b, int accountId) =>
                               {
                                   b.Id = 2;
                                   return b;
                               });


            MapperMock.Setup(m => m.Map<BeneficiaryDto>(It.IsAny<Beneficiary>()))
                       .Returns((Beneficiary source) => new BeneficiaryDto() { NickName = source.NickName, PhoneNumber = source.PhoneNumber });
        }

        public void Dispose()
        {
            // Clean up resources, if needed
        }
    }

}
