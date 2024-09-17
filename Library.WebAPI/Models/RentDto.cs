using Library.WebAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.WebAPI.Models
{
    public class RentDto
    {
        public RentDto() 
        {
            Books = new List<BookWithoutDetailsDto>();
        }
        public int Id { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int ReaderId { get; set; }

        public ReaderDto? Reader { get; set; }

        public int LibrarianId { get; set; }

        public LibrarianDto? Librarian { get; set; }

        public List<BookWithoutDetailsDto> Books { get; set; }
    }
}
