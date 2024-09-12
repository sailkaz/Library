using AutoMapper;
using Library.WebAPI.Entities;
using Library.WebAPI.Models;
using Library.WebAPI.Profiles;
using Library.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper) 
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var response = await _authorService.GetAuthorsAsync();

            if (response == null || !response.Any())
                return NotFound($"Sorry, no author was found.");

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(response));
        }

        [HttpGet("{lastName}")]
        public async Task<ActionResult<IEnumerable<AuthorWithBooksDto>>> GetAuthorsByName(string lastName)
        {
            var response = await _authorService.GetAuthorsByNameAsync(lastName);

            if (response == null || !response.Any())
                return NotFound($"Sorry, no author with last name {lastName} was found.");

            return Ok(_mapper.Map<IEnumerable<AuthorWithBooksDto>>(response));
        }

        [HttpGet("{id}/{lastName}", Name = "GetAuthor")]
        public async Task<ActionResult<AuthorWithBooksDto>> GetAuthor(int id, string lastName)
        {
            var response = await _authorService.GetAuthorAsync(id, lastName);

            if (response == null)
                return NotFound($"Sorry, no author with id = {id} and last name {lastName} was found." +
                    $" It's possible you've entered the wrong id or last name.");

            return Ok(_mapper.Map<AuthorWithBooksDto>(response));
        }

        [HttpPost]
        public ActionResult<AuthorDto> AddAuthor(AuthorForCreationDto newAuthorDto)
        {
            var authorToAdd = _mapper.Map<Author>(newAuthorDto);
            _authorService.AddAuthor(authorToAdd);

            var authorToReturn = _mapper.Map<AuthorDto>(authorToAdd);

            return CreatedAtRoute("GetAuthor",
                new
                {
                    id = authorToAdd.Id,
                    lastName = authorToAdd.LastName
                },
                authorToReturn);
        }

        [HttpPost("{bookId}")]
        public async Task<ActionResult<AuthorDto>> AddAuthorForBook(int bookId, AuthorForCreationDto newAuthorDto)
        {
            if (!await _authorService.BookExists(bookId))
                return NotFound($"No book with id = {bookId} was found");

            var authorToAdd = _mapper.Map<Author>(newAuthorDto);
            await _authorService.AddAuthorForBookAsync(bookId, authorToAdd);

            var authorToReturn = _mapper.Map<AuthorDto>(authorToAdd);

            return CreatedAtRoute("GetAuthor",
                new
                {
                    id = authorToAdd.Id,
                    lastName = authorToAdd.LastName
                },
                authorToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, AuthorForUpdateDto authorForUpdateDto)
        {
            var authorDao = await _authorService.GetAuthorByIdAsync(id);

            if (authorDao == null)
                return NotFound($"Sorry, no author with id = {id} was found.");

            _mapper.Map(authorForUpdateDto, authorDao);

            _authorService.UpdateAuthor();

            return Ok($"Author {authorDao.FirstName} {authorDao.LastName} has been updated.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var authorToRemove = await _authorService.GetAuthorByIdAsync(id);

            if (authorToRemove == null)
                return NotFound($"Sorry, no author with id = {id} was found");

            _authorService.DeleteAuthor(authorToRemove);

            return Ok($"Author {authorToRemove.FirstName} {authorToRemove.LastName} has been deleted.");
        }
    }
}