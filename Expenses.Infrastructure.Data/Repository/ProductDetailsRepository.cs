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
    public class ProductDetailsRepository : IProductDetailsRepository
    {
        private readonly ExpensesContext _context;

        public ProductDetailsRepository (ExpensesContext context)
        {
            _context = context;
        }

        public ProductDetailsRepository ()
        {
            /*if (FakeDB.productBrands.Count == 0)
            {
                var pb = new ProductBrand()
                {
                    Id = FakeDB.idProductBrand++,
                    Name = "Calvé",
                    CurrentMoney = 1.45,
                    Size = 450,
                    Product = new Product () { Id = 1}
                };
                FakeDB.productBrands.Add(pb);

                var pb2 = new ProductBrand()
                {
                    Id = FakeDB.idProductBrand++,
                    Name = "Calvé",
                    CurrentMoney = 1.15,
                    Size = 250,
                    Product = new Product () { Id = 1}
                };

                FakeDB.productBrands.Add(pb2);
            }*/
        }
        
        public int Count()
        {
            //return _context.ProductBrand.Count();
            return 0;
        }

        public IEnumerable<ProductDetails> GetAll(Filter filter)
        {
            if (filter == null)
            {
                return FakeDB.productBrands;
                //return _context.ProductBrand;
            }
            //return _context.ProductBrand
            //  .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage) //-1 para que empiece en la posición 0
            //  .Take(filter.ItemsPerPage);
            return null;
        }

        public ProductDetails GetById(int id)
        {
            foreach (ProductDetails pb in FakeDB.productBrands)
             {
                 if (pb.Id == id)
                 {
                     return pb;
                 }
             }
             return null;
            //return _context.ProductBrand.Include(pb => pb.Product).FirstOrDefault(pb => pb.Id == id);
        }

        public ProductDetails Insert(ProductDetails productBrand)
        {

            /*productBrand.Id = FakeDB.idProductBrand++;
            FakeDB.productBrands.Add(productBrand);
            return productBrand;*/

            /*
             * Lo añadimos al contexto y salvamos los cambios
            //Con el changeTracker controlamos todos lo que tiene el contexto en memoria
            //Se puede especificar los datos que queremos obtener u obtenerlo todo
            //Muy util para depurar
            //var changeTracker = _context.ChangeTracker.Entries<Product>();

            //Con el attach cargamos el producto en el contexto, para que no piense que estamos intentando insertar uno nuevo
            //Del contexto obtenemos una Entry desde la que accedemos a la entity
            if (productBrand.Product != null && 
                _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(pe => pe.Entity.Id == productBrand.Product.Id) == null)
            {
                //No deberíamos añadirlo si ya está en el contexto
                _context.Attach(productBrand.Product);
            }
            var pbInsert = _context.Add(productBrand).Entity;
            _context.SaveChanges();
            return pbInsert;
            */

            //Al adjuntarlo al contexto e indicarle que su estado es Added, entiende que hay que insertar la nueva entidad
            _context.Attach(productBrand).State = EntityState.Added;
            _context.SaveChanges();
            return productBrand;
        }

        public ProductDetails Update(ProductDetails productBrandUpdate)
        {
            /*ProductBrand pb = GetById(productBrandUpdate.Id);
            pb.Name = productBrandUpdate.Name;
            pb.Size = productBrandUpdate.Size;
            pb.CurrentMoney = productBrandUpdate.CurrentMoney;
            pb.Product = productBrandUpdate.Product;
            return pb;*/

            /*
             * 
            if (productBrandUpdate.Product != null &&
                _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(pe => pe.Entity.Id == productBrandUpdate.Product.Id) == null)
            {
                _context.Attach(productBrandUpdate.Product);
            }
            else if (productBrandUpdate.Product == null)
            {
                //Si queremos eliminar el product del update, será null
                //Debemos eliminar la relación del ProductBrand con el product
                //Indicamos que la referencia ha sido modificada para que elimine de la lista de ProductBrand de product el modificado
                _context.Entry(productBrandUpdate).Reference(p => p.Product).IsModified = true;
            }
            var pbUpdated = _context.Update(productBrandUpdate).Entity;
            _context.SaveChanges();
            return pbUpdated;
            */
            _context.Attach(productBrandUpdate).State = EntityState.Modified;
            //Si el productBrand tiene una referencia a Product, lo marcamos también como modificado para que tenga en cuenta los cambios
            _context.Entry(productBrandUpdate).Reference(p => p.Product).IsModified = true;
            _context.SaveChanges();
            return productBrandUpdate;
        }

        public ProductDetails Delete(int id)
        {
            /* ProductBrand pb = GetById(id);
             if (pb == null)
             {
                 return null;
             }
             FakeDB.productBrands.Remove(pb);
             return pb;*/
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
            var productDetails = _context.ProductDetails.Where(pd => pd.Brand.Id == idBrand).AsNoTracking().ToList();

            return productDetails.Select(p => p.Format).Distinct().ToList();
        }

        public async Task<ProductDetails> GetByDataAsync(int idProduct, int? idBrand, int? idFormat)
        {
            var productDetails = await _context.ProductDetails.Where(pd => pd.ProductId == idProduct
            && pd.BrandId == idBrand && pd.FormatId == idFormat).FirstOrDefaultAsync();
            
            return productDetails;
        }
    }
}
