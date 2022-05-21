using loginLogOutWith.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace loginLogOutWith.Controllers
{
    public class HomeController : Controller
    {
        private readonly employeecontext _db;

        public HomeController(employeecontext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index()
        {
            var rese = _db.Employees.ToList();
            return View(rese);
        }
        public IActionResult login(login obj)
        {
            var log = _db.logins.Where(m => m.email == obj.email).FirstOrDefault();
            if (log == null)
            {
                TempData["email"] = "invalid ";
            }
            else
            {
                if (log.email == obj.email && log.password == obj.password)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, log.name),
                                        new Claim(ClaimTypes.Email, log.email) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    authProperties);

                   // HttpContext.Session.SetString("Email", log.name);

                    return RedirectToAction("index");
                }
                else
                {
                    TempData["pass"] = "invalid ";
                }
            }
            return View();
        }
          [Authorize]
        public ActionResult logout()
        {

            HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                );

                return RedirectToAction("login");

           
        }
    }
}
