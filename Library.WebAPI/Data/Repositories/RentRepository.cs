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

        public void AddRent(Rent rentToAdd)
        {
            //rentToAdd.RentDate = DateTime.Today;
            //rentToAdd.ReturnDate = rentToAdd.RentDate.AddMonths(1);
            _context.Rents.Add(rentToAdd);
        }
    }
}
