using Microsoft.AspNetCore.Mvc;
using webApi.Models.Form;
using webApi.Services.Repository.Interface;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IWalletRialRepository _walletRialRepository;
        private readonly IWalletDollerRepository _walletDollerRepository;

        public PaymentController(IPaymentRepository paymentRepository, IWalletRialRepository walletRialRepository, IWalletDollerRepository walletDollerRepository)
        {
            _paymentRepository = paymentRepository;
            _walletRialRepository = walletRialRepository;
            _walletDollerRepository = walletDollerRepository;
        }

        [Route("transfer/rial")]
        [HttpPost]
        public async Task<IActionResult> TransferRial([FromBody] PaymentForm paymentForm)
        {

            var amount = paymentForm.Amount;
            var source = paymentForm.Source;
            var destination = paymentForm.Destination;

            if (amount == null || amount == 0)
                return BadRequest("amount is required !");

            if (source == null || source.Length != 16)
                return BadRequest("source account number is not valid");

            if (destination == null || destination.Length != 16)
                return BadRequest("destination account number is not valid");

            if (destination == source)
                return BadRequest("not transfer for one account");

            var soruceWallet = await _paymentRepository.GetUserByAccountNumberAsync(source);

            if (soruceWallet == null)
                return NotFound($"source wallet by account number {source} not found");

            var destinationWallet = await _paymentRepository.GetUserByAccountNumberAsync(destination);

            if(destinationWallet == null)
                return NotFound($"destination wallet by account number {destination} not found");

            if (!await _walletRialRepository.ValidAmountAsync(soruceWallet.WalletRial.Id, amount))
                return BadRequest("The amount in your account is insufficient");

            var wallets = await _paymentRepository.MoneyTransferRialAsync(source, destination, amount);

            if (wallets == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(wallets);
        }
        [Route("transfer/doller")]
        [HttpPost]
        public async Task<IActionResult> TransferDoller([FromBody] PaymentForm paymentForm)
        {

            var amount = paymentForm.Amount;
            var source = paymentForm.Source;
            var destination = paymentForm.Destination;

            if (amount == null)
                return BadRequest("amount is required !");

            if (source == null || source.Length != 16)
                return BadRequest("source account number is not valid");

            if (destination == null || destination.Length != 16)
                return BadRequest("destination account number is not valid");

            if (destination == source)
                return BadRequest("not transfer for one account");

            var soruceWallet = await _paymentRepository.GetUserByAccountNumberAsync(source);

            if (soruceWallet == null)
                return NotFound($"source wallet by account number {source} not found");

            var destinationWallet = await _paymentRepository.GetUserByAccountNumberAsync(destination);

            if (destinationWallet == null)
                return NotFound($"destination wallet by account number {destination} not found");

            if (!await _walletDollerRepository.ValidAmountAsync(soruceWallet.WalletDoller.Id, amount + ((amount * 9) / 100)))
                return BadRequest("The amount in your account is insufficient");

            var wallets = await _paymentRepository.MoneyTransferDollerAsync(source, destination, amount);

            if (wallets == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return Ok(wallets);
        }
    }
}
