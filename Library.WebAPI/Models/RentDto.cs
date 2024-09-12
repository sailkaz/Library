using Library.WebAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.WebAPI.Models
{
    public class RentDto
    {
        public RentDto() 
        {
            Books = new List<BookDto>();
        }
        public int Id { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int ReaderId { get; set; }

        public Reader? Reader { get; set; }

        public int LibrarianId { get; set; }

        public Librarian? Librarian { get; set; }

        public List<BookDto> Books { get; set; }
    }
}
