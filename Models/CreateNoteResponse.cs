namespace NotesApi.Models
{
    public class CreateNoteResponse
    {
        public CreateNoteResponse(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}