using GabrielesProject.AdformExam.Application;
using GabrielesProject.AdformExam.Application.Services;
using GabrielesProject.AdformExam.Infrastructure;
using GabrielesProject.AdformExam.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
string? dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(dbConnectionString);
builder.Services.AddHttpClient();
builder.Services.AddHostedService<CleanupHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
