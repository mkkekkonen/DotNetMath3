using DotNetMath3.Shared.Models;

namespace DotNetMath3.UI.State
{
    public class CategoryStateContainer
    {
        private List<Category> categories;

        public List<Category> Categories
        {
            get => categories;
            set
            {
                categories = value;
                NotifyCategoryDataChanged();
            }
        }

        public event Action CategoryDataChanged;

        private void NotifyCategoryDataChanged() => CategoryDataChanged?.Invoke();
    }
}