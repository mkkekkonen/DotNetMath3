namespace DotNetMath3.API.Models
{
    public class Page
    {
        public int PageId { get; set; }

        public string TitleEn { get; set; }

        public string MarkdownContentEn { get; set; }

        public string Slug { get; set; }

        public Category ParentCategory { get; set; }
    }
}