using LexNews_Charlie.Models;

namespace LexNews_Charlie.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void AddRole(string roleName);

        void AddRoleToUser(string userId, string roleName);
    }
}
