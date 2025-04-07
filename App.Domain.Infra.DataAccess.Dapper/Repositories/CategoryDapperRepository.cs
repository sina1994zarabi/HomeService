using App.Domain.Core.Contract.Repository;
using App.Domain.Core.DTOs.CategoryDto;
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
    public class CategoryDapperRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(CreateCategoryDto category, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "INSERT INTO Categories (Title, Image) VALUES (@Title, @Image)";
                await connection.ExecuteAsync(sql, new { Title = category.Title, Image = category.ImagePath });
            }
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "DELETE FROM Categories WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<Category> Get(int id, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "SELECT * FROM Categories WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
            }
        }

        public async Task<List<Category>> GetAll(CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = @"
                SELECT c.*, s.*
                FROM Categories c
                LEFT JOIN Subcategories s ON c.Id = s.CategoryId
                ORDER BY c.Id, s.Id";

                var categoryDict = new Dictionary<int, Category>();

                await connection.QueryAsync<Category, SubCategory, Category>(
                    sql,
                    (category, subcategory) =>
                    {
                        if (!categoryDict.TryGetValue(category.Id, out var currentCategory))
                        {
                            currentCategory = category;
                            currentCategory.Subcategories = new List<SubCategory>();
                            categoryDict.Add(category.Id, currentCategory);
                        }
                        if (subcategory != null)
                        {
                            currentCategory.Subcategories.Add(subcategory);
                        }
                        return currentCategory;
                    },
                    splitOn: "Id");

                return categoryDict.Values.ToList();
            }
        }

        public async Task Update(UpdateCategoryDto category, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var sql = "UPDATE Categories SET Title = @Title, Image = @Image WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Title = category.Title, Image = category.ImagePath, Id = category.Id });
            }
        }
    }
}