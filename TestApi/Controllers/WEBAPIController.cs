using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestApi.Configuration;
using TestApi.Utilities;
using TestDB.DTOs;
using TestDB.Interfaces;
using TestLogic;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WEBAPIController : ControllerBase
    {
        private readonly IOptions<Urls> _config;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        public Service _service = new Service();
        public ListToDataTable _convert = new ListToDataTable();

        public WEBAPIController(IOptions<Urls> config, IBookService bookService, IAuthorService authorService)
        {
            _config = config;
            _bookService = bookService;
            _authorService = authorService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> getAutors()
        {
            var result = "";
            List<AuthorDto> author = new List<AuthorDto>();
            try
            {

                result = _service.getAutors(_config.Value.UrlApi).Result;
                author = JsonConvert.DeserializeObject<List<AuthorDto>>(result);

                var tbl = _convert.ConvertListToDataTable(author);

                _authorService.InsertAllAuthor(tbl);

                return Ok(author);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> getBooks()
        {
            var result = "";
            List<BookDto> book = new List<BookDto>();
            try
            {
                result = _service.getBooks(_config.Value.UrlApi).Result;

                book = JsonConvert.DeserializeObject<List<BookDto>>(result);

                var tbl = _convert.ConvertListToDataTable(book);

                _bookService.InsertAllBook(tbl);

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> SaveAllAuthorsBooks()
        {
            var resultAuthors = "";
            var resultBooks = "";
            List<AuthorDto> author = new List<AuthorDto>();
            List<BookDto> book = new List<BookDto>();

            try
            {

                resultAuthors = _service.getAutors(_config.Value.UrlApi).Result;
                resultBooks = _service.getBooks(_config.Value.UrlApi).Result;

                author = JsonConvert.DeserializeObject<List<AuthorDto>>(resultAuthors, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                book = JsonConvert.DeserializeObject<List<BookDto>>(resultBooks, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                var tblA = _convert.ConvertListToDataTable(author);
                var tblB = _convert.ConvertListToDataTable(book);

                if(_authorService.InsertAllAuthor(tblA) && _bookService.InsertAllBook(tblB))
                {
                    return Ok();
                }else if (_authorService.InsertAllAuthor(tblA) && !_bookService.InsertAllBook(tblB))
                {
                    return NotFound("Se guardo satisfactoriamente los autores pero los libros no se pudieron guardar");
                }
                else if (!_authorService.InsertAllAuthor(tblA) && _bookService.InsertAllBook(tblB))
                {
                    return NotFound("Se guardo satisfactoriamente los libros pero los libros no se pudieron guardar");
                }
                else
                {
                    return NotFound("No se guardo satisfactoriamente los autores y libros");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
