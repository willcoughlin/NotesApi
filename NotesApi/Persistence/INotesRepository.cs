using System.Collections.Generic;
using NotesApi.Models;

namespace NotesApi.Persistence
{
    /// <summary>
    /// Defines note persistence operations.
    /// </summary>
    public interface INotesRepository 
    {
        public IEnumerable<Note> GetAll();
        public Note Get(int id);
        public int Create(Note note);
        public void Edit(Note note);
        public void Delete(int id);
    }
}