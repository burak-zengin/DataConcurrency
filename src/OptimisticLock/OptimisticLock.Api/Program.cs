using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OptimisticLock.Domain.Categories;
using OptimisticLock.Domain.Products;
using OptimisticLock.Infrastructure;
using OptimisticLock.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterInfrastructureServices(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/api/categories", async (
    OptimisticLockDbContext context,
    Category category,
    CancellationToken cancellationToken) =>
{
    context.Add(category);
    await context.SaveChangesAsync(cancellationToken);

    return category.Id;
})
.WithName("CategoriesPost")
.WithOpenApi();

app.MapGet("/api/categories/{id}", (
    OptimisticLockDbContext context,
    int id,
    CancellationToken cancellationToken) =>
{
    return context.Categories.FindAsync(id, cancellationToken);
})
.WithName("CategoriesGet")
.WithOpenApi();

app.MapPut("/api/categories", async (
    OptimisticLockDbContext context,
    Category category,
    CancellationToken cancellationToken) =>
{
    context.Categories.Update(category);

    try
    {
        await context.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }
    catch (DbUpdateConcurrencyException e)
    {
        return Results.BadRequest("Concurrency exception.");
    }
    catch
    {
        return Results.BadRequest("An unknown error occurred.");
    }
})
.WithName("CategoriesPut")
.WithOpenApi();

app.MapPost("/api/products", async (
    OptimisticLockDbContext context,
    Product product,
    CancellationToken cancellationToken) =>
{
    context.Add(product);
    await context.SaveChangesAsync(cancellationToken);

    return product.Id;
})
.WithName("ProductsPost")
.WithOpenApi();

app.MapGet("/api/products/{id}", (
    OptimisticLockDbContext context,
    int id,
    CancellationToken cancellationToken) =>
{
    return context.Products.FindAsync(id, cancellationToken);
})
.WithName("ProductsGet")
.WithOpenApi();

app.MapPut("/api/products", async Task<IResult> (
    OptimisticLockDbContext context,
    Product product,
    CancellationToken cancellationToken) =>
{
    context.Products.Update(product);

    try
    {
        await context.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }
    catch (DbUpdateConcurrencyException e)
    {
        return Results.BadRequest("Concurrency exception.");
    }
    catch
    {
        return Results.BadRequest("An unknown error occurred.");
    }
})
.WithName("ProductsPut")
.WithOpenApi();

app.Run();