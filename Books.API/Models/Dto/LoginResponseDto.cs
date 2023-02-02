using Books.API.Entities;

namespace Books.API.Models.Dto
{
    public class LoginResponseDto
    {
        public MemberDto User { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}
