using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.WebAPI.Data.Configurations
{
    public class BookRentConfiguration : IEntityTypeConfiguration<BookRent>
    {
        public void Configure(EntityTypeBuilder<BookRent> builder) 
        {
            builder.ToTable("BooksRentsMap");
        }
    }
}