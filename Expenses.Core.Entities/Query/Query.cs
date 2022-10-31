
namespace Expenses.Core.Entities.Query
{
    public class QueryParams
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

        public QueryParams() { }

        public QueryParams(int page, int itemsPerPage)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;

            if (Page <= 0)
            {
                Page = 1;
            }

            if (ItemsPerPage <= 0)
            {
                ItemsPerPage = 10;
            }
        }
    }
}
