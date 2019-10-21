// I, Dylan Ribau, student number 000392748, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1B.Data;
using Lab1B.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab1B.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> RoleManager)
        {
            _userManager = userManager;
            _roleManager = RoleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SeedRoles()
        {
            ApplicationUser user1 = new ApplicationUser()
            {
                Email = "user1@gmail.com",
                BirthDate = DateTime.Now,
                FirstName = "Dylan",
                LastName = "Ribau",
                UserName = "user1@gmail.com"
            };

            ApplicationUser user2 = new ApplicationUser()
            {
                Email = "user2@gmail.com",
                BirthDate = DateTime.Now,
                FirstName = "James",
                LastName = "Smith",
                UserName = "user2@gmail.com"
            };

            IdentityResult result = await _userManager.CreateAsync(user1, "User1!");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _userManager.CreateAsync(user2, "User2!");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _roleManager.CreateAsync(new IdentityRole("Staff"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });

            result = await _roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });


            result = await _userManager.AddToRoleAsync(user1, "Staff");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });

            result = await _userManager.AddToRoleAsync(user2, "Manager");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });



            return RedirectToAction("Index", "Home");
        }
    }
}