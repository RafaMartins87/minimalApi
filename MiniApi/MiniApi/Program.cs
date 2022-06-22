using Microsoft.EntityFrameworkCore;
using MiniApi.Db;
using MiniApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Clientes"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.MapGet("/Clientes/{id}", async (int id, AppDbContext dbContext) =>
      await dbContext.Clientes.FirstOrDefaultAsync(a => a.Id == id));

app.MapPost("/Clientes", async (Cliente cliente, AppDbContext dbContext) =>
{
    dbContext.Clientes.Add(cliente);
    await dbContext.SaveChangesAsync();
    return cliente;
});

app.MapPut("/Clientes/{id}", async (int id, Cliente cliente, AppDbContext dbContext) =>
{
    dbContext.Entry(cliente).State = EntityState.Modified;
    await dbContext.SaveChangesAsync();
    return cliente;
});

app.MapDelete("/Clientes/{id}", async (int id, AppDbContext dbContext) =>
{
    var cliente = await dbContext.Clientes.FirstOrDefaultAsync(a => a.Id == id);
    if (cliente != null)
    {
        dbContext.Clientes.Remove(cliente);
        await dbContext.SaveChangesAsync();
    }
    return;
});

app.UseSwaggerUI();

await app.RunAsync();


