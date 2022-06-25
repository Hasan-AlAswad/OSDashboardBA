using Microsoft.EntityFrameworkCore;
using OSDashboardBA.DB;
using OSDashboardBA.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OSDashboardBA.Auth;


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddScoped<FileService>();  // H- file services

// H- add CORS // link backend with front 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000/");
        });
});


// H- For Entity Framework
// H- link appDbContext and Npgsql server db with conn string through builder services 
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("OSDashboardConnection"));
    
});

// POSTGRE -->
// options.UseNpgsql(builder.Configuration.GetConnectionString("OSDashboardConnection"));

// mssql -->
//  options.UseSqlServer(builder.Configuration.GetConnectionString("OSDashboardConnection"));

//// H- identity service
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//H- to handle reference loop using newtonSoft.json package
builder.Services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.Preserve);

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

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
