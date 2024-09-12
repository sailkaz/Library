using Library.WebAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Models
{
    public class BookDto
    {
        public BookDto()
        {
            Authors = new List<AuthorDto>();
        }
       
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
  
        public string Description { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public List<AuthorDto> Authors { get; set; }
    }
}