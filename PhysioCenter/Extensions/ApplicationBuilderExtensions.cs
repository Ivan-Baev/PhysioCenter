namespace PhysioCenter.Extensions
{
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Seeding;
    using PhysioCenter.Hubs;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            return app;
        }

        public static IApplicationBuilder UseMyEndPoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute("paging", "{area:exists}/{controller}/{action}");
                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                    endpoints.MapHub<ChatHub>("/chatHub");
                });

            return app;
        }
    }
}