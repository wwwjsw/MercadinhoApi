using Microsoft.EntityFrameworkCore;
using MercadinhoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Context
builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS - IMPORTANTE: deve vir antes de UseAuthorization e MapControllers
app.UseCors("AllowAll");

// Remover o UseHttpsRedirection para evitar redirect para HTTPS
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MarketDbContext>();
        context.Database.EnsureCreated();
        
        // Adicionar alguns dados de exemplo
        if (!context.ItensMercado.Any())
        {
            context.ItensMercado.AddRange(
                new MercadinhoApi.Models.ItemMercado { Nome = "Maçã", Categoria = "Frutas", Preco = 2.50m, Quantidade = 10 },
                new MercadinhoApi.Models.ItemMercado { Nome = "Arroz", Categoria = "Grãos", Preco = 5.00m, Quantidade = 5 },
                new MercadinhoApi.Models.ItemMercado { Nome = "Leite", Categoria = "Laticínios", Preco = 4.50m, Quantidade = 8 }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao criar o banco de dados");
    }
}

app.Run();