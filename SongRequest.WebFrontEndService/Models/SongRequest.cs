using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongRequest.WebFrontEndService.Models
{
    public enum GenreOptions
    {
        Alternative,
        Classical,
        Country,
        [Display(Name = "Hip Hop / R&B")]
        HipHop,
        Jazz,
        Pop,
        Rap,
        Rock
    }

    public class SongRequest
    {
        [Required]
        public string Artist { get; set; }

        [Required]
        public string Title { get; set; }

        public GenreOptions Genre { get; set; }
    }
}
