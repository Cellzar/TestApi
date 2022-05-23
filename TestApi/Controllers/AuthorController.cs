using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.Responses;
using TestDB.Context;
using TestDB.DTOs;
using TestDB.Interfaces;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _autorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService autorService, IMapper mapper)
        {
            _autorService = autorService;
            _mapper = mapper;
        }


        [HttpGet("[action]")]
        public ActionResult GetAutors()
        {
            try
            {
                var autor = _autorService.GetAuthors();
                var autorDto = _mapper.Map<List<AuthorDto>>(autor);
                var response = new ApiResponse<List<AuthorDto>>(autorDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAutor(int id)
        {
            try
            {
                var autor = await _autorService.GetAuthor(id);
                var autorDto = _mapper.Map<AuthorDto>(autor);
                var response = new ApiResponse<AuthorDto>(autorDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthorDto autorDto)
        {
            try
            {
                var autor = _mapper.Map<Author>(autorDto);
                await _autorService.InsertAuthor(autor);
                autorDto = _mapper.Map<AuthorDto>(autor);
                var response = new ApiResponse<AuthorDto>(autorDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, AuthorDto autorDto)
        {
            try
            {
                var autor = _mapper.Map<Author>(autorDto);
                autor.Id = id;
                var result = await _autorService.UpdateAuthor(autor);
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
                var result = await _autorService.DeleteAuthor(id);
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
