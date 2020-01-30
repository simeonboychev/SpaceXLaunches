using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpaceXproj.Models;
using SpaceXproj.Utility;
using IronPdf;

namespace SpaceXproj.Controllers
{
    public class SpaceXController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var _apiURL = "https://api.spacexdata.com/v3/launches/" + id;
            var responseMessage = await client.GetAsync(_apiURL);

            string myJsonAsString = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                myJsonAsString = await responseMessage.Content.ReadAsStringAsync();
            }

            var viewModel = JsonConvert.DeserializeObject<SpaceX>(myJsonAsString);
            var html = await ControllerExtensions.RenderViewAsync(this,"Details", viewModel,false);

            var Renderer = new HtmlToPdf();
            var PDF = Renderer.RenderHtmlAsPdf(html);

            byte[] fileContents = PDF.BinaryData;

            return File(fileContents, System.Net.Mime.MediaTypeNames.Application.Octet, $"SpaceXLaunchPlan{viewModel.Flight_number}.pdf");
        }
        public async Task<IActionResult> Details1(int id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var _apiURL = "https://api.spacexdata.com/v3/launches/" + id;
            var responseMessage = await client.GetAsync(_apiURL);

            string myJsonAsString = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                myJsonAsString = await responseMessage.Content.ReadAsStringAsync();
            }

            var viewModels = JsonConvert.DeserializeObject<SpaceX>(myJsonAsString);
            return View("Details", viewModels);
        }
    }
}
