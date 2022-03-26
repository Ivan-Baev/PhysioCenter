namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Appointments;

    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentViewModel>().ReverseMap();
            CreateMap<Appointment, AppointmentInputViewModel>().ReverseMap();
        }
    }
}