using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Task4.Models;
using Task4.Services;

namespace Task4.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProcessLogin(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();
            if (securityService.IsValid(userModel))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", userModel.UserName));
                claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                TempData["Error"] = "Invalid Username or Password.";
                return View("Index");
            }
        }
    }
}
