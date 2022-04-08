namespace PhysioCenter.Models
{
    public class PagingViewModel
    {
        public int CurrentPage { get; set; }

        public bool HasPreviousPage => this.CurrentPage > 1;

        public int PreviousPageNumber => this.CurrentPage - 1;

        public bool HasNextPage => this.CurrentPage < this.PagesCount;

        public int NextPageNumber => this.CurrentPage + 1;

        public int ItemCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.ItemCount / this.ItemsPerPage);
    }
}