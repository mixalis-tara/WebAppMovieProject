using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMovieProject.Models
{
    public partial class MovieViewModel
    {
        public Movie Movie { get; set; }
        public int[] SelectedCategoryIds { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Platforms { get; set; }
    }
}
