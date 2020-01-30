using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceXproj.Models;

namespace SpaceXproj.Controllers
{
    public class HomeController : Controller
    {
        private static  int MaxPage;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(int id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var responseMessage = await client.GetAsync("https://api.spacexdata.com/v3/launches/upcoming");

            string myJsonAsString = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                myJsonAsString = await responseMessage.Content.ReadAsStringAsync();
            }
            var numberofships = JsonConvert.DeserializeObject<IEnumerable<SpaceX>>(myJsonAsString).Count();

            int result = numberofships / 5;
            if(numberofships%5!=0)
            {
                MaxPage = result + 1;
            }
            else
            {
                MaxPage = result;
            }
            return View();
        }
        public async Task<IActionResult> Table(int id)
        {

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var responseMessage = await client.GetAsync("https://api.spacexdata.com/v3/launches/upcoming");

            string myJsonAsString = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                myJsonAsString = await responseMessage.Content.ReadAsStringAsync();
            }

            if (id == 0)
            {
                id = 1;
            }
            var collection = JsonConvert.DeserializeObject<IEnumerable<SpaceX>>(myJsonAsString)
                .Skip((id - 1) * 5)
                .Take(5);

            var viewModel = new CollectionModel
            {
                Collection = collection,
                CurrentPage = id,
                LastPage = MaxPage,
                NextPage = id + 1,
                PreviousPage = id - 1
            };

            return View(viewModel);
        }
        [ResponseCache(Duration =3600)]
        public async Task<IActionResult> Privacy(string query)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var responseMessage = await client.GetAsync("https://api.spacexdata.com/v3/launches/upcoming");

            string myJsonAsString = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                myJsonAsString = await responseMessage.Content.ReadAsStringAsync();
            }

            var viewModels = JsonConvert.DeserializeObject<IEnumerable<SpaceX>>(myJsonAsString);

            return View(viewModels);
        }

        public async Task<IActionResult> GetPdf(string id)
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
