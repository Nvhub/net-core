using System.ComponentModel.DataAnnotations;
using webApi.Models.Wallet;

namespace webApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        public string AccountNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relation Tables

        public PhoneBook PhoneBook { get; set; }
        public WalletDoller WalletDoller { get; set; }
        public WalletRial WalletRial { get; set; }
    }
}
