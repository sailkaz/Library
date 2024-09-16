namespace Library.WebAPI.Entities
{
    public class BookRent
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int RentId { get; set; }
        public Rent Rent { get; set; }
    }
}
