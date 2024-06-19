using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppMovieProject.models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

   
    public DateTime? ReleaseDate { get; set; }

    public decimal? ImdbRating { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? WatchedDate { get; set; }

    public int? PlatformId { get; set; }

    public virtual Streamingplatform? Platform { get; set; }

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
