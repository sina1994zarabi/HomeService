using App.Domain.Core.Contract.Repository;
using App.Domain.Core.DTOs.SubCategoryDto;
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
    public class SubCategoryDapperRepository : ISubCategoryRepository
    {
        private readonly string _connectionString;

        public SubCategoryDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(CreateSubCategoryDto subcategory, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "INSERT INTO Subcategories (Title, CategoryId, ImagePath) VALUES (@Title, @CategoryId, @ImagePath)";
                await connection.ExecuteAsync(sql, new
                {
                    Title = subcategory.Title,
                    CategoryId = subcategory.CategoryId,
                    ImagePath = subcategory.ImagePath
                });
            }
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "DELETE FROM Subcategories WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<SubCategory> Get(int id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = @"
                SELECT s.*, c.*
                FROM Subcategories s
                INNER JOIN Categories c ON s.CategoryId = c.Id
                WHERE s.Id = @Id";

                var result = await connection.QueryAsync<SubCategory, Category, SubCategory>(
                    sql,
                    (subcategory, category) =>
                    {
                        subcategory.Category = category;
                        return subcategory;
                    },
                    new { Id = id },
                    splitOn: "Id"
                );

                return result.FirstOrDefault();
            }
        }

        public async Task<List<SubCategory>> GetAll(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = @"
                SELECT s.*, c.*
                FROM Subcategories s
                INNER JOIN Categories c ON s.CategoryId = c.Id
                ORDER BY s.Id";

                var subcategories = await connection.QueryAsync<SubCategory, Category, SubCategory>(
                    sql,
                    (subcategory, category) =>
                    {
                        subcategory.Category = category;
                        return subcategory;
                    },
                    splitOn: "Id"
                );

                return subcategories.ToList();
            }
        }

        public async Task Update(UpdateSubCategoryDto subcategory, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "UPDATE Subcategories SET Title = @Title, CategoryId = @CategoryId, ImagePath = @ImagePath WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new
                {
                    Title = subcategory.Title,
                    CategoryId = subcategory.CategoryId,
                    ImagePath = subcategory.ImagePath,
                    Id = subcategory.Id
                });
            }
        }
    }
}