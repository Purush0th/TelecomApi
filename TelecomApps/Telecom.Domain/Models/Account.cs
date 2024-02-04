using System.ComponentModel.DataAnnotations.Schema;

namespace Telecom.Domain.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TotalTopUpCredit { get; set; }

        public int AvailableTopUpCredit { get; set; }

        public DateTime? LastRechargeDate { get; set; }


        // Navigational Properties
        public virtual int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}
