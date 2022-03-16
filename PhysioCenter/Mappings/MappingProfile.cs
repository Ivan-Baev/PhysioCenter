namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Appointments;
    using PhysioCenter.Models.SelectLists;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentViewModel>().ReverseMap();

            CreateMap<Client, ClientSelectListViewModel>().ReverseMap();
            CreateMap<Therapist, TherapistSelectListViewModel>().ReverseMap();
            CreateMap<Service, ServiceSelectListViewModel>().ReverseMap();
        }
    }
}