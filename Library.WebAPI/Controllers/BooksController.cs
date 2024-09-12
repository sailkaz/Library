using AutoMapper;
using Library.WebAPI.Entities;
using Library.WebAPI.Models;
using Library.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookservice;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper) 
        {
            _bookservice = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{partOfTitle}", Name = "GetBook")]
        public async Task<IActionResult> GetBooksByPartOfTitle(string partOfTitle, bool includeAuthors = false)
        {
            var response = await _bookservice.GetBooksByPartOfTitleAsync(partOfTitle, includeAuthors);

            if(response == null || !response.Any())
                return NotFound($"Sorry, no book was found.");

            if(includeAuthors == false)
                return Ok(_mapper.Map<IEnumerable<BookWithoutDetailsDto>>(response));

            return Ok(_mapper.Map<IEnumerable<BookDto>>(response));
        }

        [HttpPost]
        public async Task<ActionResult<BookWithoutDetailsDto>> AddBookForAuthor(int authorId, BookForCreationDto newBookDto) 
        {
            if (!await _bookservice.AuthorExistsAsync(authorId))
                return NotFound($"Sorry, no author with id = {authorId} was found.");

            var bookToAdd = _mapper.Map<Book>(newBookDto);
            await _bookservice.AddBookForAuthorAsync(authorId, bookToAdd);

            var bookToReturn = _mapper.Map<BookWithoutDetailsDto>(bookToAdd);

            return CreatedAtRoute("GetBook",
                new
                {
                    partOfTitle = bookToAdd.Title,
                },
                bookToReturn
                );
        }

        [HttpPatch("{bookId}")]
        public async Task<ActionResult> PartiallyUpdateBook(int bookId, JsonPatchDocument<BookForUpdateDto> patchDocument )
        {
            var bookDao = await _bookservice.GetBookByIdAsync(bookId);
            if (bookDao == null)
                return NotFound($"Sorry, no book with id = {bookId} was found.");

            var bookToPatch = _mapper.Map<BookForUpdateDto>(bookDao);

            patchDocument.ApplyTo(bookToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(bookToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(bookToPatch, bookDao);

            _bookservice.UpdateBook();

            return NoContent();
        }

        [HttpDelete("{bookId}")]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            var bookToRemove = await _bookservice.GetBookByIdAsync(bookId);

            if (bookToRemove == null)
                return NotFound($"Sorry, no book with id = {bookId} was found.");

            _bookservice.DeleteBook(bookToRemove);

            return Ok($"Book with id = {bookId} and title {bookToRemove.Title} has been deleted.");
        }

    }
}