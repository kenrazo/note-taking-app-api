using NoteTakingApi.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApi.Service.Services
{
    public interface INoteService
    {
        Task<IEnumerable<NoteResponseDto>> GetNotes();
        Task<IEnumerable<NoteResponseDto>> GetNotesByUserId();
        Task<NoteResponseDto> GetNoteById(int id);
        Task<IEnumerable<NoteSimpleResponseDto>> GetNotesSimple();
        Task<NoteResponseDto> Add(AddNoteRequestDto request);
        Task<NoteResponseDto> Update(int id, UpdateNoteRequestDto request);
        Task Delete(int id);
    }
}
