namespace PhysioCenter.Models.Blogs
{

    using System;

    public class BlogViewModel
    {
        public Guid Id;

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Content { get; set; }
    }
}