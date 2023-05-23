using Api3il.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppl3il.Models;


namespace WebAppl3il.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        string baseURL = "https://localhost:7166";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }


        public async Task<IActionResult> Index()
        {

            List<Etudiant> Etudiants = new List<Etudiant>();
            List<Groupe> Groupes = new List<Groupe>();
            List<Promotion> Promotions = new List<Promotion>();
            List<Emargement> Emargements = new List<Emargement>();

            using (var Client = new HttpClient())

            {
                Client.BaseAddress = new Uri(baseURL);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData1 = await Client.GetAsync("api/Etudiants");
                HttpResponseMessage getData2 = await Client.GetAsync("api/Groupes");
                HttpResponseMessage getData3 = await Client.GetAsync("api/Promotions");
                HttpResponseMessage getData4 = await Client.GetAsync("api/Emargements");

                if (getData1.IsSuccessStatusCode && getData2.IsSuccessStatusCode &&
                    getData3.IsSuccessStatusCode && getData4.IsSuccessStatusCode)
                {
                    string results1 = await getData1.Content.ReadAsStringAsync();
                    Etudiants = JsonConvert.DeserializeObject<List<Etudiant>>(results1);

                    string results2 = await getData2.Content.ReadAsStringAsync();
                    Groupes = JsonConvert.DeserializeObject<List<Groupe>>(results2);

                    string results3 = await getData3.Content.ReadAsStringAsync();
                    Promotions = JsonConvert.DeserializeObject<List<Promotion>>(results3);

                    string results4 = await getData4.Content.ReadAsStringAsync();
                    Emargements = JsonConvert.DeserializeObject<List<Emargement>>(results4);
                }
                else
                {
                    Console.WriteLine("Erreur lors de l'appel à l'API web");
                }

                ViewData["Etudiant"] = Etudiants;
                ViewData["Groupe"] = Groupes;
                ViewData["Promotion"] = Promotions;
                ViewData["Emargement"] = Emargements;

                return View();
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Connexion()
        {
            return View();
        }

       
    }
}

