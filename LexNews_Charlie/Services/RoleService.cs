using LexNews_Charlie.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LexNews_Charlie.Models;

namespace LexNews_Charlie.Services
{
    public class RoleService:IRoleService
    {

       // private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {

            //  _db = db;
            _roleManager = roleManager;
        }

        //public List<IdentityRole> GetOneRole(int id)
        //{
        //    return _roleManager.Roles.Where(r => r.Id = id).ToList();
        //}
        public List<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }
    }
}

