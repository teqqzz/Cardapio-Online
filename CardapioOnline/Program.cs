using Microsoft.EntityFrameworkCore;
using CardapioOnline.Models;
using CardapioOnline.Infra;
using CardapioOnline.Schemas;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=cardapioonline.db"));

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configuracao de referencia ciclica
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});
// Configura��o do CORS (Mover antes de app.Build)
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

// Configura��o do Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar pol�tica de CORS (antes das rotas)
app.UseCors("AllowAll");
app.UseStaticFiles();

// Rotas Minimal API

// 1. Categorias
app.MapGet("/api/categorias", async (AppDbContext context) =>
{
    return Results.Ok(await context.Categorias.Include(c => c.Itens).ToListAsync());
});

app.MapGet("/api/categorias/{id:long}", async (long id, AppDbContext context) =>
{
    var categoria = await context.Categorias.FindAsync(id);
    return categoria == null ? Results.NotFound() : Results.Ok(categoria);
});

app.MapPost("/api/categorias", async (Categoria categoria, AppDbContext context) =>
{
    await context.Categorias.AddAsync(categoria);
    await context.SaveChangesAsync();
    return Results.Created($"/api/categorias/{categoria.Id}", categoria);
});

app.MapPut("/api/categorias/{id:long}", async (long id, Categoria updatedCategoria, AppDbContext context) =>
{
    var categoria = await context.Categorias.FindAsync(id);
    if (categoria == null) return Results.NotFound();

    categoria.Nome = updatedCategoria.Nome;
    categoria.AtualizadoEm = DateTime.Now;
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/categorias/{id:long}", async (long id, AppDbContext context) =>
{
    var categoria = await context.Categorias.FindAsync(id);
    if (categoria == null) return Results.NotFound();

    context.Categorias.Remove(categoria);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

// 2. Itens

app.MapGet("/api/itens/{id:long}", async (long? categoriaId, AppDbContext context) =>
{
    var query = context.Itens.AsQueryable();

    if (categoriaId.HasValue)
        query = query.Where(i => i.CategoriaId == categoriaId.Value);

    return Results.Ok(await query.ToListAsync());
});

app.MapGet("/api/itens", async (AppDbContext context) =>
{
    var itens = await context.Itens.Include(i => i.Categoria).ToListAsync();

    return Results.Ok(itens);
});


app.MapPost("/api/itens", async (HttpRequest request, AppDbContext context) =>
{
    var form = await request.ReadFormAsync();
    var nome = form["nome"];
    var descricao = form["descricao"];
    var precoString = form["preco"];
    var categoriaIdString = form["categoriaId"];
    var imagem = form.Files.GetFile("imagem");

    if (!decimal.TryParse(precoString, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal preco) || preco <= 0 ||
    !long.TryParse(categoriaIdString, out long categoriaId) || categoriaId <= 0)
    {
        return Results.BadRequest("Preço ou Categoria inválidos.");
    }

    if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao) || preco <= 0 || imagem == null)
    {
        return Results.BadRequest("Todos os campos s�o obrigat�rios.");
    }
    // Salvar a imagem
    var uploadsPath = Path.Combine("wwwroot/uploads");
    if (!Directory.Exists(uploadsPath))
    {
        Directory.CreateDirectory(uploadsPath);
    }

    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);
    var filePath = Path.Combine(uploadsPath, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await imagem.CopyToAsync(stream);
    }

    // Criar o item
    var novoItem = new Item
    {
        Nome = nome,
        Descricao = descricao,
        Preco = preco,
        CategoriaId = categoriaId,
        ImagemUrl = $"/uploads/{fileName}"
    };

    await context.Itens.AddAsync(novoItem);
    await context.SaveChangesAsync();

    return Results.Created($"/api/itens/{novoItem.Id}", novoItem);
});

//rota que não foi utilizada
// app.MapPost("/api/upload", async (IFormFile file) =>
// {
//     if (file is null || file.Length == 0)
//         return Results.BadRequest("Arquivo inv�lido");

//     var filePath = Path.Combine("wwwroot/uploads", file.FileName);

//     using (var stream = new FileStream(filePath, FileMode.Create))
//     {
//         await file.CopyToAsync(stream);
//     }

//     return Results.Ok(new { Url = $"/uploads/{file.FileName}" });
// });

app.MapPut("/api/itens/{id:long}", async (HttpRequest request, long id, AppDbContext context) =>
{
    var item = await context.Itens.FindAsync(id);
    if (item is null) return Results.NotFound();

    var form = await request.ReadFormAsync();
    var nome = form["nome"];
    var descricao = form["descricao"];
    var precoString = form["preco"];
    var categoriaIdString = form["categoriaId"];
    var imagem = form.Files.GetFile("imagem");

    if (string.IsNullOrEmpty(nome) ||
        string.IsNullOrEmpty(precoString) ||
        string.IsNullOrEmpty(categoriaIdString))
    {
        return Results.BadRequest("Campos obrigatórios estão faltando.");
    }

    if (!decimal.TryParse(precoString, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal preco) || preco <= 0 ||
        !long.TryParse(categoriaIdString, out long categoriaId) || categoriaId <= 0)
    {
        return Results.BadRequest("Preço ou Categoria inválidos.");
    }

    // Atualizar os campos do item
    item.Nome = nome;
    item.Preco = preco;
    item.CategoriaId = categoriaId;
    item.Descricao = descricao;
    item.Ingredientes = form["ingredientes"];

    // Processar a nova imagem, se fornecida
    if (imagem is not null)
    {
        var uploadsPath = Path.Combine("wwwroot/uploads");
        if (!Directory.Exists(uploadsPath))
        {
            Directory.CreateDirectory(uploadsPath);
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);
        var filePath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imagem.CopyToAsync(stream);
        }

        // Atualizar o caminho da imagem
        item.ImagemUrl = $"/uploads/{fileName}";
    }

    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/itens/{id:long}", async (long id, AppDbContext context) =>
{
    var item = await context.Itens.FindAsync(id);
    if (item == null) return Results.NotFound();

    context.Itens.Remove(item);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

// 3. Pedidos
app.MapGet("/api/pedidos", async (AppDbContext context) =>
{
    var pedidos = await context.Pedidos
        .Include(p => p.DetalhesPedido) // Incluir os detalhes do pedido
        .ThenInclude(d => d.Item) // Incluir o item dentro de cada detalhe do pedido
        .ToListAsync();

    return Results.Ok(pedidos);
});

app.MapGet("/api/pedidos/{id:long}", async (long id, AppDbContext context) =>
{
    var pedido = await context.Pedidos
        .Include(p => p.DetalhesPedido)
        .ThenInclude(dp => dp.Item)
        .FirstOrDefaultAsync(p => p.Id == id);

    return pedido == null ? Results.NotFound() : Results.Ok(pedido);
});

app.MapPost("/api/pedidos", async (Pedido pedido, AppDbContext context) =>
{
    await context.Pedidos.AddAsync(pedido);
    await context.SaveChangesAsync();
    return Results.Created($"/api/pedidos/{pedido.Id}", pedido);
});

app.MapPut("/api/pedidos/{id:long}/chamar-garcom", async (long id, AppDbContext context) =>
{
    var pedido = await context.Pedidos.FindAsync(id);
    if (pedido == null) return Results.NotFound();

    pedido.ChamadoGarcom = true;
    await context.SaveChangesAsync();
    return Results.Ok("Gar�om foi chamado.");
});

app.MapDelete("/api/pedidos/{id:long}", async (long id, AppDbContext context) =>
{
    var pedido = await context.Pedidos.FindAsync(id);
    if (pedido == null) return Results.NotFound();

    context.Pedidos.Remove(pedido);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/api/pedidos/{id:long}/total", async (long id, AppDbContext context) =>
{
    var pedido = await context.Pedidos
    .Include(p => p.DetalhesPedido)
    .ThenInclude(dp => dp.Item)
    .FirstOrDefaultAsync(p => p.Id == id);

    if (pedido is null) return Results.NotFound("Pedido não encontrado");
    decimal total = pedido.DetalhesPedido.Sum(dp => dp.Quantidade * dp.Item.Preco);

    return Results.Ok(total);
});

app.MapPost("/api/pedidos/adicionar-detalhe", async ([FromBody] AdicionarDetalhePedidoSchema detalhePedido, AppDbContext context) =>
{
    // Verificar se o pedido existe
    var pedido = await context.Pedidos.FirstOrDefaultAsync(p => p.Id == detalhePedido.PedidoId);
    if (pedido is null)
    {
        return Results.NotFound("Pedido não encontrado.");
    }

    // Verificar se o item existe
    var item = await context.Itens.FirstOrDefaultAsync(i => i.Id == detalhePedido.ItemId);
    if (item is null)
    {
        return Results.NotFound("Item não encontrado.");
    }

    // Associar o pedido e o item ao detalhe
    DetalhePedido novoDetalhePedido = new DetalhePedido
    {
        Quantidade = detalhePedido.Quantidade,
        PedidoId = pedido.Id,
        ItemId = item.Id,
        Item = item,
    };
    // Adicionar o detalhe ao pedido
    pedido.DetalhesPedido.Add(novoDetalhePedido);
    // Salvar as mudanças no banco de dados
    await context.SaveChangesAsync();

    var detalhesAtualizados = await context.DetalhesPedidos
    .Where(p => p.PedidoId == pedido.Id)
    .Select(d => new DetalhePedido
    {
        Id = d.Id,
        Quantidade = d.Quantidade,
        ItemId = d.ItemId,
        Item = d.Item // Isso retorna apenas os dados do Item, não do Pedido
    })
    .ToListAsync();

    // Retornar a lista atualizada de detalhes de pedido
    return Results.Ok(detalhesAtualizados);
});

app.MapGet("/api/pedidos/mesa/{numeroMesa:long}", async (long numeroMesa, AppDbContext context) =>
{
    var pedidos = await context.Pedidos.Where(p => p.NumeroMesaCliente == numeroMesa).Include(p => p.DetalhesPedido).ThenInclude(dp => dp.Item).ToListAsync();
    return Results.Ok(pedidos);
});
// 4. Detalhes do Pedido
app.MapPost("/api/detalhe-pedidos", async (DetalhePedido detalhe, AppDbContext context) =>
{
    await context.DetalhesPedidos.AddAsync(detalhe);
    await context.SaveChangesAsync();
    return Results.Created($"/api/detalhe-pedidos/{detalhe.Id}", detalhe);
});

app.MapGet("/api/detalhe-pedidos/{pedidoId:long}", async (long pedidoId, AppDbContext context) =>
{
    var detalhes = await context.DetalhesPedidos
        .Where(dp => dp.PedidoId == pedidoId)
        .Include(dp => dp.Item)
        .ToListAsync();

    return Results.Ok(detalhes);
});

// Rodando o aplicativo
app.Run();
