using Library.WebAPI.Entities;

namespace Library.WebAPI.Models
{
    public class AuthorWithBooksDto
    {
        public AuthorWithBooksDto()
        {
            Books = new List<BookDto>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public List<BookDto> Books { get; set; }
    }
}