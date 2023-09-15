using SimpleServiceDeskBuilding.Models;

namespace SimpleServiceDeskBuilding.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Save new data user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int> CreateNewUser(UserModel user);
        /// <summary>
        /// Get data user by id
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        Task<UserModel> GetUserById(string user_id);
        /// <summary>
        /// Get data user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<UserModel> GetUserByUsername(string username);
    }
}
