using Microsoft.EntityFrameworkCore;
using OSDashboardBA.DB;
using OSDashboardBA.Services;
using System.Text.Json.Serialization;
using OSDashboardBA.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
//
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;
//





var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddScoped<FileService>();  // H- file services

// H- add CORS 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://example.com",
                                "http://www.contoso.com");
        });
});

//// H- identity service
//builder.Services.AddIdentity<User, AppRole>(options =>
//    options.SignIn.RequireConfirmedAccount = false)   // -REQUIRED-: to be TRUE -check later- 
//        .AddEntityFrameworkStores<AppDbContext>();


// H- link appDbContext and Npgsql server db with conn string through builder services 
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OSDashboardConnection"));
    
});

// POSTGRE -->
// options.UseNpgsql(builder.Configuration.GetConnectionString("OSDashboardConnection"));

// mssql -->
//  options.UseSqlServer(builder.Configuration.GetConnectionString("OSDashboardConnection"));

//H- to handle reference loop using newtonSoft.json package
builder.Services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.Preserve);

// geojson
//builder.Services.AddControllers()
//  .AddJsonOptions(options => {
//      options.JsonSerializerOptions.Converters.Add(
//          new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
//  });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//// H- google auth 
//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication(); // H- Middleware to read credintials from cookies
app.UseAuthorization();

app.MapControllers();

app.Run();
