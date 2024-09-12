using AutoMapper;
using Library.WebAPI.Entities;
using Library.WebAPI.Models;
using Library.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Data;

namespace Library.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService _readerService;
        private readonly IMapper _mapper;

        public ReadersController(IReaderService readerService, IMapper mapper) 
        {
            _readerService = readerService ?? throw new ArgumentNullException(nameof(readerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReaderDto>>> GetReaders()
        {
            var response = await _readerService.GetReadersAsync();

            if (response == null || !response.Any())
                return NotFound($"Sorry, no reader was found.");

            return Ok(_mapper.Map<IEnumerable<ReaderDto>>(response));
        }

        [HttpGet("{lastName}")]
        public async Task<ActionResult<IEnumerable<ReaderDto>>> GetReadersByName(string lastName)
        {
            var response = await _readerService.GetReadersByNameAsync(lastName);

            if (response == null || !response.Any())
                return NotFound($"Sorry, no reader last name {lastName} was found");

            return Ok(_mapper.Map<IEnumerable<ReaderDto>>(response));
        }

        [HttpGet("{id}/{lastName}", Name = "GetReader")]
        public async Task<ActionResult<ReaderDto>> GetReader(int id, string lastName)
        {
            var response = await _readerService.GetReaderAsync(id, lastName);

            if (response == null)
                return NotFound($"Sorry, no reader with id = {id} and last name {lastName} was found" +
                    $" It's possible you've entered the wrong id or last name.");

            return Ok(_mapper.Map<ReaderDto>(response));
        }

        [HttpPost]
        public ActionResult<ReaderDto> AddReader(ReaderForCreationDto newReaderDto)
        {
            var readerToAdd = _mapper.Map<Reader>(newReaderDto);

            _readerService.AddReader(readerToAdd);

            var readerToReturn = _mapper.Map<ReaderDto>(readerToAdd);

            return CreatedAtRoute("GetReader",
                new
                {
                    id = readerToAdd.Id,
                    lastName = readerToReturn.LastName,
                },
                readerToReturn
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReader(int id, ReaderForUpdateDto readerForUpdateDto)
        {
            var readerDao = await _readerService.GetReaderByIdAsync(id);

            if(readerDao == null)
            return NotFound($"Sorry, no reader with id = {id} was found.");

            _mapper.Map(readerForUpdateDto, readerDao);

            _readerService.UpdateReader();

            return Ok($"Reader {readerDao.FirstName} {readerDao.LastName} has been updated.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReader(int id)
        {
            var readerToRemove = await _readerService.GetReaderByIdAsync(id);

            if (readerToRemove == null)
                return NotFound($"Sorry, no reader with id = {id} was found");

            _readerService.DeleteReader(readerToRemove);

            return Ok($"Reader {readerToRemove.FirstName} {readerToRemove.LastName} has been deleted.");
        }
    }
}
