using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Api3il.Models;
using Elasticsearch.Net;

namespace WebApplication3il.Controllers
{
    //public class HomeController : Controller
    //{
    //    string Chemin = "https://localhost:7119";

    //    public async Task<IActionResult> ListePresense()
    //    {
    //        DataTable etudiantsDt = new DataTable();
    //        DataTable emargementsDt = new DataTable();

    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri(Chemin);
    //            client.DefaultRequestHeaders.Accept.Clear();
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //            // Appel de l'API pour récupérer les données des étudiants
    //            HttpResponseMessage etudiantsResponse = await client.GetAsync("api/Etudiants");

    //            if (etudiantsResponse.IsSuccessStatusCode)
    //            {
    //                string etudiantsResult = await etudiantsResponse.Content.ReadAsStringAsync();
    //                etudiantsDt = JsonConvert.DeserializeObject<DataTable>(etudiantsResult);
    //            }
    //            else
    //            {
    //                Console.WriteLine("Erreur lors de l'appel à l'API web pour les étudiants");
    //            }

    //            // Appel de l'API pour récupérer les données des émargements
    //            HttpResponseMessage emargementsResponse = await client.GetAsync("api/Emargements");

    //            if (emargementsResponse.IsSuccessStatusCode)
    //            {
    //                string emargementsResult = await emargementsResponse.Content.ReadAsStringAsync();
    //                emargementsDt = JsonConvert.DeserializeObject<DataTable>(emargementsResult);
    //            }
    //            else
    //            {
    //                Console.WriteLine("Erreur lors de l'appel à l'API web pour les émargements");
    //            }
    //        }

    //        // Effectuer la jointure entre les étudiants et les émargements
    //        var query = from etudiant in etudiantsDt.AsEnumerable()
    //                    join emargement in emargementsDt.AsEnumerable() on etudiant.Field<int>("Id") equals emargement.Field<int>("Id")
    //                    select new { Nom = etudiant.Field<string>("Nom"), Prenom = etudiant.Field<string>("Prenom"), Emargement = emargement.Field<string>("DateEmargement") };

    //        var results = query.ToList();

    //        ViewData.Model = results;

    //        return View();
    //    }
    //}

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string baseURL = "https://localhost:7166/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //appel de l'api 
            List<Etudiant> Etudiants = new List<Etudiant>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("api/Etudiants");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    Etudiants = JsonConvert.DeserializeObject<List<Etudiant>>(results);

                }
                else
                {
                    Console.WriteLine("erreur calling web API");
                }

                ViewData["Etudiant"] = Etudiants;


            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}

