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

        public DateTime ReturnDate { get; private set; }
        public object RentDate { get; private set; }

        public RentRepository(ILibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRent(Rent rentToAdd)
        {
            var rentDb = new Rent
            {
                LibrarianId = rentToAdd.LibrarianId,
                ReaderId = rentToAdd.ReaderId,
                RentDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddMonths(1)
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



        

    }
}
