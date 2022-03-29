namespace PhysioCenter.Models.Clients
{
    using System.Collections.Generic;

    public class ClientsListViewModel
    {
        public IEnumerable<ClientViewModel> Clients { get; set; }
    }
}