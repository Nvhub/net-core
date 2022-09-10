using System.ComponentModel.DataAnnotations;

namespace webApi.Models.Form
{
    public class AmountForm
    {
        [Required]
        public double Amount { get; set; }
    }
}
