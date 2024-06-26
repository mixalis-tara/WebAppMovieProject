﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppMovieProject.Models
{
    [ModelMetadataType(typeof(MovieMetadata))]
    public partial class Movie
    {

    }

    [ModelMetadataType(typeof(ActorMetadata))]
    public partial class Actor
    {

    }

    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {

    }

    [ModelMetadataType(typeof(StreamingPlatformMetadata))]
    public partial class StreamingPlatform
    {

    }

    [ModelMetadataType(typeof(MovieViewModelMetadata))]
    public partial class MovieViewModel
    {

    }

}
