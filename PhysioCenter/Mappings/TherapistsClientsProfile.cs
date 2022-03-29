namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;

    public class TherapistsClientsProfile : Profile
    {
        public TherapistsClientsProfile()
        {
            CreateMap<Appointment, TherapistClient>();
        }
    }
}