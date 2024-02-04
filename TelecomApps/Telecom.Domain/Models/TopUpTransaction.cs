using System.ComponentModel.DataAnnotations.Schema;

namespace Telecom.Domain.Models
{
    public class TopUpTransaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int BeneficiaryId { get; set; }

        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public string BankTransactionId { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorDetails { get; set; }

    }
}
