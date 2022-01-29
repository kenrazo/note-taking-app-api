namespace NoteTakingApi.Service.Dtos
{
    public class RegisterResponseDto
    {
        public RegisterResponseDto(string username, int id)
        {
            Username = username;
            Id = id;
        }
        public string Username { get; set; }
        public int Id { get; set; }
    }
}