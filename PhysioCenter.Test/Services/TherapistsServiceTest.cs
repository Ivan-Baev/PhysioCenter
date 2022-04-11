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
    using System.Threading;
    using System.Threading.Tasks;

    public class TherapistsServiceTest
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
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();

            var store = new Mock<IUserStore<IdentityUser>>(MockBehavior.Strict);
            store.As<IUserPasswordStore<IdentityUser>>()
                 .Setup(x => x.FindByIdAsync(It.IsAny<string>(), CancellationToken.None))
                 .ReturnsAsync(new IdentityUser()
                 {
                     Id = "446a2198-61a6-4e5a-974a-33b8b8ebc7a6",
                     UserName = "therapist1",
                     Email = "therapist1@physiocenter.com",
                 });
            store.Setup(x => x.DeleteAsync(It.IsAny<IdentityUser>(), CancellationToken.None))
                .ReturnsAsync(IdentityResult.Success);

            userManager = TestUserManager<IdentityUser>(store.Object);
            mockTherapistsService = new Mock<TherapistsService>(repo, userManager).Object;
            await SeedDbAsync(repo);
        }

        [Test]
        public void GetByIdAsyncMustThrowErrorInvalidId()
        {
            var id = new Guid("00969b29-91b6-4ba1-ba3e-d78f463fee42");

            Assert.That(async () => await mockTherapistsService.GetByIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void GetByIdAsyncMustNotThrowErrorInvalidId()
        {
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");

            Assert.DoesNotThrowAsync(async () => await mockTherapistsService.GetByIdAsync(id));
        }

        [Test]
        public void FindTherapistByUserIdShouldThrowError()
        {
            var id = "00078f3d-ada4-4ea2-8916-a40f9f72cd60";
            Assert.That(async () => await mockTherapistsService.FindTherapistById(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void FindTherapistByUserIdShouldNotThrowError()
        {
            var id = "446a2198-61a6-4e5a-974a-33b8b8ebc7a6";
            Assert.DoesNotThrowAsync(async () => await mockTherapistsService.FindTherapistById(id));
        }

        [Test]
        public void FindTherapistByIdShouldThrowError()
        {
            var id = new Guid("000ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            Assert.That(async () => await mockTherapistsService.FindTherapistById(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void FindTherapistByIdShouldNotThrowError()
        {
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            Assert.DoesNotThrowAsync(async () => await mockTherapistsService.FindTherapistById(id));
        }

        [Test]
        public async Task GetAllAsyncShouldNotThrowError()
        {
            Assert.DoesNotThrowAsync(async () => await mockTherapistsService.GetAllAsync());
            var actual = await mockTherapistsService.GetAllAsync();
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actual.Count());
        }

        [Test]
        public void AddAsyncShouldNotThrowError()
        {
            var therapist = new Therapist()
            {
                Id = new Guid("604ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                UserId = "3bd8f642-bbaf-4e55-8906-cb45c73e8357",
                Description = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                FullName = "testtest",
                ProfileImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648755969/PhysioCenter%20Images/member1_ml8eld.jpg"
            };

            Assert.DoesNotThrowAsync(async () => await mockTherapistsService.AddAsync(therapist));
        }

        [Test]
        public async Task UpdateDetailsAsyncAsyncShouldNotThrowError()
        {
            var therapist = new Therapist()
            {
                Id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9"),
                UserId = "446a2198-61a6-4e5a-974a-33b8b8ebc7a6",
                Description = "Updated",
                FullName = "testtest",
                ProfileImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648755969/PhysioCenter%20Images/member1_ml8eld.jpg"
            };
            Assert.DoesNotThrowAsync(async () => await mockTherapistsService.UpdateDetailsAsync(therapist));

            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            var actual = await mockTherapistsService.GetByIdAsync(id);
            var expectedDescription = "Updated";

            Assert.AreEqual(expectedDescription, actual.Description);
        }

        [Test]
        public void DeleteAsyncAsyncShouldThrowError()
        {
            var id = new Guid("104ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            Assert.That(async () => await mockTherapistsService.DeleteAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This therapist does not exist!"));
        }

        [Test]
        public void DeleteAsyncAsyncShouldNotThrowErrorValidTherapistId()
        {
            var id = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9");
            Assert.DoesNotThrowAsync(async () => await mockTherapistsService.DeleteAsync(id));
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
                FullName = "testtest",
                ProfileImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648755969/PhysioCenter%20Images/member1_ml8eld.jpg"
            };

            await repo.AddRangeAsync(users);
            await repo.AddAsync(therapist);

            await repo.SaveChangesAsync();
        }
    }
}