using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.WebAPI.Entities
{
    public class Rent
    {
        public Rent() 
        {
            Books = new List<Book>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }

        [ForeignKey(nameof(Reader))]
        public int ReaderId { get; set; }

        public Reader? Reader { get; set; }

        [ForeignKey(nameof(Librarian))]
        public int LibrarianId { get; set; }

        public Librarian? Librarian { get; set; }

        public List<Book> Books { get; set;}
    }
}
