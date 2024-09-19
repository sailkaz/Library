using Library.WebAPI.Data.DbContexts;
using Library.WebAPI.Data.Repositories.Interfaces;
using Library.WebAPI.Entities;
using Library.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Data.Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly ILibraryDbContext _context;


        public RentRepository(ILibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Rent?> GetRentAsync(int rentId)
        {
            return await _context.Rents
                .Include(x => x.Reader)
                .Include(x => x.Librarian)
                .Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Id == rentId);
        }

        public void AddRent(Rent rentToAdd)
        {
            var rentDb = new Rent
            {
                LibrarianId = rentToAdd.LibrarianId,
                ReaderId = rentToAdd.ReaderId,
                RentDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddMonths(1),
                IsActive = true
            };
            _context.Rents.Add(rentDb);


            foreach (var item in rentToAdd.Books)
            {
                _context.BookRents.Add(new BookRent
                {
                    BookId = item.Id,
                    Rent = rentDb,
                });
            }
        }

        public void CancelRentAsync(BookRent bookRent)
        {
            bookRent.Rent.ReturnDate = DateTime.Today;
            bookRent.Rent.IsActive = false;
        }

        public async Task<Rent?> GetRentForReaderAsync(int readerId)
        {
            return await _context.Rents
                .Include(x => x.Books)
                .Include(x => x.Reader)
                .Where(x => x.IsActive == true)
                .FirstOrDefaultAsync(x => x.ReaderId == readerId);
        }
    }
}