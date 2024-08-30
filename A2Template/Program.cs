using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using A2.Data;
using A2.Helper;
using System.Security.Claims;

namespace P1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //connect db
        builder.Services.AddDbContext<A2DbContext>(options => options.UseSqlite(builder.Configuration["A2DBConnection"]));
        builder.Services.AddScoped<IA2Repo, A2Repo>();

        //autheticate stuff
        builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, A2AuthHandler>("MyAuthentication", null);

        builder.Services.AddAuthorization(options =>
        {

            options.AddPolicy("bothUsers", policy =>
            {
                policy.RequireAssertion(context =>
               context.User.HasClaim(c =>
               (c.Value == "normalUser" || c.Value == "organizer")));
            });
            options.AddPolicy("organizer", policy => policy.RequireClaim(ClaimTypes.Role, "organizer"));
            options.AddPolicy("normalUser", policy => policy.RequireClaim(ClaimTypes.Role,"normalUser"));

        });

        builder.Services.AddMvc(options => options.OutputFormatters.Add(new CalenderOutputFormatter()));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

       

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
