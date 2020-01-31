using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceXproj.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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

        public IActionResult Index()
        {
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
            if (MaxPage == 0)
            {
                MaxPage = (JsonConvert.DeserializeObject<IEnumerable<SpaceX>>(myJsonAsString)
                    .Count() / 5)+1;
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

        public IActionResult PageNotFound()
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
