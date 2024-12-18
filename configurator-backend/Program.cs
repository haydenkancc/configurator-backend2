using Microsoft.EntityFrameworkCore;
using Configurator.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddDbContext<CatalogueContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CatalogueContext>();
    context.Database.EnsureCreated();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.Run();



