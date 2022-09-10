using System.ComponentModel.DataAnnotations;

namespace webApi.Models.Form
{
    public class UserForm
    {
        [Required]
        [MaxLength(35)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        public string AccountNumber { get; set; }
        [Required]
        [MaxLength(12)]
        public string PhoneNumber{ get; set; }
        public double AmountDoller { get; set; }
        public double AmountRial { get; set; }
    }
}
