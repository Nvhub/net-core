using webApi.Models.Wallet;
using webApi.Models;

namespace webApi.Services.Repository.Interface
{
    public interface IPaymentRepository
    {
        public Task<User> GetUserByAccountNumberAsync(string accountNumber);
        public Task<List<WalletDoller>> MoneyTransferDollerAsync(string sourceAcountNumber, string destaintionAccountNumber, double amount);
        public Task<List<WalletRial>> MoneyTransferRialAsync(string sourceAcountNumber, string destaintionAccountNumber, double amount);
    }
}
