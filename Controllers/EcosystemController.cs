using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcosystemApp.Filters;
using EcosystemApp.Models;

namespace EcosystemApp.Controllers
{
    public class EcosystemController : Controller
    {
        //public IAddEcosystem AddUC { get; set; }
        //public IRemoveEcosystem RemoveUC { get; set; }
        //public IListEcosystem ListUC { get; set; }
        //public IFindEcosystem FindUC { get; set; }
        //public IWebHostEnvironment WHE { get; set; }
        //public IListCountries ListCountriesUC { get; set; }
        //public IFindCountry FindCountryUC { get; set; }
        //public IFindConservation FindConservationUC { get; set; }
        //public IListThreats ListThreatsUC { get; set; }
        //public IFindThreat FindThreatUC { get; set; }

        //public EcosystemController(IAddEcosystem addUC, IRemoveEcosystem removeUC, IListEcosystem listUC,
        //    IFindEcosystem findUC, IWebHostEnvironment whe, IListCountries listCountries, IFindCountry findCountryUC, IFindConservation findConservationUC, IListThreats listThreatsUC, IFindThreat findThreatUC)
        //{
        //    AddUC = addUC;
        //    RemoveUC = removeUC;
        //    ListUC = listUC;
        //    FindUC = findUC;
        //    WHE = whe;
        //    ListCountriesUC = listCountries;
        //    FindCountryUC = findCountryUC;
        //    FindConservationUC = findConservationUC;
        //    ListThreatsUC = listThreatsUC;
        //    FindThreatUC = findThreatUC;
        //}

        public ActionResult Index()
        {
            //IEnumerable<Ecosystem> ecos = ListUC.List();
            //if (ecos != null && ecos.Count() > 0)
            //{

            //    return View(ecos);
            //}
            //else
            //{
            //    ViewBag.Error = "No se encontraron ecosistemas.";
            //    return View(ecos);
            //}
            return View();
        }

        // public IActionResult Details() { return View(); }

        //[Private]
        //public ActionResult AddEcosystem()
        //{
        //    IEnumerable<Country> countries = ListCountriesUC.List();
        //    IEnumerable<Threat> threats = ListThreatsUC.List();
        //    VMEcosystem vmEcosystem = new VMEcosystem() { Countries = countries, IdSelectedCountry = new List<int>(), Threats = threats, IdSelectedThreats = new List<int>() };
        //    return View(vmEcosystem);
        //}

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
