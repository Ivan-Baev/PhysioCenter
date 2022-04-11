namespace PhysioCenter.Test.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Core.Services;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ClientsServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IClientsService, ClientsService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();

            await SeedDbAsync(repo);
        }

        [Test]
        public void GetAllAsyncMustNotThrowErrorValidId()
        {
            var service = serviceProvider.GetService<IClientsService>();

            Assert.DoesNotThrowAsync(async () => await service.GetAllAsync());
        }

        [Test]
        public void GetByIdAsyncMustNotThrowErrorValidId()
        {
            var id = "3bd8f642-bbaf-4e55-8906-cb45c73e8357";

            var service = serviceProvider.GetService<IClientsService>();

            Assert.DoesNotThrowAsync(async () => await service.GetClientByUserId(id));
        }

        [Test]
        public void AddAsyncShouldNotThrowError()
        {
            var input = new Client()
            {
                Id = new Guid("1db78f3d-ada4-4ea2-8916-a40f9f72cd60"),
                UserId = "1bd8f642-bbaf-4e55-8906-cb45c73e8357",
                FullName = "Client testtest"
            };

            var service = serviceProvider.GetService<IClientsService>();

            Assert.DoesNotThrowAsync(async () => await service.AddAsync(input));
        }

        [Test]
        public void FindClientByIdMustThrowErrorInvalidId()
        {
            var id = new Guid("00969b29-91b6-4ba1-ba3e-d78f463fee42");

            var service = serviceProvider.GetService<IClientsService>();

            Assert.That(async () => await service.FindClientById(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This client does not exist!"));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var user = new IdentityUser()
            {
                Id = "3bd8f642-bbaf-4e55-8906-cb45c73e8357",
                UserName = "client1",
                Email = "client1@physiocenter.com"
            };

            var user2 = new IdentityUser()
            {
                Id = "1bd8f642-bbaf-4e55-8906-cb45c73e8357",
                UserName = "client1",
                Email = "client1@physiocenter.com"
            };

            var client = new Client()
            {
                Id = new Guid("2db78f3d-ada4-4ea2-8916-a40f9f72cd60"),
                UserId = "3bd8f642-bbaf-4e55-8906-cb45c73e8357",
                FullName = "Client testtest"
            };

            await repo.AddAsync(user);
            await repo.AddAsync(user2);
            await repo.AddAsync(client);

            await repo.SaveChangesAsync();
        }
    }
}