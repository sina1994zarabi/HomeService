using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ReviewDto;
using App.Domain.Core.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AppServices
{
    public class ReviewAppService : IReviewAppService
    {
        private readonly IReviewService _reviewService;

        public ReviewAppService(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

		public async Task ConfirmOrRejectReview(int id, CancellationToken cancellation)
		{
			await _reviewService.ConfirmOrRejectReview(id,cancellation);
		}

		public async Task Create(CreateReviewDto createReviewDto, CancellationToken cancellation)
        {
            await _reviewService.Add(createReviewDto,cancellation);
        }

        public async Task Delete(int id, CancellationToken cancellation)
        {
            await _reviewService.Delete(id,cancellation);
        }

        public async Task<List<Review>> GetAll(CancellationToken cancellation)
        {
            return await _reviewService.GetAll(cancellation);
        }

        public async Task<List<Review>> GetByExpertId(int id, CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetAll(cancellationToken);
            return reviews.Where(x => x.ServiceOffering.Expert.Id == id).ToList();
        }

        public async Task<Review> GetById(int id, CancellationToken cancellation)
        {
            return await _reviewService.Get(id,cancellation);
        }

        public async Task Update(Review review, CancellationToken cancellation)
        {
            await _reviewService.Update(review, cancellation);
        }
    }
}
