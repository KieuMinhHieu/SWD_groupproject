using Application.Common;
using Infrastructures;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var configuration = builder.Configuration.Get<AppConfiguration>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/*builder.Services.AddInfrastructuresService(configuration!.DatabaseConnection);*/
builder.Services.AddInfrastructuresService(configuration!.DatabaseConnection);
builder.Services.AddWebApi(configuration!.JWTSecretKey);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tree management v1");

    });


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
