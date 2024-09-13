using Library.WebAPI.Entities;

namespace Library.WebAPI.Models
{
    public class RentForCreationDto
    {
        public RentForCreationDto()
        {
            Books = new List<BookForRentDto>();
        }
        public int ReaderId { get; set; }

        public int LibrarianId { get; set; }

        public List<BookForRentDto> Books { get; set; }
    }
}