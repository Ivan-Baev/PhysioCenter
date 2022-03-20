namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models;
    using PhysioCenter.Models.Appointments;
    using PhysioCenter.Models.Therapists;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentViewModel>().ReverseMap();
            CreateMap<Appointment, AppointmentInputViewModel>().ReverseMap();

            CreateMap<Therapist, TherapistViewModel>().ReverseMap();

        }
    }
}