using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Shared.ValueObjects
{
    public class PageParams
    {
        private int page;

        public PageParams(int page, int itemsPerPage = 100)
        {
            this.page = page < 1 ? 1 : page;
            ItemsPerPage = itemsPerPage;

            StartIndex = CalculateStartIndex();
        }

        public int ItemsPerPage { get; private set; }

        public int StartIndex { get; private set; }


        private int CalculateStartIndex()
        {
            return page * ItemsPerPage - ItemsPerPage;
        }
    }
}
