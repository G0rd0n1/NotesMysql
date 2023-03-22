
namespace HelloNet.Model
{
    public class AddNotesRequest
    {
        public int notesId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateNotesRequest
    {
        public int notesId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }


    public class NotesResponse {
        public readonly string? Note;

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int NotesId { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Content { get; set; }

    }
}
