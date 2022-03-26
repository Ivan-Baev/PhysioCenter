namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Services;

    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceViewModel>().ReverseMap();

            CreateMap<Service, ServiceInputViewModel>()
                .ForSourceMember(x => x.ImageUrl, opt => opt.DoNotValidate())
                .ReverseMap();

            CreateMap<Service, ServiceEditViewModel>()
                .ReverseMap();
        }
    }
}