var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// In-memory "static" repositories, registered as Singletons so the same
// instance (and therefore the same data) is shared across every request.
// This is what gives us: one shared product catalog, one active shopping
// cart, and a transaction history that survives until the app restarts.
builder.Services.AddSingleton<POS.Repositories.ProductRepository>();
builder.Services.AddSingleton<POS.Repositories.ShoppingCartRepository>();
builder.Services.AddSingleton<POS.Repositories.TransactionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for
    // production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
// main sol
