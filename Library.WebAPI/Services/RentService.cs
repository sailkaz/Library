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

        public async Task<Rent> GetRentAsync(int rentId)
        {
            return await _unitOfWork.RentRepository.GetRentAsync(rentId);
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

        private async Task SetBookStatus(int bookId)
        {
            await _unitOfWork.BookRepository.SetBookStatusAsync(bookId);
            _unitOfWork.Complete();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _unitOfWork.BookRepository.GetBookByIdAsync(bookId);
        }

        public async Task CancelRentOfBookAsync(int bookId)
        {
            int a = 0;
            await SetBookStatus(bookId);
            var bookRent = await _unitOfWork.BookRentsRepository.GetBookRentAsync(bookId);
            foreach (var item in bookRent.Rent.Books)
            {
                if (item.IsAvailable == true)
                    a++;
            }
            if (a == bookRent.Rent.Books.Count)
                _unitOfWork.RentRepository.CancelRentAsync(bookRent);
            _unitOfWork.Complete();
        }
    }
}