using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NotesApi.Models;
using System.Linq;
using System;

namespace NotesApi.Persistence
{
    /// <summary>
    /// Implements INotesRepository with JSON file for persistence.
    /// </summary>
    public class NotesRepository : INotesRepository
    {    
        private Dictionary<int, Note> _notes = new Dictionary<int, Note>();

        public NotesRepository()
        {
            try
            {
                if (File.Exists("notes.json")) 
                {
                    using var reader = new StreamReader("notes.json");
                    string persistedNotes = reader.ReadToEnd();
                    _notes = JsonConvert.DeserializeObject<Dictionary<int, Note>>(persistedNotes);
                }
            }
            catch {}  // Silently treat as though no file exists
        }

        public void Create(Note note)
        {
            var nextId = _notes.Keys.DefaultIfEmpty().Max() + 1;
            note.Id = nextId;
            _notes[nextId] = note;
            PersistToFile();
        }

        public void Delete(int id)
        {
            if (!_notes.Remove(id)) throw new ResourceNotFoundException();
            PersistToFile();
        }

        public void Edit(Note note)
        {
            if (!_notes.ContainsKey(note.Id)) throw new ResourceNotFoundException();
            _notes[note.Id] = note;
            PersistToFile();
        }

        public Note Get(int id)
        {
            if (_notes.TryGetValue(id, out Note value)) return value;
            return null;
        }

        public IEnumerable<Note> GetAll() => _notes.Values;

        private void PersistToFile() 
        {
            string notesToPersist = JsonConvert.SerializeObject(_notes);
            using var writer = new StreamWriter("notes.json", false);
            writer.WriteLine(notesToPersist);
        }
    }
}