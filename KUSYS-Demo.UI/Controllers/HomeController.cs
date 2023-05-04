using KUSYS_Demo.Core.Models;
using KUSYS_Demo.Services.Interfaces;
using KUSYS_Demo.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics;

namespace KUSYS_Demo.UI.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentService;

        public HomeController(ILogger<HomeController> logger, IStudentService studentService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) : base(userManager, signInManager, roleManager)
        {
            _logger = logger;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            AppUser CurrentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
            bool UserIsAdmin = userManager.IsInRoleAsync(CurrentUser, "Admin").Result;
            var result = _studentService.GetAll();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}