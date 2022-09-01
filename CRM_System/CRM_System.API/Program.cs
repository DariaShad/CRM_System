using CRM_System.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);


// to env variable
builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(@"Server=80.78.240.16;Database=CRM.Db;User Id=Student;Password=qwe!23"));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);


builder.Services.AddAuthorization();

builder.Services.AddServices();

builder.Services.AddFluentValidation();

builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();