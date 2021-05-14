using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using memes.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

using System.Text.Json.Serialization;


namespace memes.Controllers
{
    public class MemesController : Controller
    {
        public MemesController()
        {

        }

        public async Task<IActionResult> GetMemes()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            var msg = await client.GetStringAsync("https://api.imgflip.com/get_memes");
            
            Root DeserializedObject = JsonSerializer.Deserialize<Root>(msg);
            
            
      
            return View(DeserializedObject.data.memes);
        }

        public IActionResult Generate(string id, string url)
        {
            ViewBag.id = id;
            ViewBag.url = url;
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> Generate(string caption1, string caption2, string id)
        {            
            HttpClient client = new HttpClient();             

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;

            //Need to create own account and add details to code below in order to create memes.

            httpRequestMessage.RequestUri = new Uri("https://api.imgflip.com/caption_image?template_id=" + id 
                + "&username=[username]&password=[password]=" + caption1 
                + "&text1=" + caption2);
            
            HttpResponseMessage resp = await client.SendAsync(httpRequestMessage);
            string result = resp.Content.ReadAsStringAsync().Result;
            GeneratedRoot g = JsonSerializer.Deserialize<GeneratedRoot>(result);
            
            return RedirectToAction("Result",new { url=g.data.url});
            
  
            
            
        }

        public IActionResult Result(string url ){
            ViewBag.url = url;
            return View();
        }
        
    }
}
