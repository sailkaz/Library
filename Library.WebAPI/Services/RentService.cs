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

        public async Task StartRentAsync(Rent rentToAdd, int bookId)
        {
            //foreach (Book book in rentToAdd.Books) w pierwszej wersji
            //{
            //    var bookForRent = await _unitOfWork.BookRepository.GetBookByIdAsync(book.Id);
            //    bookForRent.IsAvailable = false;
            //    _unitOfWork.Complete();
            //};

            //foreach (Book book in rentToAdd.Books) w drugiej wersji
            //{
            //    SetBookStatus(book.Id);
            //}

            var bookToRent = await _unitOfWork.BookRepository.GetBookByIdAsync(bookId); /* linie 45, 46 i 53 nie są obecne w wersji 1 i 2 */
            rentToAdd.Books.Add(bookToRent);
            rentToAdd.RentDate = DateTime.Today;
            rentToAdd.ReturnDate = rentToAdd.RentDate.AddMonths(1);
            _unitOfWork.RentRepository.AddRent(rentToAdd);

            _unitOfWork.Complete();

            SetBookStatus(rentToAdd.Books);
        }

        //private async Task SetBookStatus(int bookId) w drugiej wersji
        //{
        //    var bookForRent = await _unitOfWork.BookRepository.GetBookByIdAsync(bookId);
        //    bookForRent.IsAvailable = false;
        //    _unitOfWork.Complete();
        //}

        /* Poniższa metoda nie występuje w wersji 1 i 2 */
        private async Task SetBookStatus(List<Book> books)
        {
            foreach (Book book in books) 
            {
                var bookForRent = await _unitOfWork.BookRepository.GetBookByIdAsync(book.Id);
                bookForRent.IsAvailable = false;
                _unitOfWork.Complete();
            }
        }
    }
}