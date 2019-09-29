using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Shared.ValueObjects
{
    public class PageResult<T> where T : class
    {
        public PageResult(IEnumerable<T> data, int totalDataCount)
        {
            Data = data;
            TotalDataCount = totalDataCount;
        }

        public IEnumerable<T> Data { get; private set; }

        public int TotalDataCount { get; private set; }
    }
}
