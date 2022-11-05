using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class ProductDetailsService : IProductDetailsService
    {
        private IProductDetailsRepository _productDetailsRepository;
        private IProductRepository _productRepository;

        public ProductDetailsService (IProductDetailsRepository productDetailsRepository,
            IProductRepository productRepository)
        {
            _productDetailsRepository = productDetailsRepository;
            _productRepository = productRepository;
        }

        public async Task<ProductDetails> GetProductDetailsByDataAsync (int productId, int? brandId, int? formatId)
        {
            return await _productDetailsRepository.GetByDataAsync(productId, brandId, formatId);
        }

        public List<Format> GetFormatsByBrand(int idBrand)
        {
            return _productDetailsRepository.GetFormatsByBrand(idBrand).OrderBy(f => f.Name).ToList();
        }
    }
}
