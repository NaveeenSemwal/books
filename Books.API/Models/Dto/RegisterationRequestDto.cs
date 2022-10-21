namespace Books.API.Models.Dto
{
    public class RegisterationRequestDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }


    public class RegisterationResponsetDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
