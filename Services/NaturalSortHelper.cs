using System.Linq;
using System.Text.RegularExpressions;
using TodoList.Models;

namespace TodoList.Services
{
    public static class NaturalSortHelper
    {
        public static IOrderedQueryable<TodoItem> OrderByNatural(IQueryable<TodoItem> query)
        {
            return query.OrderBy(item => PadNumbers(item.Title));
        }

        public static IOrderedQueryable<TodoItem> OrderByDescendingNatural(IQueryable<TodoItem> query)
        {
            return query.OrderByDescending(item => PadNumbers(item.Title));
        }

        private static string PadNumbers(string input)
        {
            return Regex.Replace(input ?? "", @"\d+", match => match.Value.PadLeft(10, '0'));
        }
    }
}