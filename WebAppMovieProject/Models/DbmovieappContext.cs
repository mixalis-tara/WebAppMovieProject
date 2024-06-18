using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppMovieProject.models;

public partial class DbmovieappContext : DbContext
{
    public DbmovieappContext()
    {
    }

    public DbmovieappContext(DbContextOptions<DbmovieappContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Streamingplatform> Streamingplatforms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=dbmovieapp;user=root;password=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PRIMARY");

            entity.ToTable("actors");

            entity.Property(e => e.ActorId)
                .HasColumnType("int(11)")
                .HasColumnName("actor_id");
            entity.Property(e => e.ActorName)
                .HasMaxLength(255)
                .HasColumnName("actor_name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PRIMARY");

            entity.ToTable("movies");

            entity.HasIndex(e => e.PlatformId, "platform_id");

            entity.Property(e => e.MovieId)
                .HasColumnType("int(11)")
                .HasColumnName("movie_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("image_url");
            entity.Property(e => e.ImdbRating)
                .HasPrecision(4, 1)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("imdb_rating");
            entity.Property(e => e.PlatformId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("platform_id");
            entity.Property(e => e.ReleaseDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("release_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.WatchedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("watched_date");

            entity.HasOne(d => d.Platform).WithMany(p => p.Movies)
                .HasForeignKey(d => d.PlatformId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("movies_ibfk_1");

            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "Movieactor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("movieactors_ibfk_2"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("movieactors_ibfk_1"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId").HasName("PRIMARY");
                        j.ToTable("movieactors");
                        j.HasIndex(new[] { "ActorId" }, "actor_id");
                        j.IndexerProperty<int>("MovieId")
                            .HasColumnType("int(11)")
                            .HasColumnName("movie_id");
                        j.IndexerProperty<int>("ActorId")
                            .HasColumnType("int(11)")
                            .HasColumnName("actor_id");
                    });

            entity.HasMany(d => d.Categories).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "Moviecategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("moviecategories_ibfk_2"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("moviecategories_ibfk_1"),
                    j =>
                    {
                        j.HasKey("MovieId", "CategoryId").HasName("PRIMARY");
                        j.ToTable("moviecategories");
                        j.HasIndex(new[] { "CategoryId" }, "category_id");
                        j.IndexerProperty<int>("MovieId")
                            .HasColumnType("int(11)")
                            .HasColumnName("movie_id");
                        j.IndexerProperty<int>("CategoryId")
                            .HasColumnType("int(11)")
                            .HasColumnName("category_id");
                    });
        });

        modelBuilder.Entity<Streamingplatform>(entity =>
        {
            entity.HasKey(e => e.PlatformId).HasName("PRIMARY");

            entity.ToTable("streamingplatforms");

            entity.Property(e => e.PlatformId)
                .HasColumnType("int(11)")
                .HasColumnName("platform_id");
            entity.Property(e => e.PlatformName)
                .HasMaxLength(100)
                .HasColumnName("platform_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
