using L07.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebAPIDBContext>(options => options.UseSqlite(builder.Configuration["WebAPIConnection"]));

builder.Services.AddScoped<IWebAPIRepo, DBWebAPIRepo>();


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