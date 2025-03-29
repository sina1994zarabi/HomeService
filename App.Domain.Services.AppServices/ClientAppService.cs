using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ClientDto;
using App.Domain.Core.DTOs.ReviewDto;
using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using App.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AppServices
{
    public class ClientAppService : IClientAppService
    {

        private readonly IClientService _clientService;
        private readonly IServiceService _serviceService;
        private readonly IServiceOfferingService _serviceOfferingService;
        private readonly IServiceRequestAppService _requestAppService;
        private readonly IReviewService _reviewService;

        public ClientAppService(IClientService clientService,
                                IServiceService serviceService,
                                IServiceOfferingService serviceOfferingService,
                                IServiceRequestAppService requestAppService,
                                IReviewService reviewService)
        {
            _clientService = clientService;
            _serviceService = serviceService;
            _serviceOfferingService = serviceOfferingService;
            _requestAppService = requestAppService;
            _reviewService = reviewService;
        }


        public async Task Create(CreateClientDto createClientDto, CancellationToken cancellationToken)
        {
            await _clientService.Add(createClientDto, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _clientService.Delete(id, cancellationToken);
        }

        public async Task<List<Client>> GetAll(CancellationToken cancellationToken)
        {
            return await _clientService.GetAll(cancellationToken);
        }

        public async Task<Client> GetById(int id, CancellationToken cancellationToken)
        {
            return await _clientService.Get(id, cancellationToken);
        }

        public async Task<Client> GetClientByUserId(int userId, CancellationToken cancellationToken)
        {
           var clients = await _clientService.GetAll(cancellationToken);
           return (Client)clients.Where(c => c.AppUserId == userId).Single();
        }

        public async Task<Client> GetClientInfo(int id, CancellationToken cancellationToken)
        {
            return await _clientService.GetClientInfo(id, cancellationToken);
        }

        public async Task<List<ServiceRequest>> GetServiceRequests(int clientId,CancellationToken cancellationToken)
        {
            var allServiceRequests = await _requestAppService.GetAll(cancellationToken);
            return allServiceRequests.Where(x => x.ClientId == clientId).ToList();
        }

        public async Task<List<ServiceOffering>> GetServicesOffering(int serviceRequestId, CancellationToken cancellationToken)
        {
            var result = await _serviceOfferingService.GetAll(cancellationToken);
            return result.Where(x => x.ServiceRequestId == serviceRequestId).ToList();
        }

        public async Task ProcessPayment(int serviceRequestId,CancellationToken cancellationToken)
        {
            var request = await _requestAppService.GetById(serviceRequestId,cancellationToken);
            if (request == null)
            {
                throw new InvalidOperationException("سرویس مورد نظر یافت نشد.");
            }
            var client = request.Client;
            decimal servicePrice = request.Service.Price;
            if (client == null || client.AppUser == null)
            {
                throw new InvalidOperationException("مشتری مورد نظر یافت نشد.");
            }
            if (client.AppUser.AccountBalance < servicePrice)
            {
                throw new InvalidOperationException("موجودی کافی نیست.");
            }
            await _clientService.MakeTransaction(client.Id,servicePrice,cancellationToken);
        }

        public async Task SubmitReview(CreateReviewDto createReviewDto, CancellationToken cancellationToken)
        {
            await _reviewService.Add(createReviewDto, cancellationToken);
        }

        public async Task SubmitServiceRequest(CreateServiceRequestDto createServiceRequestDto, CancellationToken cancellationToken)
        {
            await _requestAppService.Create(createServiceRequestDto, cancellationToken);
        }

        public async Task Update(UpdateClientprofileDto updateClientDto, CancellationToken cancellation,IFormFile Image)
        {
            await _clientService.Update(updateClientDto, cancellation,Image);
        }

        public async Task UpdateBalance(int clientId,decimal balance, CancellationToken cancellationToken)
        {
            await _clientService.UpdateBalance(clientId,balance,cancellationToken);
        }
    }
}
