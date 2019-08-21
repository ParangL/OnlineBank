using BankAppMvc.Data;
using BankAppMvc.Models;
using BankAppMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.Controllers
{    
    public class AuthController : Controller
    {
        private BankAppDataContext _ctx;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AuthController(BankAppDataContext ctx, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel x)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(x.UserName, x.Password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewData["Message"] = " We don't recognize that username or password. Please try again.";
            return View();
        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        

        [Authorize(Roles = "Admin")]
        public IActionResult RegisterUser()
        {
            return View();
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(LoginViewModel user)
        {
            if(user.Password == user.CheckPassword)
            {
                if (ModelState.IsValid)
                {
                    var userIdentity = new ApplicationUser { UserName = user.UserName };
                    var result = await _userManager.CreateAsync(userIdentity, user.Password);
                    var resultRole = await _userManager.AddToRoleAsync(userIdentity, user.RoleName);

                    if (result.Succeeded)
                    {
                        ViewData["Message"] = "New user is created";
                    }
                }
                return View();
            }

                ViewData["Message"] = "Wrong password! Try again";
                return View();
            }

        
    }
    }    

    

