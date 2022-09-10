using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Services.Repository.Interface;
using webApi.Models.Form;

namespace webApi.Controllers.Wallet
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletRialController : ControllerBase
    {
        private readonly IWalletRialRepository _walletRialRepository;
        private readonly IUserRepository _userRepository;

        public WalletRialController(IWalletRialRepository walletRialRepository, IUserRepository userRepository)
        {
            _walletRialRepository = walletRialRepository;
            _userRepository = userRepository;
        }

        [Route("incerement/{id}")]
        [HttpPost]
        public async Task<IActionResult> IncerementAmount(int id, [FromBody] AmountForm amountForm)
        {
            var amount = amountForm.Amount;

            if (amount == null || amount == 0)
                return BadRequest("amount is required !");

            var walletDoller = await _walletRialRepository.IncerementAmountAsync(id, amount);

            if (walletDoller == null)
                return NotFound($"wallet rial with id {id} not found");

            return Ok(walletDoller);
        }

        [Route("decerement/{id}")]
        [HttpPost]
        public async Task<IActionResult> DecerementAmount(int id, [FromBody] AmountForm amountForm)
        {
            var amount = amountForm.Amount;

            if (amount == null || amount == 0)
                return BadRequest("amount is required !");

            var walletDoller = await _walletRialRepository.GetWalletRialByIdAsync(id);

            if (walletDoller == null)
                return NotFound($"wallet rial with id {id} not found");

            bool amountValid = await _walletRialRepository.ValidAmountAsync(id, amount);

            if (!amountValid)
                return BadRequest("The amount in your account is insufficient");

            var walletDollerDecrement = await _walletRialRepository.DecerementAmountAsync(id, amount);

            if (walletDollerDecrement == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(walletDollerDecrement);
        }

        [Route("exchange/{userId}")]
        [HttpPost]
        public async Task<IActionResult> ExchangeRialToDoller(int userId, [FromBody] AmountForm amountForm)
        {
            var amount = amountForm.Amount;

            if (amount == null || amount == 0)
                return BadRequest("amount is required !");

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound($"wallet rial with userId {userId} not found");

            bool amountValid = await _walletRialRepository.ValidAmountAsync(user.WalletDoller.Id, amount);

            if (!amountValid)
                return BadRequest("The amount in your account is insufficient");

            var walletDoller = await _walletRialRepository.ExchangeToDollerAsync(userId, amount);

            if (walletDoller == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(walletDoller);
        }

    }
}
