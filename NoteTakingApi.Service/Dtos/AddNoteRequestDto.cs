namespace NoteTakingApi.Service.Dtos
{
    public class AddNoteRequestDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
