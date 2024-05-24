using Microsoft.EntityFrameworkCore;
using xilopro2.Data;
using xilopro2.Helpers.Interfaces;
using xilopro2.Helpers;
using xilopro2.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using xilopro2.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DataContext>(provider => {
    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection"));
    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    optionsBuilder.EnableSensitiveDataLogging();
    return new DataContext(optionsBuilder.Options);
});


//inyectando interface
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<ICombos, CombosHelper>();
builder.Services.AddScoped<IImageHelper, ImageHelper>();
builder.Services.AddScoped<ISwithUsers, SwitchUserHelper>();
builder.Services.AddTransient<IEmailSender, MailJetSenderService>();
builder.Services.AddTransient<IMailService, EmailSenderService>();
builder.Services.AddScoped<IEntityModelConverter, EntityModelConverter>();
builder.Services.AddScoped<SeederDB>();

builder.Services.AddIdentity<AppUser, IdentityRole>(cfg =>
{
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false;
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<DataContext>()
.AddDefaultUI()
.AddDefaultTokenProviders(); //agregamso el token


builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied";
});

var app = builder.Build();

//inicio seederdb
FeederData();
void FeederData()
{

    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using (IServiceScope? scope = scopeFactory.CreateScope())
    {
        SeederDB? service = scope.ServiceProvider.GetService<SeederDB>();
        service.FeederAsync().Wait();
    }

}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
