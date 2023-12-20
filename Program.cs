using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using webapi.Models;
using webapi.Services;

// Create a new instance of the web application builder
var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB settings using options pattern
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
// Add singleton service for user operations
builder.Services.AddSingleton<userService>();

// Add MongoDB as a singleton service
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = new MongoClient(settings.ConnectionURI);
    return client.GetDatabase(settings.DatabaseName);
});


// Add HttpContextAccessor to access HTTP-specific information
builder.Services.AddHttpContextAccessor();

// Add controllers to the service collection
builder.Services.AddControllers();

// Configure API documentation using Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Build the web application
var app = builder.Build();
// Configure Swagger UI and Swagger middleware in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS (Cross-Origin Resource Sharing)
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure method
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();