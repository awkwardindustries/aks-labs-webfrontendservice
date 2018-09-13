using System;

namespace SongRequest.WebFrontEndService.Models
{
    public class SongRequestForDisplay
    {
        public string Artist { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public DateTimeOffset RequestDate { get; set; }
    }
}
