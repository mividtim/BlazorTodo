using BlazorTodoService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("Todos"));
builder.Services.AddCors(options =>
    options.AddPolicy(name: myAllowSpecificOrigins, policy => policy
        .WithOrigins("https://localhost:7270")
        .AllowAnyHeader()
        .AllowAnyMethod()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();