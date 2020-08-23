using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moula.DTOs;
using Moula.Services;
using Moula.Services.Interfaces;
using Persistence;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Persistence.Enums.Enumerations;

namespace ProjectTests
{
    public class PaymentRequestTests
    {
        private IUnitOfWork _unitOfWork;
        private IPaymentService _paymentService;

        public PaymentRequestTests()
        {
            InitContext();
            _paymentService = new PaymentService(_unitOfWork);
        }
        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<MoulaContext>();
            var context = new MoulaContext(builder.Options);
            _unitOfWork = new UnitOfWork(context);
        }
        [Fact]
        public async void CreateBadRequest()
        {
            var dto = new PaymentRequestDTO();
            var result = await _paymentService.CreatePaymentRequest(dto);
            Assert.True(result != null && result.Reference == null && result.Reason == PaymentService.DataMissing);
        }
        [Theory]
        [InlineData("TestCustomer", 250)]
        public async void CreateSuccessfulRequest(string custId, double amount)
        {
            var reason = string.Empty;
            var originalBalance = await _paymentService.GetBalance(custId);
            var dto = new PaymentRequestDTO
            {
                CustomerId = custId,
                Amount = amount
            };
            var result = await _paymentService.CreatePaymentRequest(dto);
            var newBalance = await _paymentService.GetBalance(custId);
            Assert.True(result != null && 
                        result.Reference.StartsWith(custId) &&
                        newBalance == (originalBalance - amount) );

        }
        [Theory]
        [InlineData("NCH001", 1000000)]
        public async void CreateInsufficientFundsRequest(string custId, double amount)
        {
            var reason = string.Empty;
            var originalBalance = await _paymentService.GetBalance(custId);
            var dto = new PaymentRequestDTO
            {
                CustomerId = custId,
                Amount = amount
            };
            var result = await _paymentService.CreatePaymentRequest(dto);
            var newBalance = await _paymentService.GetBalance(custId);
            Assert.True(result != null && 
                        result.Reference.StartsWith(custId) && 
                        result.Status == StatusOfRequest.Closed.GetDescription() && 
                        result.Reason == RequestReason.InsufficientFunds.GetDescription() &&
                        newBalance == originalBalance);

        }
        [Theory]
        [InlineData("TestCustomer0001")]
        public async void CancelRequest(string reference)
        {
            var result = await _paymentService.CancelPaymentRequest(reference);
            Assert.True(result != null &&
                        result.Reason == $"Cancelled Payment Request with reference '{reference}'");
                
        }
        [Theory]
        [InlineData("TestCustomerXXXX")]
        public async void CancelNonExistantRequest(string reference)
        {
            var result = await _paymentService.CancelPaymentRequest(reference);
            Assert.True(result != null &&
                        result.Reason == $"Reference '{reference}' not found.");
        }
        [Theory]
        [InlineData("TestCustomer0003")]
        public async void CancelClosedRequest(string reference)
        {
            var result = await _paymentService.CancelPaymentRequest(reference);
            var expectedAnswer = $"Request could not be cancelled, {RequestReason.InvalidCancelRequestClosed.GetDescription()}.";
            Assert.True(result != null && result.Reason == expectedAnswer);
        }
        [Theory]
        [InlineData("TestCustomer0004")]
        public async void ProcessRequest(string reference)
        {
            var result = await _paymentService.ProcessPaymentRequest(reference);
            Assert.True(result != null &&
                        result.Reason == $"Processed Payment Request with reference '{reference}'");
        }
        [Theory]
        [InlineData("TestCustomer0002")]
        public async void CancelProcessedRequest(string reference)
        {
            var result = await _paymentService.CancelPaymentRequest(reference);
            var expectedAnswer = $"Request could not be cancelled, {RequestReason.InvalidCancelRequestProcessed.GetDescription()}.";
            Assert.True(result != null && result.Reason == expectedAnswer);
        }
        [Theory]
        [InlineData("TestCustomerXXXX")]
        public async void ProcessNonExistantRequest(string reference)
        {
            var result = await _paymentService.ProcessPaymentRequest(reference);
            Assert.True(result != null &&
                        result.Reason == $"Reference '{reference}' not found.");
        }
        [Theory]
        [InlineData("TestCustomer0003")]
        public async void ProcessClosedRequest(string reference)
        {
            var result = await _paymentService.ProcessPaymentRequest(reference);
            var expectedAnswer = $"Request could not be processed, {RequestReason.InvalidProcessRequestClosed.GetDescription()}.";
            Assert.True(result != null && result.Reason == expectedAnswer);
        }
        [Theory]
        [InlineData("TestCustomer0002")]
        public async void ProcessProcessedRequest(string reference)
        {
            var result = await _paymentService.ProcessPaymentRequest(reference);
            var expectedAnswer = $"Request could not be processed, {RequestReason.InvalidProcessRequestProcessed.GetDescription()}.";
            Assert.True(result != null && result.Reason == expectedAnswer);
        }

        [Theory]
        [InlineData("TestCustomerBalance")]
        public  async void TestPaymentsAndBalance(string custId)
        {
            var result = await _paymentService.GetPaymentRequestsAndBalance(custId);
            int countProcessed = result.PaymentRequests.Where(p => p.Status == StatusOfRequest.Processed.GetDescription()).Count();
            int countPending = result.PaymentRequests.Where(p => p.Status == StatusOfRequest.Pending.GetDescription()).Count();
            int countClosed = result.PaymentRequests.Where(p => p.Status == StatusOfRequest.Closed.GetDescription()).Count();
            Assert.True(result != null && 
                        result.Balance == 300 && 
                        countProcessed == 1 &&
                        countPending == 2 &&
                        countClosed == 3);
        }
    }
}
