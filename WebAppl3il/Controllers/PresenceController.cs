using Api3il.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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

namespace WebApp3il.Controllers
{
    public class PresenceController : Controller
    {
        private readonly ILogger<PresenceController> _logger;

        string baseURL = "https://localhost:7166";
        public PresenceController(ILogger<PresenceController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> IPresenceAsync()
        {
            List<Etudiant> Etudiants = new List<Etudiant>();
            List<Emargement> Presences = new List<Emargement>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData1 = await client.GetAsync("api/Etudiants");
                HttpResponseMessage getData2 = await client.GetAsync("api/Emargements");


                if (getData1.IsSuccessStatusCode)
                {
                    string results1 = getData1.Content.ReadAsStringAsync().Result;
                    Etudiants = JsonConvert.DeserializeObject<List<Etudiant>>(results1);

                    string results2 = getData2.Content.ReadAsStringAsync().Result;
                    Presences = JsonConvert.DeserializeObject<List<Emargement>>(results2);


                }
                else
                {
                    Console.WriteLine("erreur calling web API");
                }

                ViewData["Etudiant"] = Etudiants;
                ViewData["Presence"] = Presences;
            }
            return View();
        }
    }
}