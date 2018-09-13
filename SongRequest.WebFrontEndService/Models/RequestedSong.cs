using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRequest.WebFrontEndService.Models
{
    public class RequestedSong
    {
        public string Artist { get; set; }

        public string Title { get; set; }

        public int NumRequests { get; set; }
    }
}
