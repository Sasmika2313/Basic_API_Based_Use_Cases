
using System.ComponentModel.DataAnnotations;

namespace HCL1_Movie_Catalogue_API.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Language { get; set; } = string.Empty;

        [Range(1, 600)]
        public int DurationMinutes { get; set; }

        [Range(1888, 2035)]
        public int ReleaseYear { get; set; }
    }
}
