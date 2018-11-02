using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySpotifyPlayer.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MySpotifyPlayer.Controllers
{
    public class HomeController : Controller
    {

        private User user;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogedIn()
        {
            // You are here cause you were correctly authenticated with Spotify, so let's get some data from you...
            var url = string.Format("https://api.spotify.com/v1/me");

            var client = new RestClient(url);
            client.Proxy = new WebProxy("192.168.80.27", 9700);
            client.Proxy.Credentials = CredentialCache.DefaultCredentials;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", SpotifyController.authToken.access_token));
            IRestResponse response = client.Execute(request);

            user = JsonConvert.DeserializeObject<User>(response.Content);

            ViewData["User"] = user;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
