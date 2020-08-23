using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Moula.DTOs;
using Moula.Services.Interfaces;

namespace Moula.Controllers
{

    [ApiController]
    [Route("api/paymentrequest/")]
    public class PaymentRequestController : ControllerBase
    {
        private IPaymentService _paymentService;
        public PaymentRequestController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] PaymentRequestDTO request)
        {
            try
            {
                var result = await _paymentService.CreatePaymentRequest(request);
                if (string.IsNullOrEmpty(result.Reference))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error on PaymentRequestController.Create: {ex}");
                return BadRequest(ex);
            }
        }
        [HttpPatch]
        [Route("cancel")]
        public async Task<IActionResult> Cancel([FromBody] string paymentReference)
        {
            try 
            {
                var result = await _paymentService.CancelPaymentRequest(paymentReference);
                if (string.IsNullOrEmpty(result.Reference))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error on PaymentRequestController.Cancel: {ex}");
                return BadRequest(ex);
            }
        }
        [HttpPatch]
        [Route("process")]
        public async Task<IActionResult> Process([FromBody] string paymentReference)
        {
            try
            {
                var result = await _paymentService.ProcessPaymentRequest(paymentReference);
                if (string.IsNullOrEmpty(result.Reference))
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error on PaymentRequestController.Process: {ex}");
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("balance")]
        public async Task<IActionResult> Balance([FromBody] string customerId)
        {
            try
            {
                var result = await _paymentService.GetPaymentRequestsAndBalance(customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error on PaymentRequestController.Balance: {ex}");
                return BadRequest(ex);
            }
        }
    }
}