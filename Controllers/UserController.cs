using Microsoft.AspNetCore.Mvc;
using EcosystemApp.Filters;
//using AppLogic.UCInterfaces;
using EcosystemApp.Models;
using Newtonsoft.Json;
using PresentacionMVC.DTOs;
using EcosystemApp.Globals;
//using Utility;

namespace EcosystemApp.Controllers
{
    [Private(Role = "Admin")]
    public class UserController : Controller
    {
        public string? ApiURL { get; set; }

        public UserController(IConfiguration conf)
        {
            ApiURL = conf.GetValue<string>("ApiURL");
        }

        public ActionResult Index() { return View(); }

        public ActionResult Login() { return View(); }

        [HttpPost]
        public IActionResult Login(VMUser u)
        {
            try
            {                
                string url = $"{ApiURL}/api/Users";
                //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = Global.PostAsJson(url, u);
                string body = Global.GetContent(response);

                if (response.IsSuccessStatusCode)
                {
                    //OBTENGO EL TOKEN Y EL ROL
                    var login = JsonConvert.DeserializeObject<LoginDTO>(body);
                    string role = login.Role;
                    string token = login.TokenJWT;

                    //LOS GUARDO EN SESSION
                    HttpContext.Session.SetString("token", token);
                    HttpContext.Session.SetString("rol", role);

                    //REDIRIJO A ALGUNA ACCIÓN
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = body;
                    return View(u);
                }
            }
            catch
            {
                ViewBag.Error("Ocurrió un error inesperado");
                return View(u);
            }
        }

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

        public IActionResult AddUser() { return View(); }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddUser(VMUser model)
        //{
        //        try
        //        {
        //            model.User.HashPassword = Hash.ComputeSha256Hash(model.User.Password);
        //            model.User.Validate();
        //            if (model.VerificationPass == model.User.Password)
        //            {
        //                AddUC.Add(model.User);
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else throw new InvalidOperationException("Las contraseñas no coinciden.");
        //        }
        //        catch (InvalidOperationException ex)
        //        {
        //            ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //            return View(model);
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //            return View(model);
        //        }
        //}
    }
}
