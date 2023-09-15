using System.Data;

namespace SimpleServiceDeskBuilding.Repositories
{
    public interface IGenericRepository
    {
        Task<int> SaveAsync(string query, object param);
        Task<T> SaveAsync<T>(string query, object param);
        Task<int> UpdateAsync(string query, object param);
        Task<int> DeleteAsync(string query, object param);
        Task<T> FindByAsync<T>(string query, object param);
        Task<List<T>> FindAllByAsync<T>(string query, object param);
    }
}
