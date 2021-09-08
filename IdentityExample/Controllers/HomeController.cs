using IdentityExample.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    public class HomeController: Controller
    {
        private readonly AppDbContext _contenxt;
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(AppDbContext contenxt,
            UserManager<IdentityUser> usermanager,
            SignInManager<IdentityUser> signInManager)
        {
            _contenxt = contenxt;
            _usermanager = usermanager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //login functionality
            //login functionality
            var user = await _usermanager.FindByNameAsync(username);
            if(user != null)
            {
                // sign in
                var sigInresult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (sigInresult.Succeeded)
                {
                    return RedirectToAction("Index");
                };
            }
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // register functionality
            var user = new IdentityUser
            {
                UserName = username,
                Email = ""
            };
            var result = await _usermanager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // sign user here
                // sign in
                var sigInresult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (sigInresult.Succeeded)
                {
                    return RedirectToAction("Index");
                };
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

      
    }
}
