using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task4.Models;
using Task4.Services;

namespace Task4.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterNewUser(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();
            if (securityService.RegistrationCompleted(userModel))
            {
                TempData["Success"] = "Registration successful. Please log in.";
                return RedirectToAction("Index", "Login"); ;
            }
            else
            {
                TempData["RegistrationError"] = "Incorrect data. Please try again.";
                return View("Index");
            }
        }
    }
}
