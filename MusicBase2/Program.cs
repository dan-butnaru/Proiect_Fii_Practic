using MusicBase;
using Microsoft.EntityFrameworkCore;
using MusicBase.Services;
using MusicBase.Repositories.Songs;
using MusicBase.Repositories.Users;
using MusicBase.Repositories.Artists;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
.AddSqlServer<MusicContext>(builder.Configuration.GetConnectionString("MusicBase"))
  .AddScoped<IMusicUnitOfWork, MusicUnitOfWork>()
  .AddScoped<IUserRepository, UserRepository>()
  .AddScoped<ISongRepository, SongRepository>()
  .AddScoped<IArtistRepository, ArtistRepository>()
  .AddScoped<ICryptographyService, CryptographyService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
    .AddHttpContextAccessor();

builder.Services.AddControllersWithViews();



//Configure authentication
builder.Services
  .AddAuthorization()
  .AddAuthentication(AuthMusicConstants.Schema)
  .AddCookie(AuthMusicConstants.Schema);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
