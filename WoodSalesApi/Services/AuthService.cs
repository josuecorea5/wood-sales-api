using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WoodSalesApi.Configuration;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly JwtOptions _jwtOptions;
		public List<string> Errors { get; }
		public AuthService(UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
		{
			_userManager = userManager;
			_jwtOptions = jwtOptions.Value;
			Errors = new List<string>();
		}

		public async Task<bool> Register(RegisterUserDto registerUser)
		{
			var identityUser = new IdentityUser { UserName = registerUser.UserName, Email = registerUser.Email };

			var result = await _userManager.CreateAsync(identityUser, registerUser.Password);
			if(result.Succeeded)
			{
				await _userManager.AddToRoleAsync(identityUser, Roles.User.ToString());
				return result.Succeeded;
			}

			result.Errors.ToList().ForEach(e => Errors.Add(e.Description));

			return false;
		}

		public async Task<string> Login(LoginUserDto loginUser)
		{
			var identityUser = await _userManager.FindByEmailAsync(loginUser.Email);

			if(identityUser is null)
			{
				Errors.Add("user not found");
				return null;
			}

			var roleClaims = await _userManager.GetRolesAsync(identityUser);

            if(await _userManager.CheckPasswordAsync(identityUser, loginUser.Password))
			{
				var token = GenerateJwt(identityUser, roleClaims);
				return token;
			}

			Errors.Add("Email or password is not correct");
			return null;
		}

		private string GenerateJwt(IdentityUser identityUser, IList<string> roles)
		{
			var roleClaims = new List<Claim>();

			for(int i = 0; i < roles.Count; i++)
			{
				roleClaims.Add(new Claim("roles", roles[i]));
			}

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
				new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
				new Claim(JwtRegisteredClaimNames.Name, identityUser.UserName),
			}.Union(roleClaims);

			var signingCredentials = new SigningCredentials(
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
					SecurityAlgorithms.HmacSha256
				);

			var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, null, DateTime.UtcNow.AddHours(2), signingCredentials);

			string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
			
			return tokenValue;
		}
	}
}
