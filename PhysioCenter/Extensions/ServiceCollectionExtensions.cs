namespace PhysioCenter.Extensions
{
    using CloudinaryDotNet;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Adapters;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Core.Services;
    using PhysioCenter.Core.Utilities.Constants;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Repository;
    using PhysioCenter.ModelBinders;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(connectionString,
                     sqlServerOptionsAction: sqlOptions =>
                     {
                         sqlOptions.EnableRetryOnFailure(
             maxRetryCount: 5,
             maxRetryDelay: TimeSpan.FromSeconds(30),
             errorNumbersToAdd: null);
                     }));

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddTransient<IApplicationDbRepository, ApplicationDbRepository>()
                .AddTransient<ICloudinaryService, CloudinaryService>()
                .AddTransient<IHomeViewModelAdapter, HomeViewModelAdapter>()
                .AddTransient<IAppointmentsService, AppointmentsService>()
                .AddTransient<IServicesService, ServicesService>()
                .AddTransient<IClientsService, ClientsService>()
                .AddTransient<ITherapistsService, TherapistsService>()
                .AddTransient<ICategoriesService, CategoriesService>()
                .AddTransient<IReviewsService, ReviewsService>()
                .AddTransient<INotesService, NotesService>()
                .AddTransient<IBlogsService, BlogsService>()
                .AddTransient<ITherapistsServicesService, TherapistsServicesService>()
                .AddTransient<ITherapistsClientsService, TherapistsClientsService>();

            return services;
        }

        public static IServiceCollection AddApplicationControllers(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddMvcOptions(options =>
                    {
                       /* options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())*/;
                        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstant.NormalDateFormat));
                        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
                    });

            return services;
        }

        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            Cloudinary cloudinary = new(new Account(

           configuration["Cloudinary:Cloud"],
           configuration["Cloudinary:ApiKey"],
           configuration["Cloudinary:ApiSecret"])
);

            services.AddSingleton(cloudinary);

            return services;
        }

        public static IServiceCollection AddCookiePolicyOptions(this IServiceCollection services)
        {
            services.AddCookiePolicy(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            return services;
        }
    }
}