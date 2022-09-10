using Microsoft.AspNetCore.Mvc;
using webApi.Services.Repository.Interface;
using webApi.Models;
using webApi.Models.Form;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<User> users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Add(UserForm userForm)
        {
            User newUser = await _userRepository.SaveUserAsync(userForm);
            if(newUser != null)
            {
                return Created("",newUser);
            }
            return BadRequest();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            User getUser = await _userRepository.GetUserByIdAsync(id);
            if(getUser == null)
            {
                return NotFound("user not found");
            }
            return Ok(getUser);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await _userRepository.DeleteUserByIdAsync(id);
            if(user != null)
            {
                return Ok(user);
            }
            return NotFound($"user not found by id {id}");
        }
    }
}
