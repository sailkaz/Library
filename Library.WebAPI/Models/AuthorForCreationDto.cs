using Library.WebAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Models
{
    public class AuthorForCreationDto
    {
        [Required(ErrorMessage = "You should provide a Firstname value.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a Lastname value.")]
        public string LastName { get; set; } = string.Empty;
    }
}