using Microsoft.EntityFrameworkCore;
using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models.SeedData;
using UpgradeProjectSample.Users.Models;

namespace UpgradeProjectSample.Models
{
    public class SampleContext: DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Language)
                .WithMany(L => L.Books)
                .HasForeignKey(b => b.LanguageId);

            modelBuilder.Entity<ApplicationUser>()
                    .Property(w => w.LastUpdateDate)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Language>()
                .HasData(LanguageData.GetAll());
        }
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<SearchedBook> SearchedBooks => Set<SearchedBook>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    }
}