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

            CreateMap<Blog, BlogInputViewModel>()
                .ForSourceMember(x => x.ImageUrl, opt => opt.DoNotValidate())
                .ReverseMap();

            CreateMap<Blog, BlogEditViewModel>()
                .ReverseMap();
        }
    }
}