using Library.WebAPI.Data.Configurations;
using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Data.DbContexts
{
    public class LibraryDbContext : DbContext, ILibraryDbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Author>? Authors { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Librarian>? Librarians { get; set; }
        public DbSet<Reader>? Readers { get; set; }
        public DbSet<Rent>? Rents { get; set; }
        public DbSet<BookRent> BookRents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "Adam", LastName = "Mickiewicz"},
                new Author { Id = 2, FirstName = "Henryk", LastName = "Sienkiewicz"}
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Pan Tadeusz", IsAvailable = true },
                new Book { Id = 2, Title = "Krzyżacy", IsAvailable = true }
                );

            modelBuilder.Entity<Librarian>().HasData(
                new Librarian { Id = 1, FirstName = "Maria", LastName = "Kowalska"},
                new Librarian { Id = 2, FirstName = "Jan", LastName = "Nowak" }
                );

            modelBuilder.Entity<Reader>().HasData(
                new Reader { Id = 1, FirstName = "Franciszek", LastName = "Michalski"},
                new Reader { Id = 2, FirstName = "Agnieszka", LastName = "Jackowska" }
                );

            modelBuilder.Entity<Rent>().HasData(
                new Rent { Id = 1, LibrarianId = 1, ReaderId = 2, RentDate = new DateTime(2024,02,20), 
                    ReturnDate = new DateTime(2024,03,20) }
                );


            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookRentConfiguration());
        }
    }
}
