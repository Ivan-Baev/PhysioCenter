namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Categories;

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<Category, CategoryInputViewModel>()
                .ForSourceMember(x => x.ImageUrl, opt => opt.DoNotValidate())
                .ReverseMap();

            CreateMap<Category, CategoryEditViewModel>()
                .ReverseMap();
        }
    }
}