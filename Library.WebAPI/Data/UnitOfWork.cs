using Library.WebAPI.Data.DbContexts;
using Library.WebAPI.Data.Repositories;
using Library.WebAPI.Data.Repositories.Interfaces;

namespace Library.WebAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILibraryDbContext _context;
        public UnitOfWork(ILibraryDbContext context) 
        {
            _context = context;
            AuthorRepository = new AuthorRepository(context);
            BookRentsRepository = new BookRentsRepository(context);
            BookRepository = new BookRepository(context);
            LibrarianRepository = new LibrarianRepository(context);
            ReaderRepository = new ReaderRepository(context);
            RentRepository = new RentRepository(context);
        }

        public IAuthorRepository AuthorRepository { get; set; }

        public IBookRentsRepository BookRentsRepository { get; set; }

        public IBookRepository BookRepository { get; set; }

        public ILibrarianRepository LibrarianRepository {get; set;}

        public IReaderRepository ReaderRepository {get; set;}

        public IRentRepository RentRepository {get; set;}

        public void Complete() 
        {
            _context.SaveChanges();
        }
    }
}
