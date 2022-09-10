using System.ComponentModel.DataAnnotations;

namespace webApi.Models.Form
{
    public class PaymentForm
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        [MinLength(16)]
        [MaxLength(16)]
        public string Source { get; set; }
        [Required]
        [MinLength(16)]
        [MaxLength(16)]
        public string Destination { get; set; }
    }
}
