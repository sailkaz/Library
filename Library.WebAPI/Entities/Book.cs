using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.WebAPI.Entities
{
    public class Book
    {
        public Book() 
        {
            Authors = new List<Author>();
            Rents = new List<Rent>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public List<Author> Authors { get; set; }
        public List<Rent> Rents { get; set; }
    }
}
