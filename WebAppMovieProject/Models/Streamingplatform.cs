﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebAppMovieProject.Models;

public partial class StreamingPlatform
{
    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}