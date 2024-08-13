using Basis.Bookstore.Infraestructure.Bootstrap;
using BasisBookstore.Infraestructure.Bootstrap;
using BasisBookstore.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureDataServices(builder.Configuration);
builder.Services.ConfigureMediatrServices();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "pt-BR" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

ApplyMigrations(app);

app.Run();



static void ApplyMigrations(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<BookstoreContext>();
        db.Database.Migrate();
    }
}