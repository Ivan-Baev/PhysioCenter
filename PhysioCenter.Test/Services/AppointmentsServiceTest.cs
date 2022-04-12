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
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentsServiceTest
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
                .AddSingleton<IClientsService, ClientsService>()
                .AddSingleton<IServicesService, ServicesService>()
                .AddSingleton<ICategoriesService, CategoriesService>()
                .AddSingleton<ITherapistsServicesService, TherapistsServicesService>()
                .AddSingleton<IAppointmentsService, AppointmentsService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            userManager = TestUserManager<IdentityUser>();
            mockTherapistsService = new Mock<TherapistsService>(repo, userManager).Object;

            serviceProvider = serviceCollection.AddSingleton(mockTherapistsService)
                .BuildServiceProvider();

            await SeedDbAsync(repo);
        }

        [Test]
        public void GetByIdAsyncdShouldNotThrowError()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32");
            Assert.DoesNotThrowAsync(async () => await service.GetByIdAsync(id));
        }

        [Test]
        public void GetByIdAsyncdShouldThrowErrorInvalidId()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("00069b29-91b6-4ba1-ba3e-d78f463fee32");
            Assert.That(async () => await service.GetByIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("The provided id does not exist"));
        }

        [Test]
        public void GetAllAsyncShouldNotThrowError()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32");
            Assert.DoesNotThrowAsync(async () => await service.GetAllAsync(1, 10));
        }

        [Test]
        public async Task GetAllAsyncShouldNotThrowErrorWithWrongClientName()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var result = await service.GetAllAsync(1, 10, "asd");
            Assert.DoesNotThrowAsync(async () => await service.GetAllAsync(1, 10, "asd"));
            var expectedCount = 0;
            var actualCount = result.Count();
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public async Task GetAllAsyncShouldNotThrowErrorWithValidClientName()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var result = await service.GetAllAsync(1, 10, "test2");
            Assert.DoesNotThrowAsync(async () => await service.GetAllAsync(1, 10, "test2"));
            var expectedCount = 1;
            var actualCount = result.Count();
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void GetTodayByTherapistIdAsyncShouldThrowErrorInvalidTherapistId()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("00169b29-91b6-4ba1-ba3e-d78f463fee32");
            Assert.That(async () => await service.GetTodayByTherapistIdAsync(id, null),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void GetTodayByTherapistIdAsyncShouldNotThrowErrorValidTherapistId()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            Assert.DoesNotThrowAsync(async () => await service.GetTodayByTherapistIdAsync(id, null));
        }

        [Test]
        public async Task GetTodayByTherapistIdAsyncShouldReturnOneAppointmentForToday()
        {
            var date = DateTime.UtcNow;
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var actual = await service.GetTodayByTherapistIdAsync(id, date);
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
        }

        [Test]
        public async Task GetTodayByTherapistIdAsyncShouldReturnOneAppointmentForNull()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var actual = await service.GetTodayByTherapistIdAsync(id, null);
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
        }

        [Test]
        [TestCase("05/11/2022")]
        [TestCase("06/11/2022")]
        [TestCase("07/11/2022")]
        public async Task GetTodayByTherapistIdAsyncShouldReturnZeroAppointment(DateTime? date)
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var actual = await service.GetTodayByTherapistIdAsync(id, date);
            var expectedCount = 0;
            Assert.AreEqual(expectedCount, actual.Count());
        }

        [Test]
        public void GetUpcomingByTherapistIdAsyncShouldThrowErrorInvalidTherapistId()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("00169b29-91b6-4ba1-ba3e-d78f463fee32");
            Assert.That(async () => await service.GetUpcomingByTherapistIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void GetUpcomingByTherapistIdAsyncShouldNotThrowErrorValidTherapistId()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            Assert.DoesNotThrowAsync(async () => await service.GetUpcomingByTherapistIdAsync(id));
        }

        [Test]
        public async Task GetUpcomingByTherapistIdAsyncAsyncShouldReturnOneAppointmentForToday()
        {
            var service = serviceProvider.GetService<IAppointmentsService>();
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var actual = await service.GetUpcomingByTherapistIdAsync(id);
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
        }

        [Test]
        public void AddAsyncShouldThrowErrorInvalidTherapistId()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 10", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("004ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();

            Assert.That(async () => await service.AddAsync(appointment),

                    Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void AddAsyncShouldThrowErrorInvalidClientId()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 10", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("004ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();
            Assert.That(async () => await service.AddAsync(appointment),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This client does not exist!"));
        }

        [Test]
        public void AddAsyncShouldThrowErrorInvalidServiceId()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 10", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("01969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();
            Assert.That(async () => await service.AddAsync(appointment),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This service does not exist!"));
        }

        [Test]
        public void AddAsyncShouldNotThrowError()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 10", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("41169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();
            Assert.DoesNotThrowAsync(async () => await service.AddAsync(appointment));
        }

        [Test]
        public void UpdateAsyncShouldThrowErrorInvalidAppointmentId()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 10", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("01169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();

            Assert.That(async () => await service.UpdateAsync(appointment),

                    Throws.TypeOf<ArgumentException>().With.Message.EqualTo("The provided id does not exist"));
        }

        [Test]
        public void UpdateAsyncShouldThrowErrorInvalidTherapistId()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 10", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("004ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();
            Assert.That(async () => await service.UpdateAsync(appointment),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void UpdateAsyncShouldThrowErrorInvalidClientId()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 10", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("004ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();
            Assert.That(async () => await service.UpdateAsync(appointment),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This client does not exist!"));
        }

        [Test]
        public void UpdateAsyncShouldThrowErrorInvalidServiceId()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 10", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("01969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();
            Assert.That(async () => await service.UpdateAsync(appointment),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This service does not exist!"));
        }

        [Test]
        public async Task UpdateAsyncShouldNotThrowErrorAndUpdateCorrectly()
        {
            var appointment = new Appointment()
            {
                DateTime = DateTime.ParseExact("12/04/2022 11", "dd/mm/yyyy HH", CultureInfo.InvariantCulture),
                ClientId = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                Id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            var service = serviceProvider.GetService<IAppointmentsService>();
            Assert.DoesNotThrowAsync(async () => await service.UpdateAsync(appointment));
            var result = await service.GetByIdAsync(appointment.Id);
            var expectedDate = DateTime.ParseExact("12/04/2022 11", "dd/mm/yyyy HH", CultureInfo.InvariantCulture);
            Assert.AreEqual(expectedDate, result.DateTime);
        }

        [Test]
        public void DeleteAsyncShouldNotThrowErrorWithValidIdAndCorrectlyDelete()
        {
            var id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32");

            var service = serviceProvider.GetService<IAppointmentsService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteAsync(id));
            Assert.That(async () => await service.GetByIdAsync(id)
            , Throws.TypeOf<ArgumentException>().With.Message.EqualTo("The provided id does not exist"));
        }

        [Test]
        public void DeleteAsyncShouldThrowErrorWithInvalidId()
        {
            var id = new Guid("01169b29-91b6-4ba1-ba3e-d78f463fee32");

            var service = serviceProvider.GetService<IAppointmentsService>();
            Assert.That(async () => await service.DeleteAsync(id)
            , Throws.TypeOf<ArgumentException>().With.Message.EqualTo("The provided id does not exist"));
        }

        [Test]
        public async Task GetCountShouldReturnCorrectAppointmentCountWithValidName()
        {
            var name = "test2";

            var service = serviceProvider.GetService<IAppointmentsService>();
            var result = await service.GetCount(name);
            var expected = 1;
            Assert.That(result.Equals(expected));
        }

        [Test]
        public async Task GetCountShouldReturnCorrectAppointmentCountWithInvalidName()
        {
            var name = "test22";

            var service = serviceProvider.GetService<IAppointmentsService>();
            var result = await service.GetCount(name);
            var expected = 0;
            Assert.That(result.Equals(expected));
        }

        [Test]
        public async Task GetScheduleListShouldReturnCorrectSchedule()
        {
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var service = serviceProvider.GetService<IAppointmentsService>();
            var schedule = await service.GetScheduleList(id);
            var convertedSchedule = schedule.Value as List<string>;
            Assert.That(convertedSchedule.Count.Equals(1));
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
                 UserName = "client1",
                 Email = "client1@physiocenter.com",
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

            var client = new Client()
            {
                Id = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                UserId = "3bd8f642-bbaf-4e55-8906-cb45c73e8357",
                FullName = "test2",
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

            var appointment = new Appointment()
            {
                DateTime = DateTime.Now,
                ClientId = client.Id,
                ServiceId = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                TherapistId = therapist.Id,
                Id = new Guid("11169b29-91b6-4ba1-ba3e-d78f463fee32")
            };

            await repo.AddRangeAsync(users);
            await repo.AddAsync(therapist);
            await repo.AddAsync(client);
            await repo.AddRangeAsync(categories);
            await repo.AddRangeAsync(services);
            await repo.AddAsync(therapistService);
            await repo.AddAsync(appointment);

            await repo.SaveChangesAsync();
        }
    }
}