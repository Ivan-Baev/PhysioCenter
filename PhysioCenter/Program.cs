using CloudinaryDotNet;

using Ganss.XSS;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PhysioCenter.Core.Contracts;
using PhysioCenter.Core.Services;
using PhysioCenter.Core.Services.Appointments;
using PhysioCenter.Core.Services.Clients;
using PhysioCenter.Core.Services.Services;
using PhysioCenter.Core.Services.Therapists;
using PhysioCenter.Core.Utilities.Constants;
using PhysioCenter.Infrastructure.Data;
using PhysioCenter.Infrastructure.Data.Common;
using PhysioCenter.Infrastructure.Data.Repository;
using PhysioCenter.Infrastructure.Data.Seeding;
using PhysioCenter.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var test = builder.Configuration.GetValue(typeof(string), "TestProperty");
;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Added EF Core resilient connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseSqlServer(connectionString,
         sqlServerOptionsAction: sqlOptions =>
     {
         sqlOptions.EnableRetryOnFailure(
         maxRetryCount: 5,
         maxRetryDelay: TimeSpan.FromSeconds(30),
         errorNumbersToAdd: null);
     }));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstant.NormalDateFormat));
        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
    });

builder.Services.AddAutoMapper(typeof(Program));

// HTML sanitizer - check if it works like this?
builder.Services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();

Cloudinary cloudinary = new(new Account
{
});

builder.Services.AddSingleton(cloudinary);

// Application services

builder.Services.AddTransient<IApplicationDbRepository, ApplicationDbRepository>();
builder.Services.AddTransient<ICloudinaryService, CloudinaryService>();
builder.Services.AddTransient<IAppointmentsService, AppointmentsService>();
builder.Services.AddTransient<IServicesService, ServicesService>();
builder.Services.AddTransient<IClientsService, ClientsService>();
builder.Services.AddTransient<ITherapistsService, TherapistsService>();
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IReviewsService, ReviewsService>();
builder.Services.AddTransient<INotesService, NotesService>();
builder.Services.AddTransient<IBlogsService, BlogsService>();
builder.Services.AddTransient<ITherapistsServicesService, TherapistsServicesService>();
builder.Services.AddTransient<ITherapistsClientsService, TherapistsClientsService>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute("paging", "{area:exists}/{controller}/{action}");
                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });

app.Run();