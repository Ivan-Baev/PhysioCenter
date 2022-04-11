namespace PhysioCenter.Test.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Moq;

    using NUnit.Framework;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Core.Services;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TherapistsServicesServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private UserManager<IdentityUser> userManager;
        private ITherapistsService? mockTherapistsService;

        private UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<ITherapistsService, TherapistsService>()
                .AddSingleton<IServicesService, ServicesService>()
                .AddSingleton<ICategoriesService, CategoriesService>()
                .AddSingleton<ITherapistsServicesService, TherapistsServicesService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            userManager = TestUserManager<IdentityUser>();
            mockTherapistsService = new Mock<TherapistsService>(repo, userManager).Object;

            serviceProvider = serviceCollection.AddSingleton(mockTherapistsService)
                .BuildServiceProvider();

            await SeedDbAsync(repo);
        }

        [Test]
        public void AddAllServicesToTherapistIdShouldNotThrowError()
        {
            var services = new List<Service>()
            {
                new Service()
                {
                Id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Description = "Loresm Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Name = "Test Service1",
                Price = 25,
                CategoryId = new Guid("22969b29-91b6-4ba1-ba3e-d78f463fee32")
                },
                new Service()
                {
                Id = new Guid("21969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Name = "TestService2",
                Price = 30,
                CategoryId = new Guid("33969b29-91b6-4ba1-ba3e-d78f463fee32")
                },
            };
            var service = serviceProvider.GetService<ITherapistsServicesService>();
            var id = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            Assert.DoesNotThrowAsync(async () => await service.AddAllServicesToTherapistId(services, id));
        }

        [Test]
        public void AddAllTherapistsToServiceIdShouldNotThrowError()
        {
            var therapists = new List<Therapist>()
            {
                new Therapist()
            {
                Id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                UserId = "446a2198-61a6-4e5a-974a-33b8b8ebc7a6",
                Description = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                FullName = "test",
                ProfileImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648755969/PhysioCenter%20Images/member1_ml8eld.jpg"
            },
            new Therapist()
            {
                Id = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                UserId = "3bd8f642-bbaf-4e55-8906-cb45c73e8357",
                Description = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                FullName = "test2",
                ProfileImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648755969/PhysioCenter%20Images/member1_ml8eld.jpg"
            }
        };

            var service = serviceProvider.GetService<ITherapistsServicesService>();
            var id = new Guid("21969b29-91b6-4ba1-ba3e-d78f463fee32");
            Assert.DoesNotThrowAsync(async () => await service.AddAllTherapistsToServiceId(therapists, id));
        }

        [Test]
        public void GetTherapistServicesByIdAsyncShouldThrowError()
        {
            var id = new Guid("00078f3d-ada4-4ea2-8916-a40f9f72cd60");
            var service = serviceProvider.GetService<ITherapistsServicesService>();
            Assert.That(async () => await service.GetTherapistServicesByIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void GetTherapistServicesByIdAsyncShouldNotThrowErrorCorrectId()
        {
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var service = serviceProvider.GetService<ITherapistsServicesService>();
            Assert.DoesNotThrowAsync(async () => await service.GetTherapistServicesByIdAsync(id));
        }

        [Test]
        public void GetProvidedTherapistServicesByIdAsyncShouldThrowErrorInvalidId()
        {
            var id = new Guid("00078f3d-ada4-4ea2-8916-a40f9f72cd60");
            var service = serviceProvider.GetService<ITherapistsServicesService>();
            Assert.That(async () => await service.GetProvidedTherapistServicesByIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void GetProvidedTherapistServicesByIdAsyncShouldNotThrowErrorCorrectId()
        {
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var service = serviceProvider.GetService<ITherapistsServicesService>();
            Assert.DoesNotThrowAsync(async () => await service.GetProvidedTherapistServicesByIdAsync(id));
        }

        [Test]
        public void ChangeProvidedStatusAsyncShouldThrowErrorInvalidTherapistId()
        {
            var therapistId = new Guid("00b78f3d-ada4-4ea2-8916-a40f9f72cd60");
            var serviceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");
            var service = serviceProvider.GetService<ITherapistsServicesService>();

            Assert.That(async () => await service.ChangeProvidedStatusAsync(therapistId, serviceId),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void ChangeProvidedStatusAsyncShouldThrowErrorInvalidServiceId()
        {
            var therapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var serviceId = new Guid("00969b29-91b6-4ba1-ba3e-d78f463fee32");
            var service = serviceProvider.GetService<ITherapistsServicesService>();

            Assert.That(async () => await service.ChangeProvidedStatusAsync(therapistId, serviceId),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This service does not exist!"));
        }

        [Test]
        public void ChangeProvidedStatusAsyncShouldThrowErrorIfNull()
        {
            var therapistId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var serviceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");
            var service = serviceProvider.GetService<ITherapistsServicesService>();

            Assert.That(async () => await service.ChangeProvidedStatusAsync(therapistId, serviceId),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not provide the chosen service. Unable to disable."));
        }

        [Test]
        public void ChangeProvidedStatusAsyncShouldNotThrowErrorValidData()
        {
            var therapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var serviceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");
            var service = serviceProvider.GetService<ITherapistsServicesService>();

            Assert.DoesNotThrowAsync(async () => await service.ChangeProvidedStatusAsync(therapistId, serviceId));
        }

        [Test]
        public void FindTherapistServiceByIdShouldThrowErrorInvalidTherapistId()
        {
            var therapistId = new Guid("00b78f3d-ada4-4ea2-8916-a40f9f72cd60");
            var serviceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");
            var service = serviceProvider.GetService<ITherapistsServicesService>();

            Assert.That(async () => await service.FindTherapistServiceById(therapistId, serviceId),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void FindTherapistServiceByIdShouldThrowErrorInvalidServiceId()
        {
            var therapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var serviceId = new Guid("00969b29-91b6-4ba1-ba3e-d78f463fee32");
            var service = serviceProvider.GetService<ITherapistsServicesService>();

            Assert.That(async () => await service.FindTherapistServiceById(therapistId, serviceId),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This service does not exist!"));
        }

        [Test]
        public void FindTherapistServiceByIdShouldThrowErrorIfNull()
        {
            var therapistId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var serviceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");
            var service = serviceProvider.GetService<ITherapistsServicesService>();

            Assert.That(async () => await service.FindTherapistServiceById(therapistId, serviceId),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This service is not provided by the therapist!"));
        }

        [Test]
        public async Task FindTherapistServiceByIdShouldNotThrowErrorValidData()
        {
            var therapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var serviceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");
            var service = serviceProvider.GetService<ITherapistsServicesService>();
            Assert.DoesNotThrowAsync(async () => await service.FindTherapistServiceById(therapistId, serviceId));
            var result = await service.FindTherapistServiceById(therapistId, serviceId);
            Assert.AreEqual(serviceId, result.ServiceId);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var users = new List<IdentityUser>()
             {
                new IdentityUser()
            {
                Id = "446a2198-61a6-4e5a-974a-33b8b8ebc7a6",
                UserName = "therapist1",
                Email = "therapist1@physiocenter.com",
            },
             new IdentityUser()
             {
                 Id = "3bd8f642-bbaf-4e55-8906-cb45c73e8357",
                 UserName = "therapist2",
                 Email = "therapist2@physiocenter.com",
             },
                        };

            var therapist = new Therapist()
            {
                Id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                UserId = "446a2198-61a6-4e5a-974a-33b8b8ebc7a6",
                Description = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                FullName = "test",
                ProfileImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648755969/PhysioCenter%20Images/member1_ml8eld.jpg"
            };

            var therapist2 = new Therapist()
            {
                Id = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                UserId = "3bd8f642-bbaf-4e55-8906-cb45c73e8357",
                Description = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                FullName = "test2",
                ProfileImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648755969/PhysioCenter%20Images/member1_ml8eld.jpg"
            };

            var categories = new List<Category>()
            {
                new Category()
                {
                Id = new Guid("22969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Description = "Loresm Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Name = "Test Cat",
                },
                new Category()
                {
                Id = new Guid("33969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Description = "Loresm Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Name = "Test Cat2",
                },
            };

            var services = new List<Service>()
            {
                new Service()
                {
                Id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Description = "Loresm Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Name = "Test Service1",
                Price = 25,
                CategoryId = new Guid("22969b29-91b6-4ba1-ba3e-d78f463fee32")
                },
                new Service()
                {
                Id = new Guid("21969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Name = "TestService2",
                Price = 30,
                CategoryId = new Guid("33969b29-91b6-4ba1-ba3e-d78f463fee32")
                },
            };

            var therapistService = new TherapistService()
            {
                TherapistId = therapist.Id,
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                isProvided = true
            };

            await repo.AddRangeAsync(users);
            await repo.AddAsync(therapist);
            await repo.AddAsync(therapist2);
            await repo.AddRangeAsync(categories);
            await repo.AddRangeAsync(services);
            await repo.AddAsync(therapistService);

            await repo.SaveChangesAsync();
        }
    }
}