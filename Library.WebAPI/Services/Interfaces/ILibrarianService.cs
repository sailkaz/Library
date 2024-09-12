using Library.WebAPI.Entities;

namespace Library.WebAPI.Services.Interfaces
{
    public interface ILibrarianService
    {
        Task<IEnumerable<Librarian>> GetLibrariansAsync();
        Task<Librarian> GetLibrarianByIdAsync(int id);
        void AddLibrarian(Librarian newLibrarian);
        void UpdateLibrarian();
        void DeleteLibrarian(Librarian librarianToRemove);
    }
}
