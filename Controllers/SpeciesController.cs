//using AppLogic.UCInterfaces;
using DTOs;
using EcosystemApp.Filters;
using EcosystemApp.Globals;
using EcosystemApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentacionMVC.DTOs;
using System;
//using EcosystemApp.Filters;
//using Exceptions;
//using Domain.Entities;
using System.Collections.Generic;
//using AppLogic.UseCases;
using System.Linq.Expressions;
using System.Security.Policy;

namespace EcosystemApp.Controllers
{
    public class SpeciesController : Controller
    {

        public IWebHostEnvironment WHE { get; set; }
        public string? ApiURL { get; set; }

        public SpeciesController(IWebHostEnvironment whe, IConfiguration conf)
        {
            WHE = whe;
            ApiURL = conf.GetValue<string>("ApiURL");
        }
        public ActionResult Index(string? option, int? optionParam1, int? optionParam2)
        {
            IEnumerable<SpeciesDTO> species = new List<SpeciesDTO>();
            string url = $"{ApiURL}api/Species";
            try
            {
                if (option == "Cientific")
                {
                    url = $"{ApiURL}api/Species/OrderByScientific";
                }
                else if (option == "Extintion")
                {
                    url = $"{ApiURL}api/Species/Endangered";
                }
                else if (option == "Weight" && optionParam1 != null && optionParam2 != null)
                {
                    int min = optionParam1.Value;
                    int max = optionParam2.Value;
                    url = $"{ApiURL}api/Species/Weight/{min}/{max}";
                }
                else if (option == "eco" && optionParam1 != null)
                {
                    int idEco = optionParam1.Value;
                    url = $"{ApiURL}api/Species/byEco/{idEco}";
                }
                else
                {
                    url = $"{ApiURL}api/Species";
                }

                HttpResponseMessage response = Global.GetResponse(url);
                string body = Global.GetContent(response);
                species = JsonConvert.DeserializeObject<List<SpeciesDTO>>(body);

                if (species != null && species.Count() > 0)
                {
                    return View(species);
                }
                else
                {
                    ViewBag.Error = "No se encontraron Especies.";
                    return View(species);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un error";
                return View(species);
            }
        }

        public ActionResult ListUninhabitableEcos(int id)
        {
            try
            {
                string url = $"{ApiURL}api/Ecosystem/UninhabitableEcos/{id}";
                HttpResponseMessage response = Global.GetResponse(url);
                string body = Global.GetContent(response);
                List<EcosystemDTO> ecos = JsonConvert.DeserializeObject<List<EcosystemDTO>>(body);


                return View(ecos);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Error inesperado a la hora de traer los ecosistemas";
                ModelState.AddModelError(string.Empty, ViewBag.Error);
                return RedirectToAction(nameof(Index));
            }
            
        }

        [Private]
        public ActionResult Delete(int id)
        {
            return View(id);
        }

        [Private]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            string url = $"{ApiURL}api/Species/{id}";

            HttpResponseMessage response = Global.Delete(url);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                string error = Global.GetContent(response);
                ViewBag.Error = error;
                return View();
            }
        }

        private VMSpecies GenerateModelPost()
        {
            string urlEcos = $"{ApiURL}api/Ecosystem/";
            string urlThreats = $"{ApiURL}api/Threat/";

            string bodyEcos = Global.GetContent(Global.GetResponse(urlEcos));
            string bodyThreats = Global.GetContent(Global.GetResponse(urlThreats));

            IEnumerable<EcosystemDTO> ecos = JsonConvert.DeserializeObject<List<EcosystemDTO>>(bodyEcos);
            IEnumerable<ThreatDTO> threats = JsonConvert.DeserializeObject<List<ThreatDTO>>(bodyThreats);


            VMSpecies vm = new() { Threats = threats, IdSelectedThreats = new List<int>(), Ecosystems = ecos, IdSelectedEcos = new List<int>() };
            return vm;
        }

        [Private]
        public ActionResult AddSpecies()
        {
            return View(GenerateModelPost());
        }

        // POST: SpeciesController/Create
        [Private]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSpecies(VMSpecies model)
        {
            try
            {
                List<ThreatDTO> threats = new List<ThreatDTO>();

                model.IdSelectedThreats.ForEach(threat => {
                    string urlThreat = $"{ApiURL}api/Threat/{threat}";
                    string bodyThreat = Global.GetContent(Global.GetResponse(urlThreat));
                    ThreatDTO t = JsonConvert.DeserializeObject<ThreatDTO>(bodyThreat);
                    threats.Add(t);
                });
                model.Species.Threats = threats;

                List<EcosystemDTO> ecos = new List<EcosystemDTO>();

                model.IdSelectedEcos.ForEach(eco => {
                    string urlEcosystem = $"{ApiURL}api/Ecosystem/{eco}";
                    string bodyEcosystem = Global.GetContent(Global.GetResponse(urlEcosystem));
                    EcosystemDTO t = JsonConvert.DeserializeObject<EcosystemDTO>(bodyEcosystem);
                    ecos.Add(t);
                });
                model.Species.Ecosystems = ecos;

                string urlConservation = $"{ApiURL}api/Conservation?sec={model.Species.Security}";
                string bodyConservation = Global.GetContent(Global.GetResponse(urlConservation));
                ConservationDTO cons = JsonConvert.DeserializeObject<ConservationDTO>(bodyConservation);
                model.Species.Conservation = cons;

                FileInfo fi = new(model.ImgSpecies.FileName);
                string ext = fi.Extension;
                model.Species.ImgRoute = "placeholder";

                if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                {

                    string url = $"{ApiURL}api/Species";
                    HttpResponseMessage response1 = Global.PostAsJson(url, model.Species);
                    string body2 = Global.GetContent(response1);
                    if (response1.IsSuccessStatusCode)
                    {
                        //OBTENGO EL ID GENERADO   
                        string body = Global.GetContent(response1);
                        SpeciesDTO created = JsonConvert.DeserializeObject<SpeciesDTO>(body);
                        int? generated_id = created.Id;

                        //GUARDO LA IMAGEN LOCALMENTE
                        string imgName = generated_id + ext;

                        string routeDir = WHE.WebRootPath;
                        string route = Path.Combine(routeDir, "img/Species", imgName);

                        FileStream fs = new(route, FileMode.Create);
                        model.ImgSpecies.CopyTo(fs);
                        fs.Flush();
                        fs.Close();

                        //ACTUALIZO EL NOMBRE DE IMAGEN DEL PAIS DADO DE ALTA
                        created.ImgRoute = imgName;

                        HttpResponseMessage response2 = Global.PutAsJson(url, created);

                        if (response2.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            string error1 = Global.GetContent(response2);
                            ViewBag.Error = error1;
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "El tipo de imagen debe ser png, jpg o jpeg.";
                    ModelState.AddModelError(string.Empty, ViewBag.Error);
                    return View(GenerateModelPost());
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
                return View(GenerateModelPost());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
                return View(GenerateModelPost());
            }
            return View(GenerateModelPost());
        }


        //[Private]
        //public ActionResult AssignEcosystem(int id)
        //{
        //    try
        //    {
        //        Species species = FindUC.Find(id);
        //        IEnumerable<Ecosystem> ecos = ListEcosystemUC.ListUninhabitableEcos(id);
        //        if (species == null)
        //        {
        //            ViewBag.Error = "La espcie con el id " + id + " no existe";
        //        }
        //        VMSpecies vm = new VMSpecies() { Species = species, Ecosystems = ecos, IdSelectedThreats = new List<int>(), };
        //        return View(vm);
        //    }
        //    catch (Exception ex)
        //    {
        //        Species species = FindUC.Find(id);
        //        IEnumerable<Ecosystem> ecos = ListEcosystemUC.ListUninhabitableEcos(id);
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        VMSpecies vm = new VMSpecies() { Species = species, Ecosystems = ecos, IdSelectedThreats = new List<int>(), };
        //        return View(vm);

        //    }

        //}

        //[Private]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AssignEcosystem(VMSpecies s, int speciesId)
        //{
        //    try
        //    {
        //        Species species = FindUC.Find(speciesId);
        //        foreach (int eco in s.IdSelectedEcos) { species.Ecosystems.Add(FindEcosystemUC.Find(eco)); };
        //        UpdateSpeciesUC.UpdateSpecies(species);
        //    }
        //    catch (Exception ex)
        //    {
        //        Species species = FindUC.Find(speciesId);
        //        IEnumerable<Ecosystem> ecos = ListEcosystemUC.ListUninhabitableEcos(speciesId);
        //        VMSpecies vm = new VMSpecies() { Species = species, Ecosystems = ecos, IdSelectedThreats = new List<int>(), };
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        ViewBag.Error = ex.Message;
        //        return View(vm);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
        // public IActionResult Details() { return View(); }





        //public ActionResult Edit(int id) { return View(id); }

        //// POST: SpeciesController/Edit
        //[Private]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //[Private]
        //public ActionResult Delete(int id)
        //{
        //    Species species = FindUC.Find(id);
        //    if (species == null)
        //    {
        //        ViewBag.Error = "El cliente con el id " + id + " noexiste";
        //    }
        //    return View(species);
        //}

        //// POST: SpeciesController/Delete/5
        //[Private]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(Species s)
        //{
        //    try
        //    {
        //        if (s != null)
        //        {
        //            RemoveUC.Remove(s);
        //            return RedirectToAction("Index", "Species");
        //        }
        //        else throw new InvalidOperationException("No se encontró la specie que desea eliminar.");
        //    }
        //    catch (SpeciesException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        return View("Index");
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        return View("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ViewBag.Error = ex.Message);
        //        return View("Index");
        //    }
        //}
    }
}
