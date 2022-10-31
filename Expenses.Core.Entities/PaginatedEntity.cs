using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Expenses.Core.Entities
{
	public class PaginatedEntity<T>
	{
		public List<T> Items { get; set; }
		public int ItemsPerPage { get; set; }
		public int CountItems { get; set; }
		public int CountPage { get; set; }
		public int PageNumber { get; set; }

		public PaginatedEntity()
		{		}

		public PaginatedEntity(IEnumerable<T> items, int count, int pageNumber, int itemsPerPage)
		{
			PageNumber = pageNumber;
			ItemsPerPage = itemsPerPage;
			CountPage = (int)Math.Ceiling(count / (double)itemsPerPage);
			CountItems = count;
			Items = items.ToList();
		}

		public static PaginatedEntity<T> ToPaginate (IQueryable<T> items, int pageNumber, int itemsPerPage)
		{
			int count = items.Count();
			if (pageNumber == 0)
			{
				pageNumber = 1;
			}

			if (itemsPerPage == 0)
			{
				itemsPerPage = count;
			}

			IEnumerable<T> chunk = items.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage);
			return new PaginatedEntity<T>(chunk, count, pageNumber, itemsPerPage);
		}
	}
}

