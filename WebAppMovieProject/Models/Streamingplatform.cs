using System;
using System.Collections.Generic;

namespace WebAppMovieProject.models;

public partial class Streamingplatform
{
    public int PlatformId { get; set; }

    public string PlatformName { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
