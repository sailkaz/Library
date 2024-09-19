using AutoMapper;
using Library.WebAPI.Entities;
using Library.WebAPI.Models;
using Library.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrariansController : ControllerBase
    {
        private readonly ILibrarianService _librarianService;
        private readonly IMapper _mapper;

        public LibrariansController(ILibrarianService librarianService, IMapper mapper)
        {
            _librarianService = librarianService ?? throw new ArgumentNullException(nameof(librarianService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets all librarians
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibrarianDto>>> GetLibrarians()
        {
            var response = await _librarianService.GetLibrariansAsync();
            if (response == null || !response.Any())
                return NotFound($"Sorry, no Librarian was found.");
            return Ok(_mapper.Map<IEnumerable<LibrarianDto>>(response));
        }

        /// <summary>
        /// Gets a particular librarian by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetLibrarian")]
        public async Task<ActionResult<LibrarianDto>> GetLibrarianById(int id)
        {
            var response = await _librarianService.GetLibrarianByIdAsync(id);
            if(response == null)
                return NotFound($"Sorry, no Librarian with id = {id} was found.");
            return Ok(_mapper.Map<LibrarianDto>(response));
        }

        /// <summary>
        /// Adds a new librarian
        /// </summary>
        /// <param name="newLibrarianDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<LibrarianDto> AddLibrarian(LibrarianForCreationDto newLibrarianDto)
        {
            var librarianToAdd = _mapper.Map<Librarian>(newLibrarianDto);
            _librarianService.AddLibrarian(librarianToAdd);

            var librarianToReturn = _mapper.Map<LibrarianDto>(librarianToAdd);

            return CreatedAtRoute("GetLibrarian",
                new
                {
                    id = librarianToAdd.Id,
                },
                librarianToReturn
                );
        }

        /// <summary>
        /// Updates a particular librarian
        /// </summary>
        /// <param name="id"></param>
        /// <param name="librarianForUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLibrarian(int id, LibrarianForUpdateDto librarianForUpdateDto)
        {
            var librarianDao = await _librarianService.GetLibrarianByIdAsync(id);

            if (librarianDao == null)
                return NotFound($"Sorry, no librarian with id = {id} was found");

            _mapper.Map(librarianForUpdateDto, librarianDao);
            _librarianService.UpdateLibrarian();

            return Ok($"Librarian {librarianDao.FirstName} {librarianDao.LastName} has been updated.");
        }

        /// <summary>
        /// Removes a particular librarian
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLibrarian(int id) 
        {
            var librarianToRemove = await _librarianService.GetLibrarianByIdAsync(id);

            if (librarianToRemove == null)
                return NotFound($"Sorry, no librarian with id = {id} was found.");

            _librarianService.DeleteLibrarian(librarianToRemove);

            return Ok($"Librarian {librarianToRemove.FirstName} {librarianToRemove.LastName} has been deleted.");
        }
    }
}
