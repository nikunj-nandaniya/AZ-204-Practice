using ConnectAzureSQL.Services;


namespace ConnectAzureSQL
{
    public class Startup
    {

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Ensure to add the services
        //    services.AddMvc();
        //    services.AddTransient<CourseService>();
        //}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Ensure to map the controllers accordingly
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Course}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }

    }
}
