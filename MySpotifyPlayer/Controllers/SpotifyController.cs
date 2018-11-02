using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace MySpotifyPlayer.Controllers
{
    [Route("api/spotify")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private string state;

        private string GenerateRandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        [Route("login")]
        public RedirectResult Login()
        {
            //generate state and save it locally
            var client_id = "93f3bc83b01b433caba41235e75e7ad2";
            state = GenerateRandomString(16);
            var redirect_uri = "https://localhost:5001/api/spotify/callback";
            var scope = "user-read-private user-read-email";
            var response_type = "code";

            var url = string.Format("https://accounts.spotify.com/authorize?client_id={0}&response_type={1}&redirect_uri={2}&scope={3}&state={4}", client_id, response_type, redirect_uri, scope, state);
            return RedirectPermanent(url);
        }

        [HttpGet]
        [Route("callback")]
        public void Callback(string code, string state)
        {
            //compare received state with previously saved sata
        }

    }
}
