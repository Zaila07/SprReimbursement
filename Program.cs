using Azure;
using Azure.AI.FormRecognizer;
using Microsoft.EntityFrameworkCore;
using SprEmployeeReimbursement.DataAccess.SprDbContext;
using System.Data.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using SprEmployeeReimbursement.Business.ServiceCollection;
using SprEmployeeReimbursement.Business.FormRecognizer;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Azure Form Recognizer Client
builder.Services.AddSingleton<Azure.AI.FormRecognizer.FormRecognizerClient>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
var endpoint = configuration["AzureFormRecognizer:Endpoint"];
var apiKey = configuration["AzureFormRecognizer:ApiKey"];
var credentials = new AzureKeyCredential(apiKey);
return new FormRecognizerClient(new Uri(endpoint), credentials);
});

//Add DbContext
builder.Services.AddDbContext<SprReimbursementDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//Add FormRecognizerHelper
builder.Services.AddScoped<FormRecognizerHelper>();

//Register the IReimbursementService and its Implementation
builder.Services.AddScoped<IReimbursementService, ReimbursementService>(); 

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
