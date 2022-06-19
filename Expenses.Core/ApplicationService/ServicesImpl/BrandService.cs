using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;
using Microsoft.Extensions.Caching.Memory;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class BrandService : IBrandService
    {
        private IUnitOfWork _unitOfWork;

        private readonly IProductService _productService;

        private readonly IBrandRepository _brandRepository;

        private readonly IMemoryCache _cache;

        public BrandService(IUnitOfWork unitOfWork,
            IProductService productService,
            IBrandRepository brandRepository,
            IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
            _productService = productService;
            _cache = cache;
        }

        public async Task<IEnumerable<Brand>> GetBrandsByProduct(int id)
        {
            //return await _brandRepository.GetBrandsByProduct(id);
            return null;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<BrandResponse> SaveBrandAsync(Brand addBrand)
        {
            try
            {
                Brand newBrand = new Brand();
                //Exists brand
                newBrand = await FindBrandByNameAsync(addBrand.Name);

                if (newBrand == null)
                {
                    newBrand = new Brand()
                    {
                        Name = addBrand.Name
                    };
                }

                //if (addBrand.ProductList.Any())
                //{
                //    if (newBrand.ProductList.Any(b => b.Id == addBrand.ProductList.First().Id))
                //    {
                //        throw new Exception($"El producto con id {addBrand.ProductList.First().Id} " +
                //            $"ya está asociado a la marca {newBrand.Name}");
                //    }

                //    Product newProduct = (await _productService.FindProductByIdAsync(addBrand.ProductList.First().Id))?.Resource;

                //    if (newProduct == null)
                //    {
                //        throw new Exception($"El producto con Id {addBrand.ProductList.First().Id} no existe en base de datos");
                //    }

                //    newBrand.ProductList.Add(newProduct);
                //}

                _brandRepository.Update(newBrand);

                await _unitOfWork.Commit();
                return new BrandResponse(newBrand);
            }
            catch (Exception ex)
            {
                return new BrandResponse($"An error occurred when saving a brand: {ex.Message}");
            }
        }

        public async Task<Brand> FindBrandByIdAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<Brand> FindBrandByNameAsync (string name)
        {
            Expression<Func<Brand, bool>> predicate = (Brand entity) => entity.Name == name;
            return await _brandRepository.GetBrandByNameAsync(predicate);
        }
    }
}
