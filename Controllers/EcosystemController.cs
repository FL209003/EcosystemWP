using Microsoft.AspNetCore.Mvc;
using EcosystemApp.Filters;
using EcosystemApp.Models;
using DTOs;
using Newtonsoft.Json;
using EcosystemApp.Globals;
using System.Security.Policy;
using System;

namespace EcosystemApp.Controllers
{
    public class EcosystemController : Controller
    {
        public IWebHostEnvironment WHE { get; set; }
        public string ApiURL { get; set; }

        public EcosystemController(IWebHostEnvironment whe, IConfiguration conf)
        {
            WHE = whe;
            ApiURL = conf.GetValue<string>("ApiURL");
        }

        public ActionResult Index()
        {
            IEnumerable<EcosystemDTO> ecos = null;

            string url = $"{ApiURL}/api/Countries/";

            string body = Global.GetContent(Global.GetResponse(url));

            if (Global.GetResponse(url).IsSuccessStatusCode)
            {
                ecos = JsonConvert.DeserializeObject<List<EcosystemDTO>>(body);
                return View(ecos);
            }
            else
            {
                ViewBag.Error = body;
                return View(new List<EcosystemDTO>());
            }
        }

        // public IActionResult Details() { return View(); }

        [Private]
        public ActionResult AddEcosystem()
        {
            string urlCountries = $"{ApiURL}/api/Countries/";
            string urlThreats = $"{ApiURL}/api/Threats/";

            string bodyCountries = Global.GetContent(Global.GetResponse(urlCountries));
            string bodyThreats = Global.GetContent(Global.GetResponse(urlThreats));

            IEnumerable<CountryDTO> countries = JsonConvert.DeserializeObject<List<CountryDTO>>(bodyCountries);
            IEnumerable<ThreatDTO> threats = JsonConvert.DeserializeObject<List<ThreatDTO>>(bodyThreats);

            VMEcosystem vmEcosystem = new VMEcosystem() { Countries = countries, IdSelectedCountry = new List<int>(), Threats = threats, IdSelectedThreats = new List<int>() };
            return View(vmEcosystem);
        }

        // POST: EcosystemController/Create
        [Private]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEcosystem(VMEcosystem model)
        {
            try
            {
                //model.Countries = ListCountriesUC.List();
                //if (model.Ecosystem.Countries == null) { model.Ecosystem.Countries = new List<Country>(); };
                //if (model.IdSelectedCountry == null) { model.IdSelectedCountry = new List<int>(); };                
                //foreach (int country in model.IdSelectedCountry) { model.Ecosystem.Countries.Add(FindCountryUC.FindById(country)); };

                //model.Threats = ListThreatsUC.List();
                //if (model.Ecosystem.Threats == null) { model.Ecosystem.Threats = new List<Threat>(); };
                //if (model.IdSelectedThreats == null) { model.IdSelectedThreats = new List<int>(); };
                //foreach (int threat in model.IdSelectedThreats) { model.Ecosystem.Threats.Add(FindThreatUC.Find(threat)); };

                //model.Ecosystem.EcoConservation = FindConservationUC.FindBySecurity(model.Ecosystem.Security);
                //model.Ecosystem.EcosystemName = new Domain.ValueObjects.Name(model.EcosystemNameVAL);
                //model.Ecosystem.EcoDescription = new Domain.ValueObjects.Description(model.EcoDescriptionVAL);
                //model.Ecosystem.GeoDetails = new Domain.ValueObjects.GeoUbication(model.Lat, model.Long);

                //FileInfo fi = new(model.ImgEco.FileName);
                //string ext = fi.Extension;

                //if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                //{
                //    string trimmedString = model.EcosystemNameVAL.Replace(" ", "");
                //    string fileName = trimmedString + "_001" + ext;
                //    model.Ecosystem.ImgRoute = fileName;

                //    model.Ecosystem.Validate();
                //    AddUC.Add(model.Ecosystem);

                //    string rootDir = WHE.WebRootPath;
                //    string route = Path.Combine(rootDir, "img/Ecosystems", fileName);
                //    using (FileStream fs = new(route, FileMode.Create))
                //    {
                //        model.ImgEco.CopyTo(fs);
                //    }
                return RedirectToAction(nameof(Index));
                //}
                //else
                //{
                //    ViewBag.Error = "El tipo de imagen debe ser png, jpg o jpeg.";
                //    ModelState.AddModelError(string.Empty, ViewBag.Error);
                return View(model);
                // }
            }
            //catch (EcosystemException ex)
            //{
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //    return View(model);
            //}
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        //[Private]
        //public ActionResult Delete(int id)
        //{

        //    Ecosystem eco = FindUC.Find(id);
        //    if (eco == null)
        //    {
        //        ViewBag.Error = "El cliente con el id " + id + " noexiste";
        //    }
        //    return View(eco);
        //}

        // POST: EcosystemController/Delete
        //[Private]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(Ecosystem e)
        //{
        //    try
        //    {
        //        Ecosystem eco = FindUC.Find(e.Id);
        //        if (eco != null)
        //        {
        //            if (eco.Species == null || eco.Species.Count > 0)
        //            {
        //                throw new InvalidOperationException("Este ecosistema tiene especies asociadas y por lo tanto no se puede eliminar");
        //            }
        //            RemoveUC.Remove(eco);
        //            return RedirectToAction("Index", "Ecosystem");
        //        }
        //        else throw new InvalidOperationException("No se encontró el ecosistema que desea eliminar.");
        //    }
        //    catch (EcosystemException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        return View();
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        return View();
        //    }
        //}
    }
}
