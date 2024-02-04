using System.ComponentModel.DataAnnotations.Schema;

namespace Telecom.Domain.Models
{
    public class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsVerified { get; set; }
    }
}