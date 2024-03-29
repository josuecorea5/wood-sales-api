﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace WoodSalesApi.Configuration
{
	public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
	{
		private JwtOptions _jwtOptions;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {
			_jwtOptions = jwtOptions.Value;
        }

        public void Configure(JwtBearerOptions options)
		{
			options.TokenValidationParameters = new()
			{
				ValidateActor = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = _jwtOptions.Issuer,
				ValidAudience = _jwtOptions.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
			};
		}
		public void Configure(string? name, JwtBearerOptions options)
			=> Configure(options);
	}
}
