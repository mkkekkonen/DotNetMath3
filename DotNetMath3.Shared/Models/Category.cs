namespace DotNetMath3.Shared.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string NameEn { get; set; }

        public Category ParentCategory { get; set; }

        public List<Category> Subcategories { get; set; }

        public List<Page> Pages { get; set; }
    }
}