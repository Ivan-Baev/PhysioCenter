namespace PhysioCenter.Test.Services
{
    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Core.Services;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BlogsServiceTest
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
                .AddSingleton<IBlogsService, BlogsService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public void GetByIdAsyncMustThrowErrorInvalidId()
        {
            var id = new Guid("4864b85a-1ea4-4f94-982b-576076390cf5");

            var service = serviceProvider.GetService<IBlogsService>();

            Assert.That(async () => await service.GetByIdAsync(id),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This blog does not exist"));
        }

        [Test]
        public void GetByIdAsyncMustReturnBlog()
        {
            var id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");

            var service = serviceProvider.GetService<IBlogsService>();

            Assert.DoesNotThrowAsync(async () => await service.GetByIdAsync(id));
        }

        [Test]
        public void GetAllAsyncShouldNotThrowErrors()
        {
            var service = serviceProvider.GetService<IBlogsService>();

            Assert.DoesNotThrowAsync(async () => await service.GetAllAsync());
        }

        [Test]
        public void AddAsyncShouldThrowErrorSameTitle()
        {
            var input = new Blog()
            {
                Id = new Guid("3da35b2a-ce29-4be7-93b7-de9bae195ee9"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Title = "George",
            };

            var service = serviceProvider.GetService<IBlogsService>();
            Assert.That(async () => await service.AddAsync(input),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Blog title already exists"));
        }

        [Test]
        public void AddAsyncShouldNotThrowErrorDifferentTitle()
        {
            var input = new Blog()
            {
                Id = new Guid("3da35b2a-ce29-4be7-93b7-de9bae195ee9"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Title = "George2",
            };

            var service = serviceProvider.GetService<IBlogsService>();

            Assert.DoesNotThrowAsync(async () => await service.AddAsync(input));
        }

        [Test]
        public void UpdateAsyncShouldThrowErrorInvalidId()
        {
            var input = new Blog()
            {
                Id = new Guid("4da35b2a-ce29-4be7-93b7-de9bae195ee9"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Title = "George",
            };

            var service = serviceProvider.GetService<IBlogsService>();

            Assert.That(async () => await service.UpdateDetailsAsync(input),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This blog does not exist"));
        }

        [Test]
        public void UpdateAsyncShouldThrowErrorSameTitle()
        {
            var input = new Blog()
            {
                Id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Title = "George1",
            };

            var service = serviceProvider.GetService<IBlogsService>();

            Assert.That(async () => await service.UpdateDetailsAsync(input),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Blog title already exists"));
        }

        [Test]
        public void UpdateAsyncShouldNotThrowErrorValidBlog()
        {
            var input = new Blog()
            {
                Id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Content = "Lorem Ipsum 11is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Title = "George33",
            };

            var service = serviceProvider.GetService<IBlogsService>();

            Assert.DoesNotThrowAsync(async () => await service.UpdateDetailsAsync(input));
        }

        [Test]
        public void DeleteAsyncShouldThrowErrorInvalidId()
        {
            var id = new Guid("33a35b2a-ce29-4be7-93b7-de9bae195ee9");

            var service = serviceProvider.GetService<IBlogsService>();

            Assert.That(async () => await service.DeleteAsync(id),
               Throws.TypeOf<ArgumentException>().With.Message.EqualTo("This blog does not exist"));
        }

        [Test]
        public void DeleteAsyncShouldNotThrowErrorValidBlog()
        {
            var id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32");

            var service = serviceProvider.GetService<IBlogsService>();

            Assert.DoesNotThrowAsync(async () => await service.DeleteAsync(id));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var blogs = new List<Blog>()
            {
                new Blog()
                {
                Id = new Guid("11969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Title = "George",
                },
                new Blog()
                {
                Id = new Guid("21969b29-91b6-4ba1-ba3e-d78f463fee32"),
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1648308468/PhysioCenter%20Images/djco4v9bda6tvpcge4bs.png",
                Title = "George1",
                },
            };

            await repo.AddRangeAsync(blogs);
            await repo.SaveChangesAsync();
        }
    }
}