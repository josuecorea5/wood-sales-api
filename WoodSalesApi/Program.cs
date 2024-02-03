using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WoodSalesApi.Authorization;
using WoodSalesApi.Automappers;
using WoodSalesApi.Configuration;
using WoodSalesApi.DTOs;
using WoodSalesApi.Helpers;
using WoodSalesApi.Middlewares;
using WoodSalesApi.Models;
using WoodSalesApi.Repositories;
using WoodSalesApi.Services;
using WoodSalesApi.Validators;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "corsPolicy";

//Cors
builder.Services.AddCors(options =>
{
	options.AddPolicy(corsPolicy, policy =>
	{
		policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
	});
});

//Repositories
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<ITransactionalRepository<Sale>, SaleRepository>();
builder.Services.AddScoped<IRepository<SaleDetail>, SaleDetailRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();

// Add services to the container.
builder.Services.AddScoped<ICommonService<ClientDto, ClientInsertDto, ClientUpdateDto>, ClientService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICommonService<ProductDto, ProductInsertDto, ProductUpdateDto>, ProductService>();

//Fluent Validations
builder.Services.AddScoped<IValidator<SaleInsertDto>, SaleInsertValidator>();
builder.Services.AddScoped<IValidator<SaleUpdateDto>, SaleUpdateValidator>();
builder.Services.AddScoped<IValidator<ClientInsertDto>, ClientInsertValidator>();
builder.Services.AddScoped <IValidator<ClientUpdateDto>, ClientUpdateValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserValidator>();
builder.Services.AddScoped<IValidator<RegisterUserDto>,  RegisterUserValidator>();
builder.Services.AddScoped<IValidator<ProductInsertDto>, ProductInsertValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();

//Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
	options.Password.RequiredLength = 8;
	options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<WoodSalesContext>().AddDefaultTokenProviders();

//JWT Setup
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();

//Policy register
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy(PolicyNames.AdminRolePolicy, policy =>
		policy.RequireRole("Admin"));
	options.AddPolicy(PolicyNames.AdminUserRolePolicy, policy =>
		policy.RequireRole("Admin", "User"));
});


builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

//EF injection
builder.Services.AddDbContext<WoodSalesContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("woodSalesConnection"));
});


//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Wood Sales API", Version = "v1" });

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Description = "JWT must be provided",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(corsPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<CustomAuthorizationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
