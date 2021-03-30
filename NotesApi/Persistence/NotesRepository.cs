using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NotesApi.Models;
using System.Linq;
using NotesApi.Exceptions;

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
                // If file exists, parse it into dictionary
                if (File.Exists("notes.json")) 
                {
                    using var reader = new StreamReader("notes.json");
                    string persistedNotes = reader.ReadToEnd();
                    _notes = JsonConvert.DeserializeObject<Dictionary<int, Note>>(persistedNotes);
                }
            }
            catch {}  // Silently treat as though no file exists
        }

        /// <summary>
        /// Adds a note to the collection.
        /// </summary>
        /// <param name="note">Note to add.</param>
        /// <returns>ID of created note.</returns>
        public int Create(Note note)
        {
            var nextId = _notes.Keys.DefaultIfEmpty().Max() + 1;
            note.Id = nextId;
            _notes[nextId] = note;
            PersistToFile();
            return nextId;
        }

        /// <summary>
        /// Deletes a note from the collection.
        /// </summary>
        /// <param name="id">ID of note to delete.</param>
        /// <exception cref="NotesApi.Exceptions.ResourceNotFoundException">Thrown when a note with the given ID is not found.</exception>
        public void Delete(int id)
        {
            if (!_notes.Remove(id)) throw new ResourceNotFoundException();
            PersistToFile();
        }

        /// <summary>
        /// Edits an existing note in the collection.
        /// </summary>
        /// <param name="note">Note to edit.</param>
        /// <exception cref="NotesApi.Exceptions.ResourceNotFoundException">Thrown when a note with the given ID is not found.</exception>
        public void Edit(Note note)
        {
            if (!_notes.ContainsKey(note.Id)) throw new ResourceNotFoundException();
            _notes[note.Id] = note;
            PersistToFile();
        }

        /// <summary>
        /// Gets a specific note.
        /// </summary>
        /// <param name="id">ID of note to return.</param>
        /// <returns>The note with the specified ID, or null if not found.</returns>
        public Note Get(int id)
        {
            if (_notes.TryGetValue(id, out Note value)) return value;
            return null;
        }

        /// <summary>
        /// Gets all saved notes.
        /// </summary>
        /// <returns>A collection of saved notes.</returns>
        public IEnumerable<Note> GetAll() => _notes.Values;

        /// <summary>
        /// Saves the contents of the private backing store to a file.
        /// </summary>
        private void PersistToFile() 
        {
            string notesToPersist = JsonConvert.SerializeObject(_notes);
            using var writer = new StreamWriter("notes.json", false);
            writer.WriteLine(notesToPersist);
        }
    }
}