using Library.WebAPI.Data.DbContexts;
using Library.WebAPI.Data.Repositories.Interfaces;
using Library.WebAPI.Entities;
using Library.WebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Data.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly ILibraryDbContext _context;

        public ReaderRepository(ILibraryDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IEnumerable<Reader>> GetReadersAsync()
        {
            return await _context.Readers
                .OrderBy(x => x.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reader>> GetReadersByNameAsync(string lastName)
        {
            return await _context.Readers
                .Where(x => x.LastName == lastName)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Reader?> GetReaderAsync(int id, string lastName)
        {
            return await _context.Readers
                .Where(x => x.Id == id && x.LastName == lastName)
                .FirstOrDefaultAsync();
        }
        public async Task<Reader?> GetReaderByIdAsync(int id)
        {
            return await _context.Readers
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void AddReader(Reader newReader)
        {
            _context.Readers.Add(newReader);
        }

        public void DeleteReader(Reader readerToRemove)
        {
            _context.Readers.Remove(readerToRemove);
        }
    }
}
