using System.ComponentModel.DataAnnotations;

namespace webApi.Models
{
    public class PhoneBook
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "phone_number")]
        [MinLength(12)]
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
