using Microsoft.AspNetCore.Http;
using NoteTakingApi.Common.Exceptions;
using NoteTakingApi.Common.Models;
using NoteTakingApi.DataAccess.Entities;
using NoteTakingApi.DataAccess.Repositories;
using NoteTakingApi.Service.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteTakingApi.Service.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly int _userId;

        public NoteService(INoteRepository noteRepository, 
            IHttpContextAccessor contextAccessor)
        {
            _noteRepository = noteRepository;
            _contextAccessor = contextAccessor;
            _userId = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst("userId")?.Value);
        }

        public async Task<IEnumerable<NoteResponseDto>> GetNotes()
        {
            var notes = await _noteRepository.GetNotes();

            return notes.Select(m => new NoteResponseDto(m.Id, m.Title, m.Content, m.UserId)).ToList();
        }

        public async Task<IEnumerable<NoteResponseDto>> GetNotesByUserId()
        {
           
            var notes = await _noteRepository.GetNotesByUserId(_userId);

            return notes.Select(m => new NoteResponseDto(m.Id, m.Title, m.Content, m.UserId)).ToList();
        }

        public async Task<NoteResponseDto> GetNoteById(int id)
        {
            var note = await _noteRepository.GetNoteById(id);

            if (note == null)
                throw new NotFoundException(new ErrorResponse($"Note with an id of {id} not found"));

            return new NoteResponseDto(note.Id, note.Title, note.Content, note.UserId);
        }

        public async Task<IEnumerable<NoteSimpleResponseDto>> GetNotesSimple()
        {
            var notes = await _noteRepository.GetNotesByUserId(_userId);

            return notes.Select(m => new NoteSimpleResponseDto(m.Id, m.Title)).ToList();
        }

        public async Task<NoteResponseDto> Add(AddNoteRequestDto request)
        {
            if (request.Title == null)
                throw new ValidationException(new ErrorResponse("Title is empty"));

            var note = new Note()
            {
                UserId = _userId,
                Title = request.Title,
                Content = request.Content
            };

            var data = await _noteRepository.Insert(note);

            return new NoteResponseDto(note.Id, note.Title, note.Content, note.UserId);
        }

        public async Task<NoteResponseDto> Update(int id,UpdateNoteRequestDto request)
        {
            if(request == null)
                throw new ValidationException(new ErrorResponse("Request is null"));

            if(id == 0)
                throw new ValidationException(new ErrorResponse("Id is 0"));

            request.Id = id;

            var note = await _noteRepository.GetNoteById(request.Id);

            if (note == null)
                throw new NotFoundException(new ErrorResponse($"Note with an id of {request.Id} not found"));

            note.Title = request.Title;
            note.Content = request.Content;

            var data = await _noteRepository.Update(note);

            return new NoteResponseDto(data.Id, data.Title, data.Content, data.UserId);
        }

        public async Task Delete(int id)
        {
            if(id == 0 )
                throw new ValidationException(new ErrorResponse("Id is 0"));

            var note = await _noteRepository.GetNoteById(id);

            if (note == null)
                throw new NotFoundException(new ErrorResponse($"Note with an id of {id} not found"));

            await _noteRepository.Delete(note);
        }
    }
}
