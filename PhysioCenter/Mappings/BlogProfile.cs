namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Blogs;

    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogViewModel>().ReverseMap();
        }
    }
}