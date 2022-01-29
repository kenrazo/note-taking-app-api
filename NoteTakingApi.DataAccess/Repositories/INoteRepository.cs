using NoteTakingApi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApi.DataAccess.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetNotes();
        Task<IEnumerable<Note>> GetNotesByUserId(int userId);
        Task<Note> GetNoteById(int id);
        Task<Note> Insert(Note note);
        Task<Note> Update(Note note);
        Task Delete(Note note);
    }
}
