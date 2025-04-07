using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Entities.BaseEntity;
using Dapper;
using Microsoft.Data.SqlClient;

namespace App.Domain.Infra.Repos.Dapper.Repository
{
    public class CityDapperRepository : ICityRepository
    {
        private readonly string _connectionString;

        public CityDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(City city, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "INSERT INTO Cities (Name) VALUES (@Name)";
                await connection.ExecuteAsync(sql, city);
            }
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "DELETE FROM Cities WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<City> Get(int id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "SELECT * FROM Cities WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<City>(sql, new { Id = id });
            }
        }

        public async Task<List<City>> GetAll(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "SELECT * FROM Cities";
                var cities = await connection.QueryAsync<City>(sql);
                return cities.ToList();
            }
        }

        public async Task Update(City city, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "UPDATE Cities SET Name = @Name WHERE Id = @Id";
                await connection.ExecuteAsync(sql, city);
            }
        }
    }
}