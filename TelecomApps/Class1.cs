using System;

namespace TelecomApps.Tests.Fixtures
{
    public class BeneficiaryServiceTestFixture : IDisposable
    {
        public Mock<IBeneficiaryRepository> BeneficiaryRepoMock { get; private set; }
        public Mock<IMapper> MapperMock { get; private set; }
        public BeneficiaryService BeneficiaryService { get; private set; }

        public BeneficiaryServiceTestFixture()
        {
            // Common setup for all tests
            BeneficiaryRepoMock = new Mock<IBeneficiaryRepository>();
            MapperMock = new Mock<IMapper>();

            BeneficiaryService = new BeneficiaryService(BeneficiaryRepoMock.Object, null, MapperMock.Object);
        }

        public void Dispose()
        {
            // Clean up resources, if needed
        }
    }

}
