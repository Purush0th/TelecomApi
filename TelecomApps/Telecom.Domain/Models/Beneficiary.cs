using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Telecom.Domain.Models
{
    public class Beneficiary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        public required string NickName { get; set; }

        public required string PhoneNumber { get; set; }

        public int TopUpTotal { get; set; }

        public int LastRechargeAmount { get; set; }

        public DateTime? LastRechargeDate { get; set; }


        public virtual int AccountId { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }

    }
}