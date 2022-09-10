using Microsoft.AspNetCore.Mvc;
using webApi.Services.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserRepository _context;
            
        public HomeController (IUserRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _context.GetUserByIdAsync(1);
            if(user == null)
                return NotFound();
            return Ok(user.WalletRial.Id);
        }
    }
}
