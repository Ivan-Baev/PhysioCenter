namespace PhysioCenter.Mappings
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Notes;

    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteViewModel>().ReverseMap();
        }
    }
}