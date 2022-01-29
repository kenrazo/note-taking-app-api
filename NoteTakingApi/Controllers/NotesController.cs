using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteTakingApi.Service.Dtos;
using NoteTakingApi.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteTakingApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _noteService.GetNotesByUserId();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _noteService.GetNoteById(id);

            return Ok(data);
        }

        [HttpGet("simple")]
        public async Task<IActionResult> GetAllSimple()
        {
            var data = await _noteService.GetNotesSimple();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddNoteRequestDto requestDto)
        {
            var data = await _noteService.Add(requestDto);

            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateNoteRequestDto requestDto)
        {
            var data = await _noteService.Update(id,requestDto);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _noteService.Delete(id);

            return NoContent();
        }
    }
}
