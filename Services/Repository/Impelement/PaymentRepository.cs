using webApi.Models.Wallet;
using webApi.Models;
using webApi.Services.Repository.Interface;
using webApi.Context;
using Microsoft.EntityFrameworkCore;

namespace webApi.Services.Repository.Impelement
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly SqlServerContext _context;
        private readonly IWalletRialRepository _walletRialRepository;
        private readonly IWalletDollerRepository _walletDollerRepository;
        private readonly IUserRepository _userRepository;

        public PaymentRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByAccountNumberAsync(string accountNumber)
        {   
            if(accountNumber == null || accountNumber.Length != 16)
            {
                return null;
            }
            var user = await _context.Users
                .Include(user => user.WalletDoller)
                .Include(user => user.WalletRial)
                .SingleOrDefaultAsync(user => user.AccountNumber == accountNumber);
            if(user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<List<WalletDoller>> MoneyTransferDollerAsync(string sourceAcountNumber, string destaintionAccountNumber, double amount)
        {
            if (amount == null || sourceAcountNumber == null || sourceAcountNumber.Length != 16 || destaintionAccountNumber == null || destaintionAccountNumber.Length != 16)
            {
                return null;
            }

            var source = await GetUserByAccountNumberAsync(sourceAcountNumber);
            var destaintion = await GetUserByAccountNumberAsync(destaintionAccountNumber);

            if (source == null || destaintion == null)
            {
                return null;
            }

            //var isAmountValid = await _walletRialRepository.ValidAmountAsync(source.WalletRial.Id, amount);

            //if (!isAmountValid)
            //{
            //    return null;
            //}

            source.WalletDoller.Amount -= amount + ((amount * 9) / 100);
            source.WalletDoller.UpdatedAt = DateTime.Now;

            destaintion.WalletDoller.Amount += amount;
            destaintion.WalletDoller.UpdatedAt = DateTime.Now;

            var wallets = new List<WalletDoller>();
            wallets.Add(source.WalletDoller);
            wallets.Add(destaintion.WalletDoller);

            await _context.SaveChangesAsync();


            return wallets;
        }

        public async Task<List<WalletRial>> MoneyTransferRialAsync(string sourceAcountNumber, string destaintionAccountNumber, double amount)
        {
            if(amount == null || sourceAcountNumber == null || sourceAcountNumber.Length != 16 || destaintionAccountNumber == null || destaintionAccountNumber.Length != 16)
            {
                return null;
            }

            var source = await GetUserByAccountNumberAsync(sourceAcountNumber);
            var destaintion = await GetUserByAccountNumberAsync(destaintionAccountNumber);

            if(source == null || destaintion == null)
            {
                return null;
            }

            //var isAmountValid = await _walletRialRepository.ValidAmountAsync(source.WalletRial.Id, amount);

            //if (!isAmountValid)
            //{
            //    return null;
            //}

            source.WalletRial.Amount -= amount;
            source.WalletRial.UpdatedAt = DateTime.Now;

            destaintion.WalletRial.Amount += amount;
            destaintion.WalletRial.UpdatedAt = DateTime.Now;

            var wallets = new List<WalletRial>();
            wallets.Add(source.WalletRial);
            wallets.Add(destaintion.WalletRial);

            await _context.SaveChangesAsync();


            return wallets;
        }
    }
}
