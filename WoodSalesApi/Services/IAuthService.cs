using WoodSalesApi.DTOs;

namespace WoodSalesApi.Services
{
	public interface IAuthService
	{
		public List<string> Errors { get; }
		Task<bool> Register(RegisterUserDto registerUser);
		Task<string> Login(LoginUserDto loginUser);
	}
}