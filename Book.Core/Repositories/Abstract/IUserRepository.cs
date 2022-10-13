namespace Books.API.Services.Abstract
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string userName);

        //Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        //Task<LocalUser> Register(RegisterationRequestDto registerationRequestDto);
    }
}
