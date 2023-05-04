using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lib.Models {
    public partial class LibDbContext : DbContext {

        public static LibDbContext Instance { get; set; } = new LibDbContext();

        public LibDbContext() {
        }

        public LibDbContext(DbContextOptions<LibDbContext> options)
            : base(options) {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<AuthorBook> AuthorBooks { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<FeaturedBook> FeaturedBooks { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<GenreBook> GenreBooks { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Mark> Marks { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<RoleUser> RoleUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=db_lib;password=2232", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Author>(entity => {
                entity.ToTable("author");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Biography)
                    .HasMaxLength(4096)
                    .HasColumnName("biography");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Photo)
                    .HasMaxLength(512)
                    .HasColumnName("photo");
            });

            modelBuilder.Entity<AuthorBook>(entity => {
                entity.ToTable("author_book");

                entity.HasIndex(e => e.AuthorId, "FK_ab_author_idx");

                entity.HasIndex(e => e.BookId, "FK_ab_book_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorBooks)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ab_author");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorBooks)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ab_book");
            });

            modelBuilder.Entity<Book>(entity => {
                entity.ToTable("book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvgRating).HasColumnName("avg_rating");

                entity.Property(e => e.DateOfCreation)
                    .HasMaxLength(128)
                    .HasColumnName("date_of_creation");

                entity.Property(e => e.Description)
                    .HasMaxLength(2048)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Photo)
                    .HasMaxLength(512)
                    .HasColumnName("photo");
            });

            modelBuilder.Entity<FeaturedBook>(entity => {
                entity.ToTable("featured_book");

                entity.HasIndex(e => e.BookId, "FK_fb_book_idx");

                entity.HasIndex(e => e.MarkId, "FK_fb_mark_idx");

                entity.HasIndex(e => e.UserId, "FK_fb_user_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.DateOfAdd)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_add");

                entity.Property(e => e.MarkId).HasColumnName("mark_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.FeaturedBooks)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_fb_book");

                entity.HasOne(d => d.Mark)
                    .WithMany(p => p.FeaturedBooks)
                    .HasForeignKey(d => d.MarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_fb_mark");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FeaturedBooks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_fb_user");
            });

            modelBuilder.Entity<Genre>(entity => {
                entity.ToTable("genre");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<GenreBook>(entity => {
                entity.ToTable("genre_book");

                entity.HasIndex(e => e.BookId, "FK_gb_book_idx");

                entity.HasIndex(e => e.GenreId, "FK_gb_genre_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.GenreBooks)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_gb_book");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.GenreBooks)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_gb_genre");
            });

            modelBuilder.Entity<Like>(entity => {
                entity.ToTable("like");

                entity.HasIndex(e => e.ReviewId, "FK_like_review_idx");

                entity.HasIndex(e => e.UserId, "FK_like_user_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_like_review");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_like_user");
            });

            modelBuilder.Entity<Mark>(entity => {
                entity.ToTable("mark");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Note>(entity => {
                entity.ToTable("note");

                entity.HasIndex(e => e.UserId, "FK_note_user_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(4096)
                    .HasColumnName("content");

                entity.Property(e => e.DateOfCreation)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_creation");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_note_user");
            });

            modelBuilder.Entity<Review>(entity => {
                entity.ToTable("review");

                entity.HasIndex(e => e.BookId, "FK_review_book_idx");

                entity.HasIndex(e => e.UserId, "FK_review_user_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(4096)
                    .HasColumnName("content");

                entity.Property(e => e.DateOfCreation)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_creation");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_review_book");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_review_user");
            });

            modelBuilder.Entity<RoleUser>(entity => {
                entity.ToTable("role_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("user");

                entity.HasIndex(e => e.RoleId, "FK_user_role_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfRegistration)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_registration");

                entity.Property(e => e.Login)
                    .HasMaxLength(256)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
