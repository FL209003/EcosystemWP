//using AppLogic.UCInterfaces;
//using AppLogic.UseCases;
using EcosystemApp.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public ActionResult ModifyNameLengths() { return View(); }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyNameLengths(int minLength, int maxLength)
        {
            try
            {
                if (maxLength > minLength)
                {
                    //ModifyLengthParamUC.ModifyNameParams(minLength, maxLength);
                    //TempData["SuccessMessage"] = "Valores limite cambiados con éxito.";
                    return View();
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
                if (maxLength > minLength)
                {
                    //ModifyLengthParamUC.ModifyDescParams(minLength, maxLength);
                    //TempData["SuccessMessage"] = "Valores limite cambiados con éxito.";
                    return View();
                }
                else throw new InvalidOperationException("El largo máximo de la descripción debe ser mayor al largo mínimo.");
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
