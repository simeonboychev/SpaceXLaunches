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
    public class SpaceXController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> Upcoming(int page)
        //{

        //}
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

            var viewModels = JsonConvert.DeserializeObject<SpaceX>(myJsonAsString);
            //var html = await ControllerExtensions.RenderViewAsync(this,"Details", viewModels,false);

            var sb = new StringBuilder();

            sb.Append(@"<html>
<head>
    <meta charset='utf-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <title> - SpaceXproj</title>
    <link rel='stylesheet' href='/lib/bootstrap/dist/css/bootstrap.min.css' />
    <link rel='stylesheet' href='/css/site.css' />
</head>
<body>
    <h1>Details</h1>
    <div>
        <h4>SpaceX</h4>
        <hr />
        <dl class='row'>
            <dt class='col-sm-2'>
                Flight_number
            </dt>
            <dd class='col-sm-10'>
                90
            </dd>
            <dt class='col-sm-2'>
                Mission_name
            </dt>
            <dd class='col-sm-10'>
                Starlink 4
            </dd>
            <dt class='col-sm-2'>
                Details
            </dt>
            <dd class='col-sm-10'>
This mission will launch the fourth batch of Starlink version 1.0 satellites, from SLC-40, Cape Canaveral AFS. It is the fifth Starlink launch overall. The satellites will be delivered to low Earth orbit and will spend a few weeks maneuvering to their operational altitude of 550 km. The booster for this mission is expected to land on OCISLY.            </dd>
            <dt class='col-sm-2'>
                Launch_date_utc
            </dt>
            <dd class='col-sm-10'>
                2020-02-14
            </dd>
        </dl>
            <img src='https://images2.imgbox.com/9a/96/nLppz9HW_o.png' alt='Mission patch'>
    </div>
    <div>
    </ div >
</ body >
</ html > ");
            

            var Renderer = new HtmlToPdf();
            var PDF = Renderer.RenderHtmlAsPdf(sb.ToString());
            var OutputPath = "HtmlToPDF.pdf";
            PDF.SaveAs(OutputPath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(@"HtmlToPDF.pdf");
            
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "pesho.pdf");
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
