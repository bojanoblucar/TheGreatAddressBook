using AddressBook.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Shared.Extensions
{
    public static class QueryablePageExtension
    {
        public static PageResult<T> GetPage<T>(this IQueryable<T> query, PageParams pageParams) where T : class
        {
            var data = query.Skip(pageParams.StartIndex)
                            .Take(pageParams.ItemsPerPage)
                            .ToList();

            int count = query.Count();

            return new PageResult<T>(data, count);
        }

        public static async Task<PageResult<T>> GetPageAsync<T>(this IQueryable<T> query, PageParams pageParams) where T : class
        {
            return await Task.Run(() =>
            {
                var data = query.Skip(pageParams.StartIndex).Take(pageParams.ItemsPerPage).ToList();
                var count = query.Count();

                return new PageResult<T>(data, count);
            });
        }
    }
}
