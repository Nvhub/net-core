using webApi.Models.Wallet;

namespace webApi.Services.Repository.Interface
{
    public interface IWalletRialRepository
    {
        public Task<WalletRial> GetWalletRialByIdAsync(int id);
        public Task<WalletRial> IncerementAmountAsync(int id, double amount);
        public Task<WalletRial> DecerementAmountAsync(int id, double amount);
        public Task<bool> ValidAmountAsync(int id, double amount);
        public Task<WalletRial> ExchangeToDollerAsync(int userId, double amount);
    }
}
