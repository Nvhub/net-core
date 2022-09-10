using webApi.Models.Wallet;
using webApi.Services.Repository.Interface;
using webApi.Context;
using Microsoft.EntityFrameworkCore;

namespace webApi.Services.Repository.Impelement
{
    public class WalleRialRepository : IWalletRialRepository
    {
        private readonly SqlServerContext _context;

        public WalleRialRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<WalletRial> GetWalletRialByIdAsync(int id)
        {
            
            WalletRial walletRial = await _context.WalletRials.SingleOrDefaultAsync(wr => wr.Id == id);
            return walletRial;
        }

        public async Task<bool> ValidAmountAsync(int id, double amount)
        {
            WalletRial walletRial= await GetWalletRialByIdAsync(id);
            if (walletRial.Amount >= amount)
            {
                return true;
            }
            return false;
        }

        public async Task<WalletRial> DecerementAmountAsync(int id, double amount)
        {
            WalletRial walletRial = await GetWalletRialByIdAsync(id);
            if (walletRial == null)
                return null;

            if (!await ValidAmountAsync(id, amount))
                return null;

            walletRial.Amount -= amount;
            walletRial.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return walletRial;
        }

        public async Task<WalletRial> IncerementAmountAsync(int id, double amount)
        {
            WalletRial walletRial = await GetWalletRialByIdAsync(id);

            if (walletRial == null)
                return null;

            walletRial.Amount += amount;
            walletRial.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return walletRial;
        }

        public async Task<WalletRial> ExchangeToDollerAsync(int userId, double amount)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == userId);

            if (user == null)
                return null;

            if (amount > user.WalletRial.Amount)
                return null;

            user.WalletRial.Amount -= amount;
            user.WalletDoller.Amount += amount / 300000;
            await _context.SaveChangesAsync();
            return user.WalletRial;
        }
    }
}
