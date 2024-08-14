
using L07.Data;
using Microsoft.EntityFrameworkCore;
using L07.Model;

namespace L07;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // builder.Services.AddScoped<IWebAPIRepo, DBWebAPIRepo>;


        //db shit
        builder.Services.AddDbContext<WebAPIDBContext>(options =>
            options.UseSqlite(builder.Configuration["WebAPIConnection"]));

        
        // Add services to the container.
        builder.Services.AddAuthorization();

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

        //redirect user to https from http
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void AddRecord()
    {
        List<Customer> customers = new List<Customer>() { 
        new Customer {Id=1, FirstName="John",LastName="Minton"}
        };
    }
}
