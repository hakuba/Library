using Library.Configuration;
using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);

// Set the static file path
var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "public");

builder.Services.AddControllers(options =>
{
    
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Authorization"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//JwtConfig.secret = builder.Configuration["JwtSecret:secret"];

builder.Services
    .AddDatabaseSetup(builder.Configuration)
    .AddScopedData();

var app = builder.Build();


app.UseCors("CorsPolicy");

//app.UseWebSocketHandler();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "./Storage")),
    RequestPath = "/Storage"
});




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();


app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthentication();


app.Map("/api", backendApp =>
{
    backendApp.Use(async (context, next) =>
    {
        await next();
    });

    backendApp.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
});

app.Run();
