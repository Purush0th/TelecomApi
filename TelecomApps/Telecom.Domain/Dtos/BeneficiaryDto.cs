using System.ComponentModel.DataAnnotations;

namespace Telecom.Domain.ViewModels
{
    public class BeneficiaryDto
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public required string NickName { get; set; }

        public required string PhoneNumber { get; set; }

        public int TopUpTotal { get; set; }

        public int LastRechargeAmount { get; set; }

        public DateTime? LastRechargeDate { get; set; } = default(DateTime?);
    }
}
