using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcosystemApp.Models;
//using AppLogic.UCInterfaces;

namespace EcosystemApp.Controllers
{
    public class ThreatController : Controller
    {
        //public IAddThreat AddUC { get; set; }

        //public ThreatController(IAddThreat addUC)
        //{
        //    AddUC = addUC;
        //}

        // GET: ThreatController/Create
        public ActionResult AddThreat() { return View(); }

        // POST: ThreatController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddThreat(VMThreat model)
        //{
        //    model.Threat.ThreatName = new Domain.ValueObjects.Name(model.ThreatNameVAL);
        //    try
        //    {
        //        model.Threat.Validate();
        //        AddUC.Add(model.Threat);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        return View(model);
        //    }
        //}
    }
}
