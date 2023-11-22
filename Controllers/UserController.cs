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
        public IActionResult Login(VMUser model)
        {
            try
            {
                string url = $"{ApiURL}api/User/Login";
                //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = Global.PostAsJson(url, model.User);
                string body = Global.GetContent(response);

                if (response.IsSuccessStatusCode)
                {
                    //OBTENGO EL TOKEN Y EL ROL
                    var login = JsonConvert.DeserializeObject<LoginDTO>(body);
                    string role = login.Role;
                    string token = login.TokenJWT;

                    //LOS GUARDO EN SESSION
                    HttpContext.Session.SetString("token", token);
                    HttpContext.Session.SetString("role", role);

                    //REDIRIJO A ALGUNA ACCIÓN
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = body;
                    return View(model);
                }
            }
            catch
            {
                ViewBag.Error("Ocurrió un error inesperado");
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Private(Role = "Admin")]
        public ActionResult AddUser() { return View(); }

        [Private(Role = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(VMUser model)
        {
            try
            {
                model.User.HashPassword = Hash.ComputeSha256Hash(model.User.Password);

                //el model state no queria validar el hash, ni idea por que
                ModelState.Remove("User.HashPassword");
                ModelState.MarkFieldValid("User.HashPassword");

                if (ModelState.IsValid && model.VerificationPass == model.User.Password)
                {
                    string url = $"{ApiURL}api/User";

                    HttpResponseMessage response = Global.PostAsJson(url, model.User);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        string error = Global.GetContent(response);
                        ViewBag.Error = error;
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.Error = "Todos los campos son obligatorios.";
                    return View(model);
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado, no se ha podido crear el usuario.";
            }
            return View(model);
        }
    }
}
