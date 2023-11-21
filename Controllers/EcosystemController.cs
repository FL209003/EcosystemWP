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
            string urlEco = $"{ApiURL}/api/Ecosistem/";

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

        [Private]
        public ActionResult AddEcosystem()
        {
            string urlCountries = $"{ApiURL}/api/Country/";
            string urlThreats = $"{ApiURL}/api/Threat/";

            string bodyCountries = Global.GetContent(Global.GetResponse(urlCountries));
            string bodyThreats = Global.GetContent(Global.GetResponse(urlThreats));

            IEnumerable<CountryDTO> countries = JsonConvert.DeserializeObject<List<CountryDTO>>(bodyCountries);
            IEnumerable<ThreatDTO> threats = JsonConvert.DeserializeObject<List<ThreatDTO>>(bodyThreats);

            VMEcosystem vmEcosystem = new() { Countries = countries, IdSelectedCountry = new List<int>(), Threats = threats, IdSelectedThreats = new List<int>() };
            return View(vmEcosystem);
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

                    if (ext == ".png" || ext == ".jpg")
                    {
                        //HAGO EL ALTA POR WEBAPI
                        string url = $"{ApiURL}/api/Ecosystem";
                        HttpResponseMessage response1 = Global.PostAsJson(url, model);

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
                            string route = Path.Combine(routeDir, "Ecosystems", imgName);

                            FileStream fs = new(route, FileMode.Create);
                            model.ImgEco.CopyTo(fs);
                            fs.Flush();
                            fs.Close();

                            //ACTUALIZO EL NOMBRE DE IMAGEN DEL PAIS DADO DE ALTA
                            created.ImgRoute = imgName;
                            url = url + "/" + generated_id;
                            HttpResponseMessage response2 = Global.PutAsJson(url, imgName);

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
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.Error = "El tipo de imagen debe ser png o jpg.";
                        return View(model);
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "Ocurrió un error inesperado, no se ha podido realizar el alta del ecosistema.";
                }
                return View(model);
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
            string url = $"{ApiURL}/api/Ecosystem/{id}";

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
