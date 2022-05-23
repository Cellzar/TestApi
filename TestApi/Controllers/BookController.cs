using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Responses;
using TestDB.Context;
using TestDB.DTOs;
using TestDB.Interfaces;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public ActionResult GetBooks()
        {
            try
            {
                var book = _bookService.GetBooks();
                var bookDto = _mapper.Map<List<BookDto>>(book);
                var response = new ApiResponse<List<BookDto>>(bookDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpGet("[action]")]
        public ActionResult GetBooksByAuthor(int idBook, DateTime initialDate, DateTime endDate)
        {
            try
            {
                var book = _bookService.GetBooks();
                var bookDto = _mapper.Map<List<BookDto>>(book);
                var responseBook = bookDto.Where(c => c.Id == idBook && c.PublicDate >= initialDate && c.PublicDate<= endDate).ToList();
                var response = new ApiResponse<List<BookDto>>(responseBook);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(int id)
        {
            try
            {
                var book = await _bookService.GetBook(id);
                var bookDto = _mapper.Map<BookDto>(book);
                var response = new ApiResponse<BookDto>(bookDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookDto bookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDto);
                await _bookService.InsertBook(book);
                bookDto = _mapper.Map<BookDto>(book);
                var response = new ApiResponse<BookDto>(bookDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, BookDto bookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDto);
                book.Id = id;
                var result = await _bookService.UpdateBook(book);
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _bookService.DeleteBook(id);
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
