using Azure;
using Azure.AI.FormRecognizer;
using Microsoft.EntityFrameworkCore;
using SprEmployeeReimbursement.DataAccess.SprDbContext;
using System.Text.Json.Serialization;
using SprEmployeeReimbursement.Business.ServiceCollection;
using SprEmployeeReimbursement.Business.FormRecognizer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using SprEmployeeReimbursement.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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
builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new ApiVersion(1, 1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
   
});
builder.Services.AddSwaggerGen();

//Add  API Versioning
builder.Services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
builder.Services.AddApiVersioning(options=>
{ 
    options.AssumeDefaultVersionWhenUnspecified= true;
    options.DefaultApiVersion=new ApiVersion(1, 0);
});
builder.Services.AddVersionedApiExplorer(options=>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl= true;

});
builder.Services.AddSwaggerGen(options=>
{
    options.OperationFilter<SwaggerDefaultValues>();
});

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
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:8080")
        .AllowAnyMethod()
        .AllowAnyHeader();

    });

});

var app = builder.Build();

app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"SprEmployeeReimbursement API - {description.GroupName.ToUpper()}");
    }
});


app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();
app.Run();
