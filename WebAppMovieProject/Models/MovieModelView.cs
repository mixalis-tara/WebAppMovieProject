using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMovieProject.Models
{
    public partial class MovieViewModel
    {
        public Movie Movie { get; set; }
        public int[] SelectedCategoryIds { get; set; }

        public int[] SelectedActorsIds { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Platforms { get; set; }

        public IEnumerable<SelectListItem> Actors { get; set; }

        public IFormFile ImageFile { get; set; }
        public string CurrentImageName { get; set; }

        public IEnumerable<string> ExistingImages { get; set; }
    }
}
