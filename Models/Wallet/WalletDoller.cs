using System.ComponentModel.DataAnnotations;

namespace webApi.Models.Wallet
{
    public class WalletDoller
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
