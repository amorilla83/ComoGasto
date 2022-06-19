﻿using System;
using System.Threading.Tasks;
using Expenses.Core.Entities;

namespace Expenses.Core.DomainService
{
	public interface IProductPurchaseRepository: IGenericRepository<ProductPurchase>
	{
		Task<ProductPurchase> GetByIdAsync(int id);
	}
}

