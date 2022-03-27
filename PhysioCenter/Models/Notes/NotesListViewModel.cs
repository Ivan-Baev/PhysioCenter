namespace PhysioCenter.Models.Notes
{
    using System.Collections.Generic;

    public class NotesListViewModel
    {
        public IEnumerable<NoteViewModel> Notes { get; set; }
    }
}