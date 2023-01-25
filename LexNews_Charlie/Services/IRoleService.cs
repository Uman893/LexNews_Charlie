using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using LexNews_Charlie.Models;


namespace LexNews_Charlie.Services
{
    public interface IRoleService
    {
        List<IdentityRole> GetRoles();
    }
}
