using Library.WebAPI.Data;
using Library.WebAPI.Entities;
using Library.WebAPI.Services.Interfaces;

namespace Library.WebAPI.Services
{
    public class RentService : IRentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> BookExists(int bookId)
        {
            return await _unitOfWork.BookRepository.BookExistsAsync(bookId);
        }

        public async Task<bool> CheckBookStatus(int bookId)
        {
            var bookForCheck = await _unitOfWork.BookRepository.GetBookByIdAsync(bookId);
            if (!bookForCheck.IsAvailable)
            {
                return false;
            }
            return true;
        }

        public async Task StartRentAsync(Rent rentToAdd)
        {

            foreach (Book book in rentToAdd.Books)
            {
                await SetBookStatus(book.Id);
            }

            _unitOfWork.RentRepository.AddRent(rentToAdd);

            _unitOfWork.Complete();
        }

        private async Task SetBookStatus(int bookId)
        {
            await _unitOfWork.BookRepository.SetBookStatusAsync(bookId);
            _unitOfWork.Complete();
        }
    }
}