using webApi.Models.Wallet;
using webApi.Services.Repository.Interface;
using webApi.Context;
using Microsoft.EntityFrameworkCore;

namespace webApi.Services.Repository.Impelement
{
    public class WalletDollerRepository : IWalletDollerRepository
    {
        private readonly SqlServerContext _context;

        public WalletDollerRepository(SqlServerContext context)
        {
            _context = context;
        }
        public async Task<WalletDoller> GetWalletDollerByIdAsync(int id)
        {
            WalletDoller walletDoller = await _context.WalletDollers.SingleOrDefaultAsync(wd => wd.Id == id);
            return walletDoller;
        }

        public async Task<bool> ValidAmountAsync(int id, double amount)
        {
            WalletDoller walletDoller = await GetWalletDollerByIdAsync(id);
            if (walletDoller == null)
                return false;
            if (walletDoller.Amount >= amount)
            {
                return true;
            }
            return false;
        }

        public async Task<WalletDoller> DecerementAmountAsync(int id, double amount)
        {
            WalletDoller walletDoller = await GetWalletDollerByIdAsync(id);
            if (walletDoller == null)
                return null;

            if (!await ValidAmountAsync(id, amount))
                return null;

            walletDoller.Amount -= amount;
            walletDoller.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return walletDoller;
        }

        public async Task<WalletDoller> IncerementAmountAsync(int id, double amount)
        {
            WalletDoller walletDoller = await GetWalletDollerByIdAsync(id);

            if (walletDoller == null)
                return null;
            
            walletDoller.Amount += amount;
            walletDoller.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return walletDoller;
        }

        public async Task<WalletDoller> ExchangeToRailAsync(int userId, double amount)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == userId);

            if (user == null)
                return null;

            if (amount > user.WalletDoller.Amount)
                return null;

            user.WalletDoller.Amount -= amount;
            user.WalletRial.Amount += amount * 30000;
            await _context.SaveChangesAsync();
            return user.WalletDoller;
        }

    }
}
