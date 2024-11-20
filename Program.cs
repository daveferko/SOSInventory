using Microsoft.EntityFrameworkCore;
using SOSInventory.Data;
using SOSInventory.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SosInventoryDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SosInventoryDbContext>();
    if (app.Environment.IsDevelopment())
    {
        dbContext.Database.EnsureDeleted();
    }
    dbContext.Database.Migrate();

    SeedData(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SeedData(SosInventoryDbContext context)
{
    if (!context.Items.Any())
    {
        var item = new Item { ItemName = "Blue X-Large T-Shirt" };
        context.Items.Add(item);
        context.SaveChanges();

        context.ItemReceipts.AddRange(
        new ItemReceipt { ReceiptReferenceNumber = "IR001", ItemId = item.ItemId, QuantityReceived = 52 }
        );

        context.Shipments.AddRange(
        new Shipment { ShipmentRefNumber = "SH001", ItemId = item.ItemId, QuantityShipped = 10 }
        );

        context.Adjustments.AddRange(
          new Adjustment { AdjustmentReferenceNumber = "AD001", ItemId = item.ItemId, QuantityAdjusted = 2 }
        );

        context.SaveChanges();
    }
}