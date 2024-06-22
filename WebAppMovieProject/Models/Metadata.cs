using System.ComponentModel.DataAnnotations;

namespace WebAppMovieProject.Models
{
    public partial class MovieMetadata
    {
        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Choice Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Imdb Rating")]
        [Required(ErrorMessage = "Complete Imdb Rating")]
        public decimal? ImdbRating { get; set; }

        [Display(Name = "Watched Date")]
        public DateTime? WatchedDate { get; set; }

        [Display(Name = "Image Name")]
        [Required(ErrorMessage = "Give Image Name")]
        public string ImageUrl { get; set; }

        [Display(Name = "Platform")]
        [Required(ErrorMessage = "Choice Platform")]
        public int? PlatformId { get; set; }
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

    public partial class StreamingPlatformMetadata
    {
        [Display(Name = "Platform Name")]
        [Required(ErrorMessage = "Give Platform Name")]
        public string PlatformName { get; set; }
    }

    public partial class MovieViewModelMetadata
    {
        [Display(Name = "Categories")]
        [Required(ErrorMessage = "Choice Category")]
        public int[] SelectedCategoryIds { get; set; }
    }
}
