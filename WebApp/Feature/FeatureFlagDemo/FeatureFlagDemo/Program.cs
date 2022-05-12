using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFeatureManagement();

builder.Configuration.AddAzureAppConfiguration(options =>
                     options.Connect("Endpoint=https://appconfignik.azconfig.io;Id=FjkZ-lae-s0:BI6YAlRVsHUK4vAlXPz5;Secret=6Bz5FHY2nkhJCUB0zuZBViPQdNgTbYckDna1n8GYtvE=").UseFeatureFlags());


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

app.Run();
