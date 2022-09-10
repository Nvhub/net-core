using webApi.Models.Wallet;

namespace webApi.Services.Repository.Interface
{
    public interface IWalletDollerRepository
    {
        public Task<WalletDoller> GetWalletDollerByIdAsync(int id);
        public Task<WalletDoller> IncerementAmountAsync(int id, double amount);
        public Task<WalletDoller> DecerementAmountAsync(int id, double amount);
        public Task<bool> ValidAmountAsync(int id, double amount);
        public Task<WalletDoller> ExchangeToRailAsync(int userId, double amount);
    }
}
