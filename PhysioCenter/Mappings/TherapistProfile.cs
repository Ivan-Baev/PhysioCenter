namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Therapists;
    using PhysioCenter.Models.TherapistsServices;

    public class TherapistProfile : Profile
    {
        public TherapistProfile()
        {
            CreateMap<Therapist, TherapistViewModel>().ReverseMap();

            CreateMap<Therapist, TherapistServicesViewModel>().ReverseMap();

            CreateMap<Therapist, TherapistInputViewModel>()
               .ForSourceMember(x => x.ProfileImageUrl, opt => opt.DoNotValidate())
               .ReverseMap();

            CreateMap<Therapist, TherapistEditViewModel>()
                .ReverseMap();
        }
    }
}