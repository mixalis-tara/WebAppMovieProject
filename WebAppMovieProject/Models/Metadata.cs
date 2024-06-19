using System.ComponentModel.DataAnnotations;

namespace WebAppMovieProject.Models
{
    public partial class MovieMetadata
    {
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Imdb Rating")]
        public decimal? ImdbRating { get; set; }

        [Display(Name = "Watched Date")]
        public DateTime? WatchedDate { get; set; }
    }
}
