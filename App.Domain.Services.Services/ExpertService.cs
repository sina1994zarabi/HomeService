﻿using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ExpertDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Services
{
    public class ExpertService : IExpertService
    {
        private readonly IExpertRepository _expertRepository;


        public ExpertService(IExpertRepository expertRepository)
        {
            _expertRepository = expertRepository;
        }

        public async Task Add(CreateExpertDto expert, CancellationToken cancellation)
        {
            await _expertRepository.Add(expert, cancellation);
        }

        public async Task ChangeStatus(int id, CancellationToken cancellation)
        {
            await _expertRepository.ChangeStatus(id,cancellation);
        }

        public async Task Delete(int id, CancellationToken cancellation)
        {
            await _expertRepository.Delete(id, cancellation);
        }

        public async Task<Expert> Get(int id, CancellationToken cancellation)
        {
            return await _expertRepository.Get(id, cancellation);
        }

        public async Task<List<Expert>> GetAll(CancellationToken cancellation)
        {
            return await _expertRepository.GetAll(cancellation);
        }

        public async Task<Expert> GetExpertInfo(int id, CancellationToken cancellation)
        {
            return await _expertRepository.GetExpertInfo(id,cancellation);
        }

        public async Task ProccessPayment(int id, decimal amount, CancellationToken cancellation)
        {
            await _expertRepository.ProccessPayment(id,amount,cancellation);
        }

        public async Task RemoveSkill(int id, Service service, CancellationToken cancellation)
        {
            await _expertRepository.RemoveSkill(id,service,cancellation);
        }

        public async Task Update(UpdateExpertDto expert, CancellationToken cancellation)
        {
            await _expertRepository.Update(expert, cancellation);
        }

        public async Task UpdateSkills(int id, Service service, CancellationToken cancellation)
        {
            await _expertRepository.UpdateSkills(id,service,cancellation);
        }
    }
}
