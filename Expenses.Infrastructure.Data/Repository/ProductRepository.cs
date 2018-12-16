using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Infrastructure.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ExpensesContext _context;

        public ProductRepository (ExpensesContext context)
        {
            _context = context;
        }

        public ProductRepository()
        {
            /*if (FakeDB.products.Count == 0)
            {
                var product = new Product ()
                {
                    Id = FakeDB.idProduct++,
                    Name = "Mayonesa",
                    Detail = "Detalles",
                    Image = "Este sería el icono"
                };

                var product2 = new Product ()
                {
                    Id = FakeDB.idProduct++,
                    Name = "Pan de molde",
                    Detail = "Detalles pan de molde",
                    Image = "Icono pan de molde"
                };

                FakeDB.products.Add(product);
                FakeDB.products.Add(product2);
            }*/
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product;
            //return FakeDB.products;
        }

        public Product GetById(int id)
        {
            /*
            //Al hacer el select, ya no apuntamos a la misma posición de memoria del product que podríamos haber obtenido
            //previamente al obtenerlos todos. Ahora apunta a una posición de memoria en la que está él
            //Es el clon de Linq
            return FakeDB.products.
                Select(p => new Product()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Image = p.Image
                }).
                FirstOrDefault(p => p.Id == id);
                */
            var changeTracker = _context.ChangeTracker.Entries<Product>();
            return _context.Product.FirstOrDefault(p => p.Id == id);
        }

        public Product GetByIdIncludeProductBrands (int id)
        {
            return _context.Product
                .Where (p => p.Id == id)
                .Include(p => p.ProductBrands)
                .FirstOrDefault();
        }

        public Product Insert(Product product)
        {
            /*
            product.Id = FakeDB.idProduct++;
            FakeDB.products.Add(product);
            return product;
            */
            /*
            var productInsert = _context.Add(product).Entity;
            _context.SaveChanges();
            return productInsert;*/
            _context.Attach(product).State = EntityState.Added;
            //SaveChanges to the UOW
            //_context.SaveChanges();
            return product;
        }

        public Product Update(Product productUpdate)
        {
            /*
            Product p = GetById(productUpdate.Id);
            p.Name = productUpdate.Name;
            p.Detail = productUpdate.Detail;
            p.Image = productUpdate.Image;
            return p;
            */
            /*var productUpdated = _context.Update(productUpdate).Entity;
            _context.SaveChanges();
            return productUpdated;*/
            _context.Attach(productUpdate).State = EntityState.Modified;
            //SaveChanges to the UOW
            //_context.SaveChanges();
            return productUpdate;
        }

        public Product Delete(int id)
        {
            /*
            Product p = GetById(id);
            if (p == null)
            {
                return null;
            }
            FakeDB.products.Remove(p);
            return p;
            */
            // _context.Product.Remove()

            //Eliminamos los productBrands de este producto
            //Al poner el DeleteBehavior a SetNull, ya no necesitamos eliminarlos si no queremos
            //var productBrandsToRemove = _context.ProductBrand.Where(pb => pb.Product.Id == id);
            //_context.RemoveRange(productBrandsToRemove);
            //Eliminamos el producto
            var productRemove = _context.Product.Remove(new Product { Id = id }).Entity;
            //SaveChanges to the UOW
            //_context.SaveChanges();
            return productRemove;

        }
    }
}
