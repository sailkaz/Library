using Library.WebAPI.Data;
using Library.WebAPI.Entities;
using Library.WebAPI.Services.Interfaces;

namespace Library.WebAPI.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReaderService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<Reader>> GetReadersAsync()
        {
            return await _unitOfWork.ReaderRepository.GetReadersAsync();
        }

        public async Task<IEnumerable<Reader>> GetReadersByNameAsync(string lastName)
        {
            return await _unitOfWork.ReaderRepository.GetReadersByNameAsync(lastName);
        }

        public async Task<Reader> GetReaderAsync(int id, string lastName)
        {
            return await _unitOfWork.ReaderRepository.GetReaderAsync(id, lastName);
        }

        public async Task<Reader> GetReaderByIdAsync(int id)
        {
            return await _unitOfWork.ReaderRepository.GetReaderByIdAsync(id);
        }

        public void AddReader(Reader newReader)
        {
            _unitOfWork.ReaderRepository.AddReader(newReader);
            _unitOfWork.Complete();
        }

        public void UpdateReader()
        {
            _unitOfWork.Complete();
        }

        public void DeleteReader(Reader readerToRemove)
        {
            _unitOfWork.ReaderRepository.DeleteReader(readerToRemove);
            _unitOfWork.Complete();
        }
    }
}
