using AutoMapper;
using Library.WebAPI.Entities;
using Library.WebAPI.Models;
using Library.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentsController : ControllerBase
    {
        private readonly IRentService _rentService;
        private readonly IMapper _mapper;

        public RentsController(IRentService rentService, IMapper mapper) 
        {
            _rentService = rentService;
            _mapper = mapper;
        }

        [HttpGet ("{rentId}", Name = "GetRent")]
        public async Task<ActionResult> GetRent(int rentId) 
        {
            var response = await _rentService.GetRentAsync(rentId);
            if(response == null)
                return NotFound($"Sorry, no rent with id = {rentId} was found.");

            return Ok(_mapper.Map<RentDto>(response));
        }

        [HttpGet]
        public async Task<ActionResult> GetRentForReader(int readerId)
        {
            var response = await _rentService.GetRentForReaderAsync(readerId);
            if (response == null || !response.Books.Any())
                return NotFound($"Sorry, no active rent for reader with id = {readerId} was found.");

            return Ok(_mapper.Map<RentDto>(response));
        }

        [HttpPost]
        public async Task<ActionResult<RentDto>> StartRent(RentForCreationDto newRent) 
        {
            var currentRent = await _rentService.GetRentForReaderAsync(newRent.ReaderId);
            if (currentRent.IsActive)
                return BadRequest($"Sorry, reader {currentRent.Reader.FirstName} {currentRent.Reader.LastName} hasn't given back all books yet.");

            foreach (BookForRentDto bookForRentDto in newRent.Books)
            {
                if (!await _rentService.BookExists(bookForRentDto.Id))
                    return NotFound($"Sorry, no book with id = {bookForRentDto.Id} was found.");

                if (!await _rentService.CheckBookStatus(bookForRentDto.Id))
                    return BadRequest($"Sorry, book with id = {bookForRentDto.Id} isn't available.");
            }

            var rentToAdd = _mapper.Map<Rent>(newRent);
            await _rentService.StartRentAsync(rentToAdd);

            var rentToReturn = _mapper.Map<RentDto>(rentToAdd);

            return CreatedAtRoute("GetRent",
                new { rentId = rentToAdd.Id },
                rentToReturn);
        }

        [HttpDelete]
        public async Task<ActionResult> CancelRentOfBook(int bookId)
        {
            var bookToCancelRent = await _rentService.GetBookByIdAsync(bookId);

            if(bookToCancelRent == null) 
                return NotFound($"Sorry, no book with id = {bookId} was found.");

            if (bookToCancelRent.IsAvailable == true)
                return BadRequest($"Sorry, book with id = {bookId} is still available.");

            await _rentService.CancelRentOfBookAsync(bookId);

            return Ok($"Rent of book with id = {bookId} has been canceled.");
        }
    }
}
