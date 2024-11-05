using GerenciadoraAD.Services.GlobalVariables;

Globals.SetServidorLdap();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MinhaPoliticaDeCors", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();  
    });
});

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MinhaPoliticaDeCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
