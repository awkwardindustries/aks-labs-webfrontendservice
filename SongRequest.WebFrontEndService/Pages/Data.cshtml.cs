using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SongRequest.WebFrontEndService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SongRequest.WebFrontEndService.Pages
{
    public class DataModel : PageModel
    {
        [BindProperty]
        public IEnumerable<SongRequestForDisplay> SongRequests { get; set; }

        [BindProperty]
        public IEnumerable<RequestedSong> RequestedSongs { get; set; }

        [BindProperty]
        public Feedback Feedback { get; set; }

        private readonly HttpClient _httpClient;

        public DataModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync()
        {
            await FetchData();
        }

        public async Task<IActionResult> OnPostRefreshAsync()
        {
            await FetchData();
            return Page();
        }

        private async Task FetchData()
        {
            var result = await _httpClient.GetAsync($"{Environment.GetEnvironmentVariable("SONGREQUEST_APISERVICE_BASE_URL")}api/SongRequests?top=20");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                this.SongRequests = JsonConvert.DeserializeObject<SongRequestForDisplay[]>(content);
            }
            else
            {
                this.Feedback = new Feedback(FeedbackLevel.Warning, "Unable to refresh the data.");
                this.SongRequests = new SongRequestForDisplay[0];
            }

            result = await _httpClient.GetAsync($"{Environment.GetEnvironmentVariable("SONGREQUEST_APISERVICE_BASE_URL")}api/SongRequests/TopRequests");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                this.RequestedSongs = JsonConvert.DeserializeObject<RequestedSong[]>(content);
            }
            else
            {
                this.Feedback = new Feedback(FeedbackLevel.Warning, "Unable to refresh the data.");
                this.RequestedSongs = new RequestedSong[0];
            }
        }
    }
}