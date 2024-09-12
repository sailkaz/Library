using Library.WebAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Models
{
    public class BookForUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}
