using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Expenses.Infrastructure.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Infrastructure.Data.Repository
{
    public class ProductDetailsRepository : IProductDetailsRepository
    {
        private readonly ExpensesContext _context;

        public ProductDetailsRepository (ExpensesContext context)
        {
            _context = context;
        }

        public ProductDetailsRepository ()
        {        }
        
        public ProductDetails Insert(ProductDetails productBrand)
        {
            //Al adjuntarlo al contexto e indicarle que su estado es Added, entiende que hay que insertar la nueva entidad
            _context.Attach(productBrand).State = EntityState.Added;
            _context.SaveChanges();
            return productBrand;
        }

        public ProductDetails Update(ProductDetails productBrandUpdate)
        {
            _context.Attach(productBrandUpdate).State = EntityState.Modified;
            //Si el productBrand tiene una referencia a Product, lo marcamos también como modificado para que tenga en cuenta los cambios
            _context.Entry(productBrandUpdate).Reference(p => p.Product).IsModified = true;
            _context.SaveChanges();
            return productBrandUpdate;
        }

        public ProductDetails Delete(int id)
        {
            var productBrandDeleted = _context.Remove(new ProductDetails { Id = id }).Entity;
            _context.SaveChanges();
            return productBrandDeleted;
        }

        /// <summary>
        /// Obtiene todos los formatos asociados con la marca indicada en el id
        /// </summary>
        /// <param name="idBrand">Id de la marca de la que se buscarán relaciones</param>
        /// <returns>Lista de los formatos asociados a esa marca</returns>
        public IEnumerable<Format> GetFormatsByBrand(int idBrand)
        {
            var productDetails = _context.ProductDetails
                .Include(pd => pd.Format)
                .Where(pd => pd.Brand.Id == idBrand && pd.Format != null).AsNoTracking().ToList();

            return productDetails.Select(p => p.Format)?.DistinctBy(f => f.Id)?.ToList();
        }

        public async Task<ProductDetails> GetByDataAsync(int idProduct, int? idBrand, int? idFormat)
        {
            var productDetails = await _context.ProductDetails.Where(pd => pd.ProductId == idProduct
            && pd.BrandId == idBrand && pd.FormatId == idFormat).FirstOrDefaultAsync();
            
            return productDetails;
        }
    }
}
