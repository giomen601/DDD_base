using easypos.api;
using easypos.api.Extensions;
using easypos.api.Middleware;
using easypos.application;
using easypos.infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentation()
  .AddInfraestructure(builder.Configuration)
  .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.ApplyMigrations();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

//add-migration InitialMigration -Context ApplicationDbContext -o Persistence/Migrations
