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

    public class TherapistsClientsServiceTest
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
                .AddSingleton<ITherapistsClientsService, TherapistsClientsService>()
                .AddSingleton<IClientsService, ClientsService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            UserManager<IdentityUser> usermanager = TestUserManager<IdentityUser>();
            mockTherapistsService = new Mock<TherapistsService>(repo, userManager).Object;

            serviceProvider = serviceCollection.AddSingleton(mockTherapistsService)
                .BuildServiceProvider();
            await SeedDbAsync(repo);
        }

        [Test]
        public void GetProvidedTherapistClientsByIdAsyncMustThrowErrorInvalidId()
        {
            var id = new Guid("00969b29-91b6-4ba1-ba3e-d78f463fee42");
            var service = serviceProvider.GetService<ITherapistsClientsService>();

            Assert.That(async () => await service.GetProvidedTherapistClientsByIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void GetProvidedTherapistClientsByIdAsyncMustNotThrowErrorInvalidId()
        {
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");

            var service = serviceProvider.GetService<ITherapistsClientsService>();

            Assert.DoesNotThrowAsync(async () => await service.GetProvidedTherapistClientsByIdAsync(id));
        }

        [Test]
        public void GetProvidedClientTherapistsByIdAsyncShouldThrowError()
        {
            var id = new Guid("00078f3d-ada4-4ea2-8916-a40f9f72cd60");
            var service = serviceProvider.GetService<ITherapistsClientsService>();
            Assert.That(async () => await service.GetProvidedClientTherapistsByIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This client does not exist!"));
        }

        [Test]
        public async Task GetProvidedClientTherapistsByIdAsyncShouldNotThrowError()
        {
            var id = new Guid("2db78f3d-ada4-4ea2-8916-a40f9f72cd60");
            var service = serviceProvider.GetService<ITherapistsClientsService>();

            Assert.DoesNotThrowAsync(async () => await service.GetProvidedClientTherapistsByIdAsync(id));

            var actual = await service.GetProvidedClientTherapistsByIdAsync(id);
            var actualclientId = actual.Select(x => x.ClientId).FirstOrDefault();

            var expected = new TherapistClient()
            {
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ClientId = new Guid("2db78f3d-ada4-4ea2-8916-a40f9f72cd60")
            };

            Assert.AreEqual(expected.ClientId, actualclientId);
        }

        [Test]
        public void AddTherapistClientAsyncNotThrowError()
        {
            var input = new TherapistClient()
            {
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                ClientId = new Guid("3db78f3d-ada4-4ea2-8916-a40f9f72cd60")
            };
            var service = serviceProvider.GetService<ITherapistsClientsService>();

            Assert.DoesNotThrowAsync(async () => await service.AddTherapistClientAsync(input));
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
             new IdentityUser()
             {
                 Id = "4bd8f642-bbaf-4e55-8906-cb45c73e8357",
                 UserName = "client2",
                 Email = "client2@physiocenter.com",
             }
            };

            var therapist = new Therapist()
            {
                Id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                UserId = "446a2198-61a6-4e5a-974a-33b8b8ebc7a6",
                Description = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                FullName = "testtest",
                ProfileImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648755969/PhysioCenter%20Images/member1_ml8eld.jpg"
            };

            var client = new Client()
            {
                Id = new Guid("2db78f3d-ada4-4ea2-8916-a40f9f72cd60"),
                UserId = "3bd8f642-bbaf-4e55-8906-cb45c73e8357",
                FullName = "Client testtest"
            };

            var client2 = new Client()
            {
                Id = new Guid("3db78f3d-ada4-4ea2-8916-a40f9f72cd60"),
                UserId = "4bd8f642-bbaf-4e55-8906-cb45c73e8357",
                FullName = "Client testtest"
            };

            var therapistClient = new TherapistClient()
            {
                ClientId = client.Id,
                TherapistId = therapist.Id
            };

            await repo.AddRangeAsync(users);
            await repo.AddAsync(therapist);
            await repo.AddAsync(client);
            await repo.AddAsync(client2);
            await repo.AddAsync(therapistClient);

            await repo.SaveChangesAsync();
        }
    }
}