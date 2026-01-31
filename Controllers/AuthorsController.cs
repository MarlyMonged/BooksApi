using AutoMapper;
using BooksApi.Dtos.Author;
using BooksApi.Interfaces;
using BooksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
       

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost("Create Author")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = _mapper.Map<Author>(createAuthorDto); 

            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.Save();

            return Ok(author);


        }
    }
}
