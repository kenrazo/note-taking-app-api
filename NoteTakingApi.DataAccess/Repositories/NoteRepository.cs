using Microsoft.EntityFrameworkCore;
using NoteTakingApi.DataAccess.DataContexts;
using NoteTakingApi.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteTakingApi.DataAccess.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteTakingDbContext _context;

        public NoteRepository(NoteTakingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetNotes() => await _context.Note.ToListAsync();

        public async Task<IEnumerable<Note>> GetNotesByUserId(int userId) => await _context.Note.Where(m => m.UserId == userId).ToListAsync();

        public async Task<Note> GetNoteById(int id) => await _context.Note.Where(m => m.Id == id).FirstOrDefaultAsync();

        public async Task<Note> Insert(Note note)
        {
            await _context.Note.AddAsync(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task<Note> Update(Note note)
        {
            _context.Note.Attach(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task Delete(Note note)
        {
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}
