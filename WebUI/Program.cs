using ProductOverview.Application;
using ProductOverview.Infrastructure;
using ProductOverview.Infrastructure.DbContexts;
using WebUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterApplicationServices();
builder.Services.RegisterInfrastructureServices();
builder.Services.AddWebUIServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    using var scope = app.Services.CreateScope();
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initialiser.SeedAsync(CancellationToken.None);
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health")
   .UseSwagger()
   .UseSwaggerUI()
   .UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting()
   .UseCors("DevCorsPolicy");

app.MapControllers();

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.Run();