using Microsoft.AspNetCore.Mvc;
using webApi.Services.Repository.Interface;
using webApi.Models.Form;

namespace webApi.Controllers.Wallet
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletDollerController : ControllerBase
    {
        private readonly IWalletDollerRepository _walletDollerRepository;
        private readonly IUserRepository _userRepository;


        public WalletDollerController(IWalletDollerRepository walletDollerRepository, IUserRepository userRepository)
        {
            _walletDollerRepository = walletDollerRepository;
            _userRepository = userRepository;
        }
        
        [Route("incerement/{id}")]
        [HttpPost]
        public async Task<IActionResult> IncerementAmount(int id,[FromBody] AmountForm amountForm)
        {
           var amount = amountForm.Amount;

            if (amount == null || amount == 0) 
                return NotFound("amount is required !");

            var walletDoller = await _walletDollerRepository.IncerementAmountAsync(id, amount);

            if(walletDoller == null) 
                return NotFound($"wallet doller with id {id} not found");

            return Ok(walletDoller);
        }

        [Route("decerement/{id}")]
        [HttpPost]
        public async Task<IActionResult> DecerementAmount(int id,[FromBody] AmountForm amountForm)
        {
            var amount = amountForm.Amount;

            if(amount == null || amount == 0)
                return NotFound("amount is required !");

            var walletDoller = await _walletDollerRepository.GetWalletDollerByIdAsync(id);

            if (walletDoller == null)
                return NotFound($"wallet doller with id {id} not found");

            bool amountValid = await _walletDollerRepository.ValidAmountAsync(id, amount);

            if (!amountValid)
                return BadRequest("The amount in your account is insufficient");

            var walletDollerDecrement = await _walletDollerRepository.DecerementAmountAsync(id, amount);

            if(walletDollerDecrement == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            
            return Ok(walletDollerDecrement);
        }

        [Route("exchange/{userId}")]
        [HttpPost]
        public async Task<IActionResult> ExchangeDollerToRail(int userId, [FromBody] AmountForm amountForm)
        {
            var amount = amountForm.Amount;

            if (amount == null || amount == 0)
                return NotFound("amount is required !");

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound($"wallet doller with userId {userId} not found");

            bool amountValid = await _walletDollerRepository.ValidAmountAsync(user.WalletDoller.Id, amount);

            if (!amountValid)
                return BadRequest("The amount in your account is insufficient");

            var walletDoller = await _walletDollerRepository.ExchangeToRailAsync(userId, amount);

            if(walletDoller == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(walletDoller);
        }
        
      
    }
}
