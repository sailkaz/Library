using Library.WebAPI.Data;
using Library.WebAPI.Entities;
using Library.WebAPI.Services.Interfaces;

namespace Library.WebAPI.Services
{
    public class LibrarianService : ILibrarianService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LibrarianService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<Librarian>> GetLibrariansAsync()
        {
            return await _unitOfWork.LibrarianRepository.GetLibrariansAsync();
        }

        public async Task<Librarian> GetLibrarianByIdAsync(int id)
        {
            return await _unitOfWork.LibrarianRepository.GetLibrarianByIdAsync(id);
        }

        public void AddLibrarian(Librarian newLibrarian)
        {
            _unitOfWork.LibrarianRepository.Addlibrarian(newLibrarian);
            _unitOfWork.Complete();
        }

        public void UpdateLibrarian()
        {
            _unitOfWork.Complete();
        }

        public void DeleteLibrarian(Librarian librarianToRemove)
        {
            _unitOfWork.LibrarianRepository.DeleteLibrarian(librarianToRemove);
            _unitOfWork.Complete();
        }
    }
}
