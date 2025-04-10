using App.Domain.Core.Contract.Repository;
using App.Domain.Core.DTOs.ServiceDto;
using App.Domain.Core.Entities.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Infra.Repos.Dapper.Repository
{
    public class ServiceDapperRepository : IServiceRepository
    {
        private readonly string _connectionString;

        public ServiceDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(CreateServiceDto service, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "INSERT INTO Services (Title, Description, Price, SubCategoryId) VALUES (@Title, @Description, @Price, @SubCategoryId)";
                await connection.ExecuteAsync(sql, new
                {
                    Title = service.Title,
                    Description = service.Description,
                    Price = service.Price,
                    SubCategoryId = service.SubCategoryId
                });
            }
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "DELETE FROM Services WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<Service> Get(int id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = @"
                SELECT s.*, sc.*
                FROM Services s
                INNER JOIN Subcategories sc ON s.SubCategoryId = sc.Id
                WHERE s.Id = @Id";

                var result = await connection.QueryAsync<Service, SubCategory, Service>(
                    sql,
                    (service, subcategory) =>
                    {
                        service.Subcategory = subcategory;
                        return service;
                    },
                    new { Id = id },
                    splitOn: "Id"
                );

                return result.FirstOrDefault();
            }
        }

        public async Task<List<Service>> GetAll(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = @"
                SELECT s.*, sc.*
                FROM Services s
                INNER JOIN Subcategories sc ON s.SubCategoryId = sc.Id
                ORDER BY s.Id";

                var services = await connection.QueryAsync<Service, SubCategory, Service>(
                    sql,
                    (service, subcategory) =>
                    {
                        service.Subcategory = subcategory;
                        return service;
                    },
                    splitOn: "Id"
                );

                return services.ToList();
            }
        }

        public async Task<List<GetServiceDto>> GetServiceDtos(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "SELECT Id, Title AS Name, Price FROM Services";
                var serviceDtos = await connection.QueryAsync<GetServiceDto>(sql);
                return serviceDtos.ToList();
            }
        }

        public async Task Update(UpdateServiceDto service, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "UPDATE Services " +
                    "SET Title = @Title," +
                    " Description = @Description," +
                    " Price = @Price, SubCategoryId = @SubCategoryId," +
                    " Image = CASE " +
                    "WHEN (@Image IS NOT NULL AND @Image <> '') THEN @Image ELSE Image END" +
                    " WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new
                {
                    Title = service.Title,
                    Description = service.Description,
                    Price = service.Price,
                    SubCategoryId = service.SubCategoryId,
                    Image = service.ImagePath,
                    Id = service.Id
                });
            }
        }
    }
}