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

    public class NotesServiceTest
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
                .AddSingleton<INotesService, NotesService>()
                .AddSingleton<IClientsService, ClientsService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();

            await SeedDbAsync(repo);
        }

        [Test]
        public void GetByIdAsyncMustThrowErrorInvalidId()
        {
            var id = new Guid("00969b29-91b6-4ba1-ba3e-d78f463fee42");

            var service = serviceProvider.GetService<INotesService>();

            Assert.That(async () => await service.GetByIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This note does not exist"));
        }

        [Test]
        public void GetByIdAsyncMustNotThrowErrorValidId()
        {
            var id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");

            var service = serviceProvider.GetService<INotesService>();

            Assert.DoesNotThrowAsync(async () => await service.GetByIdAsync(id));
        }

        [Test]
        public void GetAllByClientIdAsyncShouldThrowError()
        {
            var id = new Guid("00078f3d-ada4-4ea2-8916-a40f9f72cd60");
            var service = serviceProvider.GetService<INotesService>();
            Assert.That(async () => await service.GetAllByClientIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This client does not exist!"));
        }

        [Test]
        public void GetAllByClientIdAsyncShouldNotThrowError()
        {
            var id = new Guid("2db78f3d-ada4-4ea2-8916-a40f9f72cd60");
            var service = serviceProvider.GetService<INotesService>();

            Assert.DoesNotThrowAsync(async () => await service.GetAllByClientIdAsync(id));
        }

        [Test]
        public void AddAsyncShouldNotThrowError()
        {
            var input = new Note()
            {
                Id = new Guid("21969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ClientId = new Guid("2db78f3d-ada4-4ea2-8916-a40f9f72cd60"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9")
            };

            var service = serviceProvider.GetService<INotesService>();

            Assert.DoesNotThrowAsync(async () => await service.AddAsync(input));
        }

        [Test]
        public void UpdateDetailsAsyncShouldThrowErrorInvalidId()
        {
            var input = new Note()
            {
                Id = new Guid("00969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
            };

            var service = serviceProvider.GetService<INotesService>();

            Assert.That(async () => await service.UpdateDetailsAsync(input),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This note does not exist"));
        }

        [Test]
        public void UpdateAsyncShouldNotThrowErrorValidNote()
        {
            var input = new Note()
            {
                Id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Content = "Update test",
            };

            var service = serviceProvider.GetService<INotesService>();

            Assert.DoesNotThrowAsync(async () => await service.UpdateDetailsAsync(input));
        }

        [Test]
        public void DeleteAsyncShouldThrowErrorInvalidId()
        {
            var id = new Guid("33a35b2a-ce29-4be7-93b7-de9bae195ee9");

            var service = serviceProvider.GetService<INotesService>();

            Assert.That(async () => await service.DeleteAsync(id),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This note does not exist"));
        }

        [Test]
        public void DeleteAsyncShouldNotThrowErrorValidBlog()
        {
            var id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");

            var service = serviceProvider.GetService<INotesService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteAsync(id));
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

            await repo.AddRangeAsync(users);
            await repo.AddAsync(therapist);
            await repo.AddAsync(client);

            var note = new Note()
            {
                Id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ClientId = new Guid("2db78f3d-ada4-4ea2-8916-a40f9f72cd60"),
                TherapistId = new Guid("304ff139-de00-4ed1-b6e5-7cbe0a19dfc9")
            };
            await repo.AddAsync(note);
            await repo.SaveChangesAsync();
        }
    }
}