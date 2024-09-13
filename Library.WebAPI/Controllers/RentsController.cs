using AutoMapper;
using Library.WebAPI.Entities;
using Library.WebAPI.Models;
using Library.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<RentDto>> StartRent(RentForCreationDto newRent) 
        {
            foreach (BookForRentDto bookForRentDto in newRent.Books)
            {
                if (!await _rentService.BookExists(bookForRentDto.Id))
                    return NotFound($"Sorry, no book with id = {bookForRentDto.Id} was found.");

                if (!await _rentService.CheckBookStatus(bookForRentDto.Id))
                    return NotFound($"Sorry, book with id = {bookForRentDto.Id} isn't available.");
            }

            var rentToAdd = _mapper.Map<Rent>(newRent);
            await _rentService.StartRentAsync(rentToAdd);

            /* ten return poniżej też tylko tymczasowo, bo ostatecznie będę chciał zwracać status 201, czyli zrobię to po zaimplementowaniu
             akcji GetRent wykorzystując metodę CreatedAtRoute - tak jak to zrobiłem w innych controllerach.
            */
            return Ok(newRent);
        }
    }
}
