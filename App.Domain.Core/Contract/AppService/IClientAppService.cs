using App.Domain.Core.DTOs.ClientDto;
using App.Domain.Core.DTOs.ReviewDto;
using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using App.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AppService
{
    public interface IClientAppService
    {
        Task Create(CreateClientDto createClientDto,CancellationToken cancellationToken);
        Task<Client> GetById(int id, CancellationToken cancellationToken);
        Task<Client> GetClientByUserId(int userId, CancellationToken cancellationToken);
        Task<List<Client>> GetAll(CancellationToken cancellationToken);
        Task<Client> GetClientInfo(int id, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Update(UpdateClientprofileDto updateClientDto, CancellationToken cancellationToken,IFormFile Image);
        Task<List<ServiceRequest>> GetServiceRequests(int clientId,CancellationToken cancellationToken);
        Task SubmitServiceRequest(CreateServiceRequestDto createServiceRequestDto,CancellationToken cancellationToken);
        Task<List<ServiceOffering>> GetServicesOffering(int serviceRequestId,CancellationToken cancellationToken);
        Task ProcessPayment(int serviceRequestId, CancellationToken cancellationToken);
        Task UpdateBalance(int userId,decimal balance,CancellationToken cancellationToken);
        Task SubmitReview(CreateReviewDto createReviewDto,CancellationToken cancellationToken);
    }
}
