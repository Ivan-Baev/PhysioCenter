namespace PhysioCenter.Models
{
    public class PagingViewModel
    {
        private int pagesCount;
        public int CurrentPage { get; set; }

        public bool HasPreviousPage => this.CurrentPage > 1;

        public int PreviousPageNumber => this.CurrentPage - 1;

        public bool HasNextPage => this.CurrentPage < PagesCount;

        public int NextPageNumber => this.CurrentPage + 1;

        public int ItemCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PagesCount
        {
            get
            {
                if (ItemCount > 0)
                {
                    pagesCount = (int)Math.Ceiling((double)ItemCount / ItemsPerPage);
                }
                else
                {
                    pagesCount = 1;
                }

                return pagesCount;
            }
        }
    }
}