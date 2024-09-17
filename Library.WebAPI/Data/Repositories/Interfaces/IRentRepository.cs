using Library.WebAPI.Entities;

namespace Library.WebAPI.Data.Repositories.Interfaces
{
    public interface IRentRepository
    {
        void AddRent(Rent rentToAdd);
        Task<Rent> GetRentAsync(int rentId);
    }
}
