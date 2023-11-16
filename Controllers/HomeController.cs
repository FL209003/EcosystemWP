//using AppLogic.UCInterfaces;
//using AppLogic.UseCases;
//using Domain.Entities;
using EcosystemApp.Filters;
using EcosystemApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using Utility;

namespace EcosystemApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //public IFindUser FindUserUC { get; set; }

        //public HomeController(ILogger<HomeController> logger, IFindUser findUser)
        //{
        //    _logger = logger;
        //    FindUserUC = findUser;
        //}

        public IActionResult Index() { return View(); }

        public IActionResult Login() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        User u = FindUserUC.Find(model.Username);
            //        if (u != null && Hash.ValidateHash(model.Password, u.HashPassword))
            //        {
            //            HttpContext.Session.SetString("username", u.Username);
            //            HttpContext.Session.SetString("rol", u.Role);
            //            return RedirectToAction("Index", "Home");
            //        }
            //        else throw new InvalidOperationException("Nombre de usuario y/o contraseña incorrectos.");
            //    }
            //    catch (InvalidOperationException ex)
            //    {
            //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
            //        return View(model);
            //    }
            //    catch (Exception ex)
            //    {
            //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
            //        return View(model);
            //    }
            //}
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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