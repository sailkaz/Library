using Library.WebAPI.Controllers;
using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.WebAPI.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasMany(x => x.Rents)
                .WithMany(x => x.Books)
                .UsingEntity<BookRent>(
                x => x.HasOne(x => x.Rent).WithMany().HasForeignKey(x => x.RentId),
                x => x.HasOne(x => x.Book).WithMany().HasForeignKey(x => x.BookId));
        }
    }
}
