using Microsoft.EntityFrameworkCore;
using ProductApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductsContext>(options => options.UseSqlite("Data Source=products.db"));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseRouting();
app.Run();
