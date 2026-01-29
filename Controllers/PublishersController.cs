using AutoMapper;
using BooksApi.Dtos.Author;
using BooksApi.Dtos.Publisher;
using BooksApi.Interfaces;
using BooksApi.Models;
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

        public PublishersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost("Create Publisher")]
        public async Task<IActionResult> CreatePublisher([FromBody] CreatePublisherDto createPublisherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = _mapper.Map<Publisher>(createPublisherDto);

            await _unitOfWork.Publishers.AddAsync(publisher);
            await _unitOfWork.Save();

            return Ok(publisher);


        }
    }
}
