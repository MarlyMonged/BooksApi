using AutoMapper;
using BooksApi.Dtos.Author;
using BooksApi.Interfaces;
using BooksApi.Models;
using BooksApi.Services;
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
        private readonly IAuthorService _authorService;

        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper, IAuthorService authorService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authorService = authorService;
        }
        [HttpGet("Get All Authors")]
        public async Task<IActionResult> GetAllAuthors()
        {
           var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }
        [HttpGet("GetAuthorById/{id:int}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if(author is null)
                return NotFound($"Author with Id = {id} not found");
            return Ok(author);
        }
        [HttpGet("GetAuthorWithBooks/{id:int}")]
        public async Task<IActionResult> GetAuthorWithBooks(int id)
        {
            var authorWithBooks = await _authorService.GetAuthorWithBooksAsync(id);
            if(authorWithBooks is null)
                return NotFound($"Author with Id = {id} not found");
            return Ok(authorWithBooks);
        }

        [HttpPost("Create Author")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto createAuthorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorId = await _authorService.CreateAuthorAsync(createAuthorDto);


            return CreatedAtAction(nameof(GetAuthorById), new { id = authorId }, createAuthorDto);
        }
        [HttpPut("UpdateAuthor/{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto updateAuthorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isUpdated = await _authorService.UpdateAuthor(id, updateAuthorDto);
            if(!isUpdated)
                return NotFound($"Author with Id = {id} not found");
            return Ok(updateAuthorDto);
        }
        

        [HttpDelete("Delete Author/{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var IsDeleted = await _authorService.DeleteAuthor(id);
            if(!IsDeleted)
                return NotFound($"Author with Id = {id} not found");
            return NoContent();
        }
    }
}
