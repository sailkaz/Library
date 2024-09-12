using Library.WebAPI.Entities;

namespace Library.WebAPI.Data.Repositories.Interfaces
{
    public interface ILibrarianRepository
    {
        Task<IEnumerable<Librarian>> GetLibrariansAsync();
        Task<Librarian> GetLibrarianByIdAsync(int id);
        void Addlibrarian(Librarian newLibrarian);
        void DeleteLibrarian(Librarian librarianToRemove);
    }
}
