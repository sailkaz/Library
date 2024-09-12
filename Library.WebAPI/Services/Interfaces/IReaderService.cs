using Library.WebAPI.Entities;

namespace Library.WebAPI.Services.Interfaces
{
    public interface IReaderService
    {
        Task<IEnumerable<Reader>> GetReadersAsync();
        Task<IEnumerable<Reader>> GetReadersByNameAsync(string lastName);
        Task<Reader> GetReaderAsync(int id, string lastName);
        Task<Reader> GetReaderByIdAsync(int id);
        void AddReader(Reader newReader);
        void UpdateReader();
        void DeleteReader(Reader readerToRemove);
    }
}
