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

    public partial class ActorMetadata
    {
        [Display (Name = "Actor Name")]
        [Required(ErrorMessage = "Give Actor Name")]
        public string ActorName { get; set; }
    }

    public partial class CategoryMetadata
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Give Category Name")]
        public string CategoryName { get; set; } = null!;
    }
}
