using Dapper;
using SimpleServiceDeskBuilding.Context;
using System.Data;

namespace SimpleServiceDeskBuilding.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DapperContext _dapperContext;

        public GenericRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> DeleteAsync(string query, object param)
        {
            using var connection = _dapperContext.CreateConnection();
            var result = await connection.ExecuteAsync(query, param);
            return result;
        }

        public async Task<List<T>> FindAllByAsync<T>(string query, object param)
        {
            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QueryAsync<T>(query, param);
            return result.ToList();
        }

        public async Task<T> FindByAsync<T>(string query, object param)
        {
            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QueryAsync<T>(query, param);
            return result.FirstOrDefault();
        }

        public async Task<int> SaveAsync(string query, object param)
        {
            using var connection = _dapperContext.CreateConnection();
            var result = await connection.ExecuteAsync(query, param);
            return result;
        }

        public async Task<T> SaveAsync<T>(string query, object param)
        {
            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QueryAsync<T>(query, param);
            return result.Single();
        }

        public async Task<int> UpdateAsync(string query, object param)
        {
            using var connection = _dapperContext.CreateConnection();
            var result = await connection.ExecuteAsync(query, param);
            return result;
        }
    }
}
