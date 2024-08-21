using Microsoft.EntityFrameworkCore;
using L14_ex.Data;
using L14_ex.Handlers;
using Microsoft.AspNetCore.Authentication;

namespace L14_ex;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        // Connecting to db
        builder.Services.AddDbContext<L14DbContext>(options => options.UseSqlite(builder.Configuration["WebAPIConnection"]));


        // Add services to the container.
        builder.Services
            .AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, AuthHandler>("MyAuthentication", null)
            .AddScheme<AuthenticationSchemeOptions, AdminHandler>("AdminAuthentication", null);

        
        

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IL14Repo, L14Repo>();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireClaim("userName"));
        }
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
