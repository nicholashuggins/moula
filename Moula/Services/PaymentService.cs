using AutoMapper;
using Moula.DTOs;
using Moula.Services.Interfaces;
using Persistence;
using Persistence.Models;
using Persistence.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Persistence.Enums.Enumerations;

namespace Moula.Services
{
    public class PaymentService : IPaymentService
    {
        private IMapper _mapper;
        private IUnitOfWork _uow;

        public const string DataMissing = "Input data missing, ensure a valid customerid and amount are supplied";
        public PaymentService(IUnitOfWork uow )
        {
            _uow = uow;
            CreateMapping();
        }
        /// <summary>
        /// Creates a payment request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// On Success returns a PaymentRequest with the request reference
        /// On failure returns a PaymentRequest with no reference and a reason
        /// </returns>
        public async Task<PaymentRequestDTO> CreatePaymentRequest(PaymentRequestDTO request)
        {
            //Validate inputs
            if (request == null || request.Amount < 0 || string.IsNullOrEmpty(request.CustomerId))
            {
                request.Reason = DataMissing;
                request.Reference = null;
                return request;
            }

            //Set defaults
            var paymentStatus = (int)StatusOfRequest.Pending;
            var paymentReason = (int)RequestReason.ValidRequest;

            //If low funds the payment request should be created and closed
            var currentBalance = await GetBalance(request.CustomerId);
            if (request.Amount > currentBalance)
            {
                paymentReason = (int)RequestReason.InsufficientFunds;
                paymentStatus = (int)StatusOfRequest.Closed;
            }

            //Create the payment request
            var result = _uow.PaymentRequests.Create(new PaymentRequest
            {
                CustomerId =request.CustomerId,
                Amount = request.Amount,
                RequestStatusId = paymentStatus,
                StatusReasonId = paymentReason,
            });
            await _uow.Complete();

            // Return the created data
            var data = await GetPaymentRequest(result.Id);
            
            return data;
        }
        /// <summary>
        /// Cancels an existing payment request
        /// </summary>
        /// <param name="reference"></param>
        /// <returns>
        /// On Success returns a Reasone and a reference in a PaymentRequestDTO
        /// On failure returns a Reasone in the PaymentRequestDTO indicating why the cancellation failed
        /// </returns>
        public async Task<PaymentRequestDTO> CancelPaymentRequest(string reference)
        {
            //Check Request exists
            var data = await GetPaymentRequest(reference);
            var dto = new PaymentRequestDTO();

            if (data == null)
            {
                dto.Reason = $"Reference '{reference}' not found.";
                return dto;
            }
            //Check request is not Processed or Closed
            if (data.RequestStatusId == (int)StatusOfRequest.Processed)
            {
                dto.Reason = $"Request could not be cancelled, {RequestReason.InvalidCancelRequestProcessed.GetDescription()}.";
                return dto;
            }

            if (data.RequestStatusId == (int)StatusOfRequest.Closed)
            {
                dto.Reason = $"Request could not be cancelled, {RequestReason.InvalidCancelRequestClosed.GetDescription()}.";
                return dto;
            }

            //Update payment request
            var dbRequest = new PaymentRequest
            {
                Id = data.Id,
                CustomerId = data.CustomerId,
                CreatedOn = data.CreatedOn,
                Amount = data.Amount,
                RequestStatusId = (int)StatusOfRequest.Closed,
                StatusReasonId = data.StatusReasonId,
                Reference = data.Reference
            };
            _uow.PaymentRequests.Update(dbRequest);
            await _uow.Complete();
            dto.Reference = data.Reference;
            dto.Status = StatusOfRequest.Closed.GetDescription();
            dto.Reason = $"Cancelled Payment Request with reference '{data.Reference}'";
            return dto;
        }
        /// <summary>
        /// Processes  an existing payment request
        /// </summary>
        /// <param name="reference"></param>
        /// <returns>
        /// On Success returns a Reason and a reference in a PaymentRequestDTO
        /// On failure returns a Reason in the PaymentRequestDTO indicating why the process failed
        /// </returns>
        public async Task<PaymentRequestDTO> ProcessPaymentRequest(string reference)
        {
            //Check Request exists
            var data = await GetPaymentRequest(reference);
            var dto = new PaymentRequestDTO();
            if (data == null)
            {
                dto.Reason = $"Reference '{reference}' not found.";
                return dto;
            }
            //Check request is not Processed or Closed
            if (data.RequestStatusId == (int)StatusOfRequest.Processed)
            {
                dto.Reason = $"Request could not be processed, {RequestReason.InvalidProcessRequestProcessed.GetDescription()}.";
                return dto;
            }

            if (data.RequestStatusId == (int)StatusOfRequest.Closed)
            {
                dto.Reason = $"Request could not be processed, {RequestReason.InvalidProcessRequestClosed.GetDescription()}.";
                return dto;
            }

            //Update payment request
            var dbRequest = new PaymentRequest
            {
                Id = data.Id,
                CustomerId = data.CustomerId,
                CreatedOn = data.CreatedOn,
                Amount = data.Amount,
                RequestStatusId = (int)StatusOfRequest.Processed,
                StatusReasonId = data.StatusReasonId,
                Reference = data.Reference
            };
            _uow.PaymentRequests.Update(dbRequest);
            //Create the transaction
            var dbTransaction = new Transaction
            {
                Id = data.Id,
                CustomerId = data.CustomerId,
                CreatedOn = data.CreatedOn,
                Amount = data.Amount * -1,
            };
            _uow.Transactions.Add(dbTransaction);
            await _uow.Complete();
            dto.Reference = data.Reference;
            dto.Reason = $"Processed Payment Request with reference '{data.Reference}'";
            dto.Status = StatusOfRequest.Processed.GetDescription();
            return dto;
        }

        public async Task<double> GetBalance(string customerId)
        {
            var balance = await _uow.Transactions.GetCustomerBalance(customerId);
            var pendingItemsTotal = await _uow.PaymentRequests.GetCustomerPendingBalance(customerId);
            return balance - pendingItemsTotal;
        }

        public async Task<PaymentRequestsAndBalanceDTO> GetPaymentRequestsAndBalance(string customerId)
        {
            var result = new PaymentRequestsAndBalanceDTO();
            var payments = await GetPaymentRequests(customerId);
            result.PaymentRequests = payments.ToList();
            result.Balance = await GetBalance(customerId);
            return result;
        }

        private async Task<PaymentRequestDTO> GetPaymentRequest(Guid requestId)
        {
            var queryResult = await _uow.PaymentRequests.GetPaymentRequest(requestId);
            var result = _mapper.Map<PaymentRequestDTO>(queryResult);
            return result;
        }
        private async Task<PaymentRequestView> GetPaymentRequest(string reference)
        {
            var queryResult = await _uow.PaymentRequests.GetPaymentRequest(reference);
            return queryResult;
        }
        public async Task<ICollection<PaymentRequestDTO>> GetPaymentRequests(string customerId)
        {
            var queryResult = await _uow.PaymentRequests.GetPaymentRequests(customerId);
            var result = new List<PaymentRequestDTO>();
            foreach (var item in queryResult)
            {
                result.Add(_mapper.Map<PaymentRequestDTO>(item));
            }
            return result;
        }

        private void CreateMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PaymentRequestView, PaymentRequestDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.Reference))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.StatusReason))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RequestStatus));
            });
            _mapper = new Mapper(config);
        }
    }
}
