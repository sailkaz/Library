using Library.WebAPI.Data.DbContexts;
using Library.WebAPI.Data.Repositories.Interfaces;
using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {

        private readonly ILibraryDbContext _context;

        public AuthorRepository(ILibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors           
                .OrderBy(x => x.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(string lastName)
        {
            return await _context.Authors
                .Where(x => x.LastName == lastName)
                .Include(x => x.Books)
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }

        public async Task<Author?> GetAuthorAsync(int id, string lastName)
        {
            return await _context.Authors
                .Include(x => x.Books)
                .Where(x => x.Id == id && x.LastName == lastName)
                .FirstOrDefaultAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void AddAuthor(Author newAuthor)
        {
            _context.Authors.Add(newAuthor);
        }

        public void DeleteAuthor(Author authorToRemove)
        {
            _context.Authors.Remove(authorToRemove);
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            return await _context.Authors.AnyAsync(x => x.Id == authorId);
        }
    }
}
