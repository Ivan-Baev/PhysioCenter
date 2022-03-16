namespace PhysioCenter.Core.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ClientInputViewModel
    {
        [Required]
        public string FullName { get; set; }
    }
}