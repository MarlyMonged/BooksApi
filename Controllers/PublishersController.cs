using AutoMapper;
using BooksApi.Dtos.Author;
using BooksApi.Dtos.Publisher;
using BooksApi.Exceptions;
using BooksApi.Interfaces;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {



        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublisherService _publisherService;

        public PublishersController(IUnitOfWork unitOfWork, IMapper mapper, IPublisherService publisherService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publisherService = publisherService;
        }
        [HttpGet("GetAllPublishers")]
        public async Task<IActionResult> GetAllPublishers()
        {
            var publishers = await _publisherService.GetAllPublishers();
            return Ok(publishers);
        }

        [HttpGet("GetPublisherWithBooksAndAuthors/{id:int}")]
        public async Task<IActionResult> GetPublisherWithBooksAndAuthors(int id)
        {
            var publisher = await _publisherService.GetPublisherWithBooksAndAuthors(id);

            if (publisher == null)
              return NotFound($"Publisher with id {id} not found.");

            return Ok(publisher);
        }

        [HttpPost("CreatePublisher")]
        public async Task<IActionResult> CreatePublisher([FromBody] CreatePublisherDto createPublisherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherId = await _publisherService.CreatePublisherAsync(createPublisherDto);
            return CreatedAtAction(nameof(GetPublisherWithBooksAndAuthors), new { id = publisherId }, createPublisherDto);

        }
        [HttpPut("UpdatePublisher/{id:int}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] CreatePublisherDto updatePublisherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = await _publisherService.UpdatePublisherAsync(id, updatePublisherDto);

            if (!isUpdated)
                return NotFound($"Publisher with id {id} not found.");

            return Ok(updatePublisherDto);

        }
        [HttpDelete("DeletePublisher/{id:int}")]

        public async Task<IActionResult> DeletePublisher(int id)
        {
            var isDeleted = await _publisherService.DeletePublisherAsync(id);

            if (!isDeleted)
                return NotFound($"Publisher with id {id} not found.");

            return NoContent();
        }


    }
}
