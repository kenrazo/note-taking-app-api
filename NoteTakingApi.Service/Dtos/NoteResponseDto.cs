using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApi.Service.Dtos
{
    public class NoteResponseDto
    {
        public NoteResponseDto(int id, string title, string content, int userId)
        {
            Id = id;
            Title = title;
            Content = content;
            UserId = userId;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
