using ConnectAzureSQL.Services;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddControllersWithViews();
builder.Services.AddFeatureManagement();

builder.Services.AddTransient<CourseService>();

builder.Configuration.AddAzureAppConfiguration("Endpoint=https://appconfignik.azconfig.io;Id=FjkZ-lae-s0:BI6YAlRVsHUK4vAlXPz5;Secret=6Bz5FHY2nkhJCUB0zuZBViPQdNgTbYckDna1n8GYtvE=");

builder.Configuration.AddAzureAppConfiguration(options =>
                     options.Connect("Endpoint=https://appconfignik.azconfig.io;Id=FjkZ-lae-s0:BI6YAlRVsHUK4vAlXPz5;Secret=6Bz5FHY2nkhJCUB0zuZBViPQdNgTbYckDna1n8GYtvE=").UseFeatureFlags());




var app = builder.Build();

//var courseService = app.Services.GetRequiredService<CourseService>();
//app.MapGet("/", () => "Hello World!");

app.UseRouting();

// Ensure to map the controllers accordingly
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Course}/{action=Index}/{id?}");

});

app.Run();