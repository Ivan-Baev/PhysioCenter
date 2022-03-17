namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models;
    using PhysioCenter.Models.Appointments;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentViewModel>().ReverseMap();
            CreateMap<Appointment, AppointmentInputViewModel>().ReverseMap();
        }
    }
}