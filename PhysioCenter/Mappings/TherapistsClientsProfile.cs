namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Therapists;

    public class TherapistsClientsProfile : Profile
    {
        public TherapistsClientsProfile()
        {
            CreateMap<Appointment, TherapistClient>();

            CreateMap<TherapistClient, TherapistCardViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Therapist.Id))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.Therapist.ProfileImageUrl))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Therapist.FullName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Therapist.Description));
            CreateMap<Therapist, TherapistClient>().ReverseMap();
        }
    }
}