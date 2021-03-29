using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using NotesApi.Persistence;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("notes")]
    [Produces("application/json")]
    public class NotesController : ControllerBase 
    {
        private readonly INotesRepository _notesRepository;

        public NotesController(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        /// <summary>
        /// List all existing notes.
        /// </summary>
        /// <returns>A collection of all notes saved.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetAll() => Ok(_notesRepository.GetAll());

        /// <summary>
        /// Get a specific note.
        /// </summary>
        /// <param name="id">The ID of the note to return.</param>
        /// <returns>A note specified by the provided ID.</returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Note> Get(int id)
        {
            var result = _notesRepository.Get(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Create a new note.
        /// </summary>
        /// <param name="note">The note's title and contents.</param>
        /// <returns>Confirmation of note creation, with the new note's ID.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] NoteRequest note)
        {
            var newNote = new Note(note.Title, note.Contents);
            _notesRepository.Create(newNote);
            return Ok();
        }

        /// <summary>
        /// Edit an existing note.
        /// </summary>
        /// <param name="id">The note's ID.</param>
        /// <param name="note">The note's updated title and/or contents.</param>
        /// <returns>Confirmation of note update.</returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, [FromBody] NoteRequest note)
        {
            var updatedNote = new Note(id, note.Title, note.Contents);

            try 
            {
                _notesRepository.Edit(updatedNote);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
            
            return Ok();
        }

        /// <summary>
        /// Delete an existing note.
        /// </summary>
        /// <param name="id">The note's ID.</param>
        /// <returns>Confirmation of note deletion.</returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            try 
            {
                _notesRepository.Delete(id);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}