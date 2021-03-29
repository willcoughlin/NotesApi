using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("notes")]
    [Produces("application/json")]
    public class NotesController : ControllerBase 
    {
        /// <summary>
        /// List all existing notes.
        /// </summary>
        /// <returns>A collection of all notes saved.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetAll()
        {
            return Ok();
        }

        /// <summary>
        /// Get a specific note.
        /// </summary>
        /// <param name="id">The ID of the note to return.</param>
        /// <returns>A note specified by the provided ID.</returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Note> Get(int id) 
        {
            return Ok(new Note { Id = id });
        }

        /// <summary>
        /// Create a new note.
        /// </summary>
        /// <param name="note">The note's title and contents.</param>
        /// <returns>Confirmation of note creation, with the new note's ID.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] NoteRequest note)
        {
            return Ok(new Note { Title = note.Title, Contents = note.Contents });
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
            return Ok(new Note { Id = id, Title = note.Title, Contents = note.Contents });
        }

        /// <summary>
        /// Delete and existing note.
        /// </summary>
        /// <param name="id">The note's ID.</param>
        /// <returns>Confirmation of note deletion.</returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(new Note { Id = id });
        }
    }
}