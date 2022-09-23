using System.Text;
using System.Text.Json.Serialization;
using BlazorTodoService.Features.Authx;
using BlazorTodoService.Features.Todos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Create a builder for the application, honoring any arguments passed in from the command line
var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

// Add ASP.NET Core Identity with Entity Framework (the DB) for storage
builder.Services.AddIdentity<AuthxUser, AuthxRole>(opt => opt.User.RequireUniqueEmail = true)
    .AddRoles<AuthxRole>()
    .AddEntityFrameworkStores<AuthxDbContext>();

// Set up the database used for Authx
builder.Services.AddDbContext<AuthxDbContext>(opt =>
{
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new Exception("Default connection string is not defined"));
    if (builder.Environment.IsDevelopment())
        opt.LogTo(Console.WriteLine);
});

// Set up the database used for the Todos feature
builder.Services.AddDbContext<TodosDbContext>(opt =>
{
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new Exception("Default connection string is not defined"));
    if (builder.Environment.IsDevelopment())
        opt.LogTo(Console.WriteLine);
});

// Set up JWT Bearer authentication and authorization
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]));
builder.Services.AddSingleton<ITokenService, TokenService>(_ =>
    new TokenService(jwtIssuer, jwtAudience, jwtSecurityKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        // TODO: Move the key itself into AWS Secrets Manager, and place the path in the app settings
        IssuerSigningKey = jwtSecurityKey,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    });
builder.Services.AddAuthorization(opt =>
    opt.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes("Bearer", JwtBearerDefaults.AuthenticationScheme)
        .RequireRole(AuthxRoleConfiguration.VisitorRole)
        .Build());

// Add the controllers for each feature, and set up Enums to serialize and deserialize as strings on the wire
// TODO: This works for DTOs on the way in, but they still appear to be ints on the way out
builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Set up CORS so that requests from the client included in the solution work
builder.Services.AddCors(options =>
    options.AddPolicy(name: myAllowSpecificOrigins, policy => policy
        .WithOrigins("https://localhost:7270")
        .AllowAnyHeader()
        .AllowAnyMethod()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Blazor To-Do API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

#endregion

// Build the web application
var app = builder.Build();

// Run the database migrations
using (var scope = app.Services.CreateScope())
{
    var authxDbContext = scope.ServiceProvider.GetRequiredService<AuthxDbContext>();
    await authxDbContext.Database.EnsureCreatedAsync();
    await authxDbContext.Database.MigrateAsync();
    var todosDbContext = scope.ServiceProvider.GetRequiredService<TodosDbContext>();
    await todosDbContext.Database.EnsureCreatedAsync();
    await todosDbContext.Database.MigrateAsync();
}

#region Configure the HTTP request pipeline

// In development, send exceptions to the visitor, and set up Swagger for API testing and documentation
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect all incoming HTTP traffic to HTTPS
app.UseHttpsRedirection();

// Set up CORS as configured above
app.UseCors(myAllowSpecificOrigins);

// Use both authentication and authorization from ASP.NET Core Identity, as configured above
app.UseAuthentication();
app.UseAuthorization();

// Add routes for all the controllers found with AddControllers() above
app.MapControllers();

#endregion

// Start the web application
app.Run();