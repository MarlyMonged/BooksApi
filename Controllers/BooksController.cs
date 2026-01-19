using AutoMapper;
using BooksApi.Dtos.Book;
using BooksApi.Interfaces;
using BooksApi.Models;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            var bookDto = _mapper.Map<IEnumerable<GetBookDto>>(books);
            return Ok(bookDto);
        }
        [HttpGet("GetBookById/{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with id {id} is not found!");
            }
            var bookDto = _mapper.Map<GetBookDto>(book);


            return Ok(bookDto);
        }
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = _mapper.Map<Book>(createBookDto);

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.Save();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, createBookDto);

        }
        [HttpPut("UpdateBook/{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound($"Book with id {id} is not found!");
            }
            _mapper.Map(updateBookDto, existingBook);
            await _unitOfWork.Books.UpdateAsync(existingBook);
            await _unitOfWork.Save();
            return Ok(updateBookDto);

        }
        [HttpDelete("DeleteBook/{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound($"Book with id {id} is not found!");
            }
            await _unitOfWork.Books.DeleteAsync(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
