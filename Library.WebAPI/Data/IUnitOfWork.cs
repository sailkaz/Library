using Library.WebAPI.Data.Repositories.Interfaces;

namespace Library.WebAPI.Data
{
    public interface IUnitOfWork
    {
        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }
        public ILibrarianRepository LibrarianRepository { get; }
        public IReaderRepository ReaderRepository { get; }
        public IRentRepository RentRepository { get; }

        void Complete();
    }
}
