using App.Domain.Core.DTOs.ReviewDto;
using App.Domain.Core.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AppService
{
    public interface IReviewAppService
    {
        Task Create(CreateReviewDto createReviewDto, CancellationToken cancellation);
        Task<Review> GetById(int id,CancellationToken cancellation);
        Task<List<Review>> GetByExpertId(int id,CancellationToken cancellationToken);
        Task<List<Review>> GetAll(CancellationToken cancellation);
        Task Delete(int id, CancellationToken cancellation);
        Task Update(Review reiew, CancellationToken cancellation);
        Task ConfirmOrRejectReview(int id, CancellationToken cancellation);
    }
}
