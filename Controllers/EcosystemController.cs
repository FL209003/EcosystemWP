using Microsoft.AspNetCore.Mvc;
using EcosystemApp.Filters;
using EcosystemApp.Models;
using DTOs;
using Newtonsoft.Json;
using EcosystemApp.Globals;

namespace EcosystemApp.Controllers
{
    public class EcosystemController : Controller
    {
        public IWebHostEnvironment WHE { get; set; }
        public string? ApiURL { get; set; }

        public EcosystemController(IWebHostEnvironment whe, IConfiguration conf)
        {
            WHE = whe;
            ApiURL = conf.GetValue<string>("ApiURL");
        }

        public ActionResult Index()
        {
            string urlEco = $"{ApiURL}api/Ecosystem/";

            string bodyEcos = Global.GetContent(Global.GetResponse(urlEco));            

            if (Global.GetResponse(urlEco).IsSuccessStatusCode)
            {
                IEnumerable<EcosystemDTO> ecos = JsonConvert.DeserializeObject<List<EcosystemDTO>>(bodyEcos);
                return View(ecos);
            }
            else
            {
                ViewBag.Error = bodyEcos;
                return View(new List<EcosystemDTO>());
            }
        }

        // public IActionResult Details() { return View(); }
        private VMEcosystem GenerateModelPost()
        {
            string urlCountries = $"{ApiURL}api/Country/";
            string urlThreats = $"{ApiURL}api/Threat/";

            string bodyCountries = Global.GetContent(Global.GetResponse(urlCountries));
            string bodyThreats = Global.GetContent(Global.GetResponse(urlThreats));

            IEnumerable<CountryDTO> countries = JsonConvert.DeserializeObject<List<CountryDTO>>(bodyCountries);
            IEnumerable<ThreatDTO> threats = JsonConvert.DeserializeObject<List<ThreatDTO>>(bodyThreats);

            return new VMEcosystem() { Countries = countries, IdSelectedCountry = new List<int>(), Threats = threats, IdSelectedThreats = new List<int>() };
        }
        [Private]
        public ActionResult AddEcosystem()
        {  
            return View(GenerateModelPost());
        }

        // POST: EcosystemController/Create
        [Private]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEcosystem(VMEcosystem model)
        {
            if (HttpContext.Session.GetString("role") != null)
            {
                try
                {

                    FileInfo fi = new(model.ImgEco.FileName);
                    string ext = fi.Extension;

                    //get conservation
                    string urlConservation = $"{ApiURL}api/Conservation?sec={model.Ecosystem.Security}";
                    string bodyConservation = Global.GetContent(Global.GetResponse(urlConservation));
                    ConservationDTO cons = JsonConvert.DeserializeObject<ConservationDTO>(bodyConservation);
                    model.Ecosystem.Conservation = cons;

                    List<CountryDTO> countries = new List<CountryDTO>();

                    model.IdSelectedCountry.ForEach(country => {
                        string urlCountry = $"{ApiURL}api/Country/{country}";
                        string bodyCountry = Global.GetContent(Global.GetResponse(urlCountry));
                        CountryDTO c = JsonConvert.DeserializeObject<CountryDTO>(bodyCountry);
                        countries.Add(c);
                    });
                    model.Ecosystem.Countries = countries;

                    List<ThreatDTO> threats = new List<ThreatDTO>();

                    model.IdSelectedThreats.ForEach(threat => {
                        string urlThreat = $"{ApiURL}api/Threat/{threat}";
                        string bodyThreat = Global.GetContent(Global.GetResponse(urlThreat));
                        ThreatDTO t = JsonConvert.DeserializeObject<ThreatDTO>(bodyThreat);
                        threats.Add(t);
                    });
                    model.Ecosystem.Threats = threats;

                    model.Ecosystem.ImgRoute = "placeholder";
                    model.Ecosystem.Species = new List<SpeciesDTO>();

                    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                    {
                        //HAGO EL ALTA POR WEBAPI
                        string url = $"{ApiURL}api/Ecosystem";
                        string token = HttpContext.Session.GetString("token");
                        HttpResponseMessage response1 = Global.PostAsJson(url, model.Ecosystem, token);

                        //cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);                        

                        if (response1.IsSuccessStatusCode)
                        {
                            //OBTENGO EL ID GENERADO   
                            string body = Global.GetContent(response1);
                            EcosystemDTO created = JsonConvert.DeserializeObject<EcosystemDTO>(body);
                            int generated_id = created.Id;

                            //GUARDO LA IMAGEN LOCALMENTE
                            string imgName = generated_id + ext;

                            string routeDir = WHE.WebRootPath;
                            string route = Path.Combine(routeDir,"img/Ecosystems", imgName);

                            FileStream fs = new(route, FileMode.Create);
                            model.ImgEco.CopyTo(fs);
                            fs.Flush();
                            fs.Close();

                            //ACTUALIZO EL NOMBRE DE IMAGEN DEL PAIS DADO DE ALTA
                            created.ImgRoute = imgName;
                            HttpResponseMessage response2 = Global.PutAsJson(url, created, token);

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
                        else
                        {
                            string error = Global.GetContent(response1);
                            ViewBag.Error = error;
                            return View(GenerateModelPost());
                        }
                    }
                    else
                    {
                        ViewBag.Error = "El tipo de imagen debe ser png o jpg.";
                        return View(GenerateModelPost());
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "Ocurrió un error inesperado, no se ha podido realizar el alta del ecosistema.";
                }
                return View(GenerateModelPost());
            }
            else
            {
                return RedirectToAction("Login", "User");
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
            string url = $"{ApiURL}api/Ecosystem/{id}";
            string token = HttpContext.Session.GetString("token");
            HttpResponseMessage response = Global.Delete(url, token);

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
