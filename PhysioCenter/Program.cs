using CloudinaryDotNet;

using Ganss.XSS;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PhysioCenter.Adapters;
using PhysioCenter.Core.Contracts;
using PhysioCenter.Core.Services;
using PhysioCenter.Core.Utilities.Constants;
using PhysioCenter.Infrastructure.Data;
using PhysioCenter.Infrastructure.Data.Repository;
using PhysioCenter.Infrastructure.Data.Seeding;
using PhysioCenter.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstant.NormalDateFormat));
        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
    });

builder.Services.AddAutoMapper(typeof(Program));

// HTML sanitizer - check if it works like this?
builder.Services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();

Cloudinary cloudinary = new(new Account(

    builder.Configuration["Cloudinary:Cloud"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"])
);

builder.Services.AddSingleton(cloudinary);

builder.Services.AddCookiePolicy(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

builder.Services.AddTransient<IApplicationDbRepository, ApplicationDbRepository>();
builder.Services.AddTransient<ICloudinaryService, CloudinaryService>();
builder.Services.AddTransient<IHomeViewModelAdapter, HomeViewModelAdapter>();
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

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