using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using LexNews_Charlie.Models.ViewModels;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LexNews_Charlie.Controllers
{
    [Authorize(Roles="Admin")]
    public class RoleController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _db;
        private readonly IRoleService _roleService;
        
        public RoleController(ILogger<AdminController> logger, RoleManager<IdentityRole> roleManager, 
            UserManager<User> userManager, IUserService userService, ApplicationDbContext db, 
            IRoleService roleService)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _userService = userService;
            _db = db;
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        public IActionResult GetRolesList()
        {
            List<IdentityRole> rolesList = _roleService.GetRoles();
            List<string> roleNames = new List<string>();
            foreach (var item in rolesList)
            {
                roleNames.Add(item.Name);
            }
            return View(roleNames);
        }
       
        
        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var values = await _roleManager.FindByIdAsync(id);

            RoleUpdateVM model = new RoleUpdateVM
            {
                Id = values.Id,
                Name = values.Name,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateVM model)
        {

            var role = await _roleManager.FindByIdAsync(model.Id);

            role.Name = model.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("GetRolesList");
            }
            return View(model);
        }

        
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult GetUsersList()
        {
            var users = _userManager.Users.ToList();
            return View (users);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult GetEmployeesList()
        {
            var employees = _userService.GetAllUsers().Where(x => x.Employee).ToList();

            return View(employees);
        }

       
        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);

            var roles = _roleManager.Roles.ToList();

            //TempData["UserId"] = user.Id;
            ViewBag.UserId = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);

            List<RoleAssignVM> model = new List<RoleAssignVM>();

            foreach (var item in roles)
            {
                RoleAssignVM m = new RoleAssignVM();
                m.Id = item.Id;
                m.Name = item.Name;
                m.Exists = userRoles.Contains(item.Name);
                model.Add(m);
            }
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignVM> model, string userId)
        {
            //var userid = (string)TempData["UserId"];
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            //var user = _usermanager.Users.FirstOrDefault(x => x.Id = model.id);
            foreach (var item in model)
            {
                if (item.Exists)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("GetEmployeesList");
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.Name
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRolesList");

                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            //var role = await _db.Roles.FirstOrDefaultAsync(r => r.Id == id);
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _db.Roles.FindAsync(id);
            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(GetRolesList));
        }

    }
}
