//using AppLogic.UCInterfaces;
//using AppLogic.UseCases;
using EcosystemApp.DTOs;
using EcosystemApp.Filters;
using EcosystemApp.Globals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Policy;

namespace EcosystemApp.Controllers
{
    [Private]
    public class ParamController : Controller
    {
        //public IModifyLengthParam ModifyLengthParamUC { get; set; }

        //public ParamController(IModifyLengthParam modifyUC)
        //{
        //    ModifyLengthParamUC = modifyUC;
        //}
        public string? ApiURL { get; set; }

        public ParamController(IConfiguration conf)
        {
            ApiURL = conf.GetValue<string>("ApiURL");
        }
        public ActionResult ModifyNameLengths() { return View(); }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyNameLengths(int minLength, int maxLength)
        {
            try
            {
                if (maxLength > minLength)
                {
                    string url = $"{ApiURL}api/Limit/name";
                    string token = HttpContext.Session.GetString("token");
                    HttpResponseMessage response1 = Global.PutAsJson(url, new LimitDTO(minLength, maxLength), token);
                    if (response1.IsSuccessStatusCode) {
                        TempData["SuccessMessage"] = "Valores limite cambiados con éxito.";
                        return View();
                    }         
                    else throw new InvalidOperationException(response1.StatusCode.ToString());

                }
                else throw new InvalidOperationException("El largo máximo del nombre debe ser mayor al largo mínimo.");
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult ModifyDescLengths() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyDescLengths(int minLength, int maxLength)
        {
            try
            {
                string url = $"{ApiURL}api/Limit/desc";
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage response1 = Global.PutAsJson(url, new LimitDTO(minLength, maxLength), token);
                if (response1.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Valores limite cambiados con éxito.";
                    return View();
                }
                else throw new InvalidOperationException(response1.StatusCode.ToString());
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
