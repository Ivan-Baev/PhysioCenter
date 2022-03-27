namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Therapists;
    using PhysioCenter.Models.TherapistsServices;

    public class TherapistsServicesProfile : Profile
    {
        public TherapistsServicesProfile()
        {
            CreateMap<TherapistService, TherapistEditViewModel>()
                .ReverseMap();

            CreateMap<TherapistService, TherapistServiceViewModel>()
                .ReverseMap();

            CreateMap<TherapistService, TherapistInputViewModel>()
               .ReverseMap();
        }
    }
}