using webApi.Context;
using webApi.Models;
using webApi.Models.Wallet;
using webApi.Models.Form;
using webApi.Services.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace webApi.Services.Repository.Impelement
{
    public class UserRepository : IUserRepository
    {

        private SqlServerContext _context;

        public UserRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = await _context.Users
                .Include(user => user.PhoneBook)
                .Include(user => user.WalletDoller)
                .Include(user => user.WalletRial)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            User user = await _context.Users
                .Include(u => u.PhoneBook)
                .Include(u => u.WalletDoller)
                .Include(u => u.WalletRial)
                .SingleOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> SaveUserAsync(UserForm userForm)
        {
            DateTime now = DateTime.Now;
            User user = new User();
            // user information //
            user.FirstName = userForm.FirstName;
            user.LastName = userForm.LastName;
            user.AccountNumber = userForm.AccountNumber;
            user.CreatedAt = now;
            // phone book information //
            PhoneBook phoneBook = new PhoneBook();

            phoneBook.PhoneNumber = userForm.PhoneNumber;
            phoneBook.CreatedAt = now;
            await _context.PhoneBooks.AddAsync(phoneBook);
            user.PhoneBook = phoneBook;

            // wallet information //
            WalletDoller walletDoller = new WalletDoller();
            WalletRial walletRial = new WalletRial();

            walletDoller.Amount = userForm.AmountDoller == null ? 0 : userForm.AmountDoller;
            walletDoller.CreatedAt = now;
            walletRial.Amount = userForm.AmountRial == null ? 0 : userForm.AmountRial;
            walletRial.CreatedAt = now;
            await _context.WalletDollers.AddAsync(walletDoller);
            await _context.WalletRials.AddAsync(walletRial);
            user.WalletDoller = walletDoller;
            user.WalletRial = walletRial;

            await _context.SaveChangesAsync();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<User> DeleteUserByIdAsync(int id)
        {
            User findUser = await GetUserByIdAsync(id);
            if(findUser == null)
            {
                return null;
            }
            User showUser =  _context.Users.SingleOrDefault(user => user.Id == id);

            PhoneBook phoneBook = await _context.PhoneBooks.SingleOrDefaultAsync(p => p.Id == findUser.PhoneBook.Id);

            //_context.PhoneBooks.Attach(phoneBook);
            _context.PhoneBooks.Remove(findUser.PhoneBook);
            _context.WalletDollers.Remove(findUser.WalletDoller);
            _context.WalletRials.Remove(findUser.WalletRial);
            _context.Users.Remove(findUser);
            await _context.SaveChangesAsync();

            return findUser;
        }
    }
}
