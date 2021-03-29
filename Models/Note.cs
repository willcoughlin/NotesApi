using System;

namespace NotesApi.Models
{
    public class Note 
    {
        public Note(string title, string contents) : this(default, title, contents) {}

        public Note(int id, string title, string contents) 
        {
            Id = id;
            Title = title;
            Contents = contents;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Contents { get; set; }
    }
}