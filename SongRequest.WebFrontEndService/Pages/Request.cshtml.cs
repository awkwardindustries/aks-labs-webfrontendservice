using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SongRequest.WebFrontEndService.Models;

namespace SongRequest.WebFrontEndService.Pages
{
    public class RequestModel : PageModel
    {
        [BindProperty]
        public Models.SongRequest SongRequest { get; set; }

        [BindProperty]
        public Feedback Feedback { get; set; }

        private HttpClient _httpClient;

        public RequestModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                this.Feedback = new Feedback(FeedbackLevel.Error, "Please correct errors below");
                return Page();
            }


            dynamic requestBodyObject = new ExpandoObject();
            requestBodyObject.Artist = this.SongRequest.Artist;
            requestBodyObject.Title = this.SongRequest.Title;
            requestBodyObject.Genre = Enum.GetName(typeof(Models.GenreOptions), this.SongRequest.Genre);
            requestBodyObject.RequestDate = DateTimeOffset.Now;
            var jsonRequestBody = JsonConvert.SerializeObject(requestBodyObject);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{Environment.GetEnvironmentVariable("SONGREQUEST_APISERVICE_BASE_URL")}api/SongRequests");
            httpRequest.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                this.Feedback = new Feedback(FeedbackLevel.Error, 
                    $"There was an error sending your request. [{response.StatusCode}: {response.ReasonPhrase}]");
                return Page();
            }

            this.Feedback = new Feedback(FeedbackLevel.Info,
                $"Successfully requested: {JsonConvert.SerializeObject(this.SongRequest, Formatting.Indented)}");

            // Clear form but stay on page
            ModelState.Clear();
            this.SongRequest = null;
            return Page();
        }
    }
}