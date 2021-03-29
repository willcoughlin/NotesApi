using System;

namespace NotesApi.Models
{
    public class Note 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Contents { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}