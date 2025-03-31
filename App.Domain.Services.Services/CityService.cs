using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Services
{
    public class CityService : ICityService
    {

        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task Add(City city, CancellationToken cancellationToken)
        {
            await _cityRepository.Add(city, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _cityRepository.Delete(id, cancellationToken);
        }

        public async Task<City> Get(int id, CancellationToken cancellationToken)
        {
            return await _cityRepository.Get(id, cancellationToken);
        }

        public async Task<List<City>> GetAll(CancellationToken cancellationToken)
        {
            return await _cityRepository.GetAll(cancellationToken);
        }

        public async Task Update(City city, CancellationToken cancellationToken)
        {
            await _cityRepository.Update(city, cancellationToken);
        }
    }
}
