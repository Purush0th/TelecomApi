

namespace Telecom.Domain.Configuration
{
    public class AppSettings
    {
        public int MaxBeneficiaryPerAccount { get; set; }
        public int MaxTopUpPerMonthForAccount { get; set; }
        public int MaxTopUpPerMonthPerVerifiedUser { get; set; }
        public int MaxTopUpPerMonthPerUnVerifiedUser { get; set; }
        public int[] AllowedRechargePlans { get; set; }

    }
}
