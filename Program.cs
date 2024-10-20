using System.Text.Json;
using System.Text.Json.Serialization;
using CardapioOnline.Infra;
using CardapioOnline.Interfaces.IRepository;
using CardapioOnline.Interfaces.IService;
using CardapioOnline.Repository;
using CardapioOnline.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();  // Adds support for controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your DbContext, repositories, and services
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IDetalhePedidoService, DetalhePedidoService>();
builder.Services.AddScoped<IDetalhePedidoRepository, DetalhePedidoRepository>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
var app = builder.Build();

// Swagger configuration for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CardapioOnlineAPI v1"));
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();  // Maps controller routes

app.Run();
