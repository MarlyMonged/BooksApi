using AutoMapper;
using BooksApi.Dtos.Book;
using BooksApi.Interfaces;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using static System.Reflection.Metadata.BlobBuilder;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        
        private readonly IBookService _bookService;
      
        public BooksController( IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
           var books = await _bookService.GetAllBooks();

            return Ok(books);
        }
        [HttpGet("GetBookById/{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound($"Book with id {id} is not found!");
            }
            return Ok(book);
        }



        [HttpGet("GetBookWithAuthors/{id:int}")]
        public async Task<IActionResult> GetBookWithAuthors(int id)
        {
            var book = await _bookService.GetBookWithAuthorsAsync(id);
            if (book == null)
            {
                return NotFound($"Book with id {id} is not found!");
            }
            return Ok(book);
        }



        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookId = await _bookService.CreateBookAsync(createBookDto);

            return CreatedAtAction(nameof(GetBookById), new {id = bookId }, createBookDto);

        }
        [HttpPut("UpdateBook/{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var IsUpdated = await _bookService.UpdateBook(id, updateBookDto);

            if (!IsUpdated)
            {
                return NotFound($"Book with id {id} is not found!");
            }

            return Ok(updateBookDto);
        }
        [HttpDelete("DeleteBook/{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var IsDeleted = await _bookService.DeleteBook(id);
            if (!IsDeleted)
            {
                return NotFound($"Book with id {id} is not found!");
            }

            return NoContent();
        }
    }
}
