
using System.Text.Json.Serialization;
using NailsSys.API.Configurations;
using NailsSys.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationsFilter)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMvc().AddJsonOptions(o => 
            {
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);
builder.Services.AddServicesConfigurations();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => 
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        x.RoutePrefix = string.Empty;   
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
