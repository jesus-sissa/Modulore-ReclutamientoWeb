using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.LoginPath = "/User/Login";
        options.Cookie.Name = "ModuloReclutamientoWeb";
        options.AccessDeniedPath = "/Forbidden/Illegal";

    });
/*
 * politicas se agrega para seguridad mas especifica
 * la politica se configura segun que claim o claims queremos que tenga el usuario para acceder a cierta vista o 
 * limitar ciertas acciones a usuarios, mientras cumpla con la polica proda acceder a la vista o ejecutar las acciones deseadas
 * builder.Services.AddAuthorization(options => 
 *       {
 *          options.AddPolicy("RestarProcess", policy => policy.RequireClaim("Restart","Y")
 *                  .RequireClaim("Manager", "Y")
 *       );
 *                                  });
 * 
 * En case de dudas revisar : https://www.youtube.com/watch?v=GbNhnksUS0k para orientacion de la implementacion
 * */

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
//app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=User}/{action=Login}/{id?}");
    pattern: "{controller=User}/{action=Login}/");

app.Run();
