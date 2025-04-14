using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TodoList.Models;

namespace TodoList.Services
{
    public static class NaturalSortHelper
    {
        public static List<TodoItem> SortByNaturalOrder(IEnumerable<TodoItem> items)
        {
            return items.OrderBy(item => item.Title, new NaturalSortComparer()).ToList();
        }

        private class NaturalSortComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return -1;
                if (y == null) return 1;

                var regex = new Regex(@"(\d+)|(\D+)");
                var xParts = regex.Matches(x);
                var yParts = regex.Matches(y);

                for (int i = 0; i < Math.Min(xParts.Count, yParts.Count); i++)
                {
                    if (xParts[i].Value == yParts[i].Value) continue;

                    if (int.TryParse(xParts[i].Value, out int xNum) &&
                        int.TryParse(yParts[i].Value, out int yNum))
                    {
                        return xNum.CompareTo(yNum);
                    }
                    return string.Compare(xParts[i].Value, yParts[i].Value, StringComparison.OrdinalIgnoreCase);
                }

                return xParts.Count.CompareTo(yParts.Count);
            }
        }
    }
}