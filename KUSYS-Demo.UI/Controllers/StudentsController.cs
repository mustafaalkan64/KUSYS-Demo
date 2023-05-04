using KUSYS_Demo.Core.Models;
using KUSYS_Demo.Core.ViewModels;
using KUSYS_Demo.Services.Interfaces;
using KUSYS_Demo.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Diagnostics;

namespace KUSYS_Demo.UI.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class StudentsController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<HomeController> logger, IStudentService studentService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) : base(userManager, signInManager, roleManager)
        {
            _logger = logger;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            var result = _studentService.GetAll();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new StudentViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> Create(StudentViewModel studentViewModel)
        {
            if(ModelState.IsValid)
            {
                var student = new Student()
                {
                    Birthday = studentViewModel.Birthday,
                    FirstName = studentViewModel.FirstName,
                    LastName = studentViewModel.LastName,
                };
                await _studentService.Create(student);

                AppUser user = new AppUser();
                user.UserName = studentViewModel.UserName;
                user.Email = studentViewModel.Email;
                user.StudentId = student.StudentId;
                user.NormalizedEmail = studentViewModel.Email.ToUpper();
                user.NormalizedUserName = studentViewModel.UserName.ToUpper();

                if (userManager.Users.Any(u => u.Email == studentViewModel.Email))
                {
                    return Json(new { Message = "Email Address Has Already Been Taken", Status = false });
                }


                IdentityResult result = await userManager.CreateAsync(user, "Password_123");
                await userManager.AddToRoleAsync(user, "User");

                if (result.Succeeded)
                {
                    return Json(new { Message = "New Student Created Successfully", Status = true });
                }
                else
                {
                    var errorList = result.Errors.Select(x => x.Description);
                    return Json(new { Message = string.Join(",", errorList), Status = false });
                }
            }
            else
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
                return Json(new { Message = string.Join(",", errorList), Status = false });
            }

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