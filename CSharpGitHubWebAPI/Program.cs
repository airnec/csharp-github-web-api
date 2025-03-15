var builder = WebApplication.CreateBuilder(args);

// CORS'u etkinleþtir
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Herhangi bir kaynaða izin ver
              .AllowAnyMethod()  // Herhangi bir HTTP metoduna izin ver
              .AllowAnyHeader(); // Herhangi bir header'a izin ver
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS'u kullan
app.UseCors("AllowAll");

app.UseStaticFiles(); // wwwroot içindeki dosyalarý sunabilmek için

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
