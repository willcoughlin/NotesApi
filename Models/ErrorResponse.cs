namespace NotesApi.Models
{
    public class ErrorResponse 
    {
        public ErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        
        public string ErrorMessage { get; set; }
    }
}