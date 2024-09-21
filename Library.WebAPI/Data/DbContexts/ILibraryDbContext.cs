using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Data.DbContexts
{
    public interface ILibraryDbContext
    {
        DbSet<Author>? Authors { get; set; }
        DbSet<Book>? Books { get; set; }
        DbSet<Librarian>? Librarians { get; set; }
        DbSet<Reader>? Readers { get; set; }
        DbSet<Rent>? Rents { get; set; }
        DbSet<BookRent> BookRents { get; set; }
        int SaveChanges();
    }
}
