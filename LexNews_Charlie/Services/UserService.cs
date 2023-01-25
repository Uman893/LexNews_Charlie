using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Identity;




namespace LexNews_Charlie.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(ApplicationDbContext db, UserManager<User>
                        userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public List<User> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public void AddRole(string roleName)
        {
            _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public void AddRoleToUser(string userId, string roleName)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            _userManager.AddToRoleAsync(user, roleName);
        }
    }
}


