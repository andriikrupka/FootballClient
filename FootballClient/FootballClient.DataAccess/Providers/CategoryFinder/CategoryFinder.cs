using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClient.DataAccess.Providers.CategoryFinder
{
    public class CategoryFinder
    {
        public static List<CategoryFinder> CategoryFinderCollection = new List<CategoryFinder>();

        public string CategoryName { get; set; }

        public string PageId { get; set; }

        public override bool Equals(object obj)
        {
            var isEquals = false;
            var other = obj as CategoryFinder;
            isEquals = other?.PageId?.Equals(this.PageId) ?? false;

            return false;
        }

        public override int GetHashCode()
        {
            return this.PageId?.GetHashCode() ?? 0;
        }
    }
}
