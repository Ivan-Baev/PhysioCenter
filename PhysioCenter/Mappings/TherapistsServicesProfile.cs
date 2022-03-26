namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Services;
    using PhysioCenter.Models.TherapistsServices;

    public class TherapistsServicesProfile : Profile
    {
        public TherapistsServicesProfile()
        {
            CreateMap<TherapistService, TherapistServiceViewModel>()
                .ReverseMap();
        }
    }
}