using  tl2_tp10_2023_IvanDMir.repositorios;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();//el signo de exclamacion es para decir que no es null
builder.Services.AddSingleton<string>(CadenaDeConexion);
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<IUsuarioRepositorio,UsuarioRepositorio>();
builder.Services.AddScoped<ITableroRepositorio,TableroRepositorio>();
builder.Services.AddScoped<ITareasRepositorio,TareaRepositorio>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
