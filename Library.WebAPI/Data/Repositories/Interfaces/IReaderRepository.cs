using Library.WebAPI.Entities;
using Library.WebAPI.Services;

namespace Library.WebAPI.Data.Repositories.Interfaces
{
    public interface IReaderRepository
    {
        Task<IEnumerable<Reader>> GetReadersAsync();
        Task<IEnumerable<Reader>> GetReadersByNameAsync(string lastName);
        Task<Reader> GetReaderAsync(int id, string lastName);
        Task<Reader> GetReaderByIdAsync(int id);
        void AddReader(Reader newReader);
        void DeleteReader(Reader readerToRemove);
    }
}
