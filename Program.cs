using javier_api.Data;
using javier_api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace javier_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Base de datos
            builder.Services.AddDbContext<LibreriaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibreriaConnection")));

            // Repositorios
            builder.Services.AddScoped<ILibroRepository, Librorepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // JWT
            var jwtKey = builder.Configuration["Jwt:Key"]!;
            var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}