using ConnectAzureSQL.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<CourseService>();

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