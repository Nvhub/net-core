using webApi.Models;
using webApi.Models.Form;

namespace webApi.Services.Repository.Interface
{
    public interface IUserRepository
    {
        //public Task<List<User>> users();
        //public Task<User> user(int id);
        //public Task<User> userSave(UserForm userForm);

        public Task<List<User>> GetAllUsersAsync();
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> SaveUserAsync(UserForm userForm);
        public Task<User> DeleteUserByIdAsync(int id);
    }
}
