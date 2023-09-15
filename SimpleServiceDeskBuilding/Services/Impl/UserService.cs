using SimpleServiceDeskBuilding.Exceptions;
using SimpleServiceDeskBuilding.Models;
using SimpleServiceDeskBuilding.Repositories;

namespace SimpleServiceDeskBuilding.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository _repository;

        public UserService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserModel> GetUserById(string user_id)
        {
            string query = $"select * from public.tb_master_user where id = '{user_id}'";

            var createNewUser = await _repository.FindByAsync<UserModel>(query, new { });
            if (createNewUser == null)
            {
                throw new NotFoundException("Data user not found");
            }

            return createNewUser;
        }

        public async Task<int> CreateNewUser(UserModel user)
        {
            string query = "insert into public.tb_master_user(fullname, username, password, email, identity_employee, is_active, role, refresh_token) values (@fullname, @username, @password, @email, @identity_employee, @is_active, @role, @refresh_token)";

            var dataUser = await _repository.SaveAsync(query, user);
            if (dataUser == 0)
            {
                throw new Exception("Error when insert data");
            }

            return dataUser;
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            string query = $"select * from public.tb_master_user where username = '{username}'";

            var createNewUser = await _repository.FindByAsync<UserModel>(query, new { });
            if (createNewUser == null)
            {
                throw new NotFoundException("Data user not found");
            }

            return createNewUser;
        }
    }
}
