using System.Collections.Generic;
using NotesApi.Models;

namespace NotesApi.Persistence
{
  /// <summary>
  /// Implements INotesRepository with JSON file for persistence.
  /// </summary>
  public class NotesRepository : INotesRepository
  {
    public void Create(Note note)
    {
      throw new System.NotImplementedException();
    }

    public void Delete(int id)
    {
      throw new System.NotImplementedException();
    }

    public void Edit(Note note)
    {
      throw new System.NotImplementedException();
    }

    public Note Get(int id)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Note> GetAll()
    {
      throw new System.NotImplementedException();
    }
  }
}