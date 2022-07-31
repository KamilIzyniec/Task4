using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task4.Models;
using Task4.Services;

namespace Task4.Controllers
{
    public class AdminPanelController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            SecurityService securityService = new SecurityService();
            List<UserModel> users = securityService.UserList();
            return View("Index", users);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login"); ;
        }
        public string LoggedInUser => User.Identity.Name;
        [HttpPost]
            [Authorize]
            public async Task<IActionResult> BlockUser([FromBody] string result)
            {

            SecurityService securityService = new SecurityService();
            if (result.Length > 0)
            {
                string correctedResult = result.Remove(result.Length - 1);
                int[] values = Array.ConvertAll(correctedResult.Split(','), int.Parse);
                int userid = securityService.GetUserId(LoggedInUser);
                foreach (int i in values)
                {
                    securityService.UsersBlocked(i);
                    if (userid == i)
                    {
                        await HttpContext.SignOutAsync();
                    }
                }
            }
            List<UserModel> users = securityService.UserList();
            return View("Index", users);
        }
        [HttpPost]
        [Authorize]
        public IActionResult UnblockUser([FromBody] string result)
        {
            SecurityService securityService = new SecurityService();
            if (result.Length > 0)
            {
                string correctedResult = result.Remove(result.Length - 1);
                int[] values = Array.ConvertAll(correctedResult.Split(','), int.Parse);

                foreach (int i in values)
                {
                    securityService.UsersUnblocked(i);

                }
            }
            List<UserModel> users = securityService.UserList();
            return View("Index", users);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromBody] string result)
        {
            SecurityService securityService = new SecurityService();
            if (result.Length > 0)
            {
                string correctedResult = result.Remove(result.Length - 1);
                int[] values = Array.ConvertAll(correctedResult.Split(','), int.Parse);
                int userid = securityService.GetUserId(LoggedInUser);
                foreach (int i in values)
                {
                    securityService.UsersDeleted(i);
                    if (userid == i)
                    {
                        await HttpContext.SignOutAsync();
                    }
                }
            }
            List<UserModel> users = securityService.UserList();
            return View("Index", users);
        }





    }
}
