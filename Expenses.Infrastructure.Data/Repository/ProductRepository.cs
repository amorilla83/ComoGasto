using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Infrastructure.Data.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ExpensesContext context)
            : base(context) { }


        public async Task<PaginatedEntity<Product>> GetAllAsync(int page, int itemsPerPage)
        {
            return  PaginatedEntity<Product>.ToPaginate(_context.Product.OrderBy(p => p.Name), page, itemsPerPage);
        }

        public async Task<Product> GetProductDetailsAsync (int id)
        {
            return await _context.Product
                .Where(p => p.Id == id)
                .Include(p => p.ProductDetails)
                .ThenInclude(pd => pd.Format )
                .Include(p => p.ProductDetails)
                .ThenInclude(p => p.Brand)
                .FirstOrDefaultAsync();
        }

        public IEnumerable<Product> GetDataProductReview()
        {
            //productDetails.SelectMany(pd => pd.ProductPurchaseList).Distinct().Count()
            //productDetails.Select(pd => pd.BrandId).Distinct().Count()
            //productDetails.Select(pd => pd.FormatId).Distinct().Count()
            //productDetails.SelectMany(pd => pd.ProductPurchaseList).OrderByDescending(pp => pp.Purchase.Date).FirstOrDefault()

            //            select* From product pr
            //left join ProductDetails pd on pr.id = pd.ProductId
            //left join ProductPurchase pp on pp.ProductDetailId = pd.id
            //left join Purchase p on p.id = pp.PurchaseId
            //var productDetails = _context.ProductDetails
            //    .Include(pd => pd.ProductPurchaseList)
            //    .Include(pd => pd.ProductPurchaseList).ThenInclude(pp => pp.Purchase)
            //    .Include(pd => pd.Product)
            //    .AsNoTracking();

            var product = _context.Product
                .Include(p => p.ProductDetails).DefaultIfEmpty()
                .Include(p => p.ProductDetails).ThenInclude(pd => pd.ProductPurchaseList).DefaultIfEmpty()
                .Include(p => p.ProductDetails).ThenInclude(pd => pd.ProductPurchaseList).ThenInclude(pp => pp.Purchase).DefaultIfEmpty()
                .OrderBy(p => p.Name)
                .AsNoTracking();

            return product.ToList();
        }

        //public IEnumerable<Product> GetAll()
        //{
        //    //return _context.Product;
        //    return FakeDB.products;
        //}

        //public Product GetById(int id)
        //{

        //    //Al hacer el select, ya no apuntamos a la misma posición de memoria del product que podríamos haber obtenido
        //    //previamente al obtenerlos todos. Ahora apunta a una posición de memoria en la que está él
        //    //Es el clon de Linq
        //    return FakeDB.products.
        //        Select(p => new Product()
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            Image = p.Image
        //        }).
        //        FirstOrDefault(p => p.Id == id);

        //    //var changeTracker = _context.ChangeTracker.Entries<Product>();
        //    //return _context.Product.FirstOrDefault(p => p.Id == id);
        //}

        //public Product GetByIdIncludeProductBrands (int id)
        //{
        //    //return _context.Product
        //    //  .Where (p => p.Id == id)
        //    //  .Include(p => p.ProductBrands)
        //    //  .FirstOrDefault();
        //    return null;
        //}

        //public Product Insert(Product product)
        //{
        //    /*
        //    product.Id = FakeDB.idProduct++;
        //    FakeDB.products.Add(product);
        //    return product;
        //    */
        //    /*
        //    var productInsert = _context.Add(product).Entity;
        //    _context.SaveChanges();
        //    return productInsert;*/
        //    _context.Attach(product).State = EntityState.Added;
        //    //SaveChanges to the UOW
        //    //_context.SaveChanges();
        //    return product;
        //}

        //public Product Update(Product productUpdate)
        //{
        //    /*
        //    Product p = GetById(productUpdate.Id);
        //    p.Name = productUpdate.Name;
        //    p.Detail = productUpdate.Detail;
        //    p.Image = productUpdate.Image;
        //    return p;
        //    */
        //    /*var productUpdated = _context.Update(productUpdate).Entity;
        //    _context.SaveChanges();
        //    return productUpdated;*/
        //    _context.Attach(productUpdate).State = EntityState.Modified;
        //    //SaveChanges to the UOW
        //    //_context.SaveChanges();
        //    return productUpdate;
        //}

        //public Product Delete(int id)
        //{

        //    Product p = GetById(id);
        //    if (p == null)
        //    {
        //        return null;
        //    }
        //    FakeDB.products.Remove(p);
        //    return p;

        //    // _context.Product.Remove()

        //    //Eliminamos los productBrands de este producto
        //    //Al poner el DeleteBehavior a SetNull, ya no necesitamos eliminarlos si no queremos
        //    //var productBrandsToRemove = _context.ProductBrand.Where(pb => pb.Product.Id == id);
        //    //_context.RemoveRange(productBrandsToRemove);
        //    //Eliminamos el producto
        //    //var productRemove = _context.Product.Remove(new Product { Id = id }).Entity;
        //    //SaveChanges to the UOW
        //    //_context.SaveChanges();
        //    //return productRemove;

        //}
    }
}
