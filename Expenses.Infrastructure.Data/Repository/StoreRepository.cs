using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Infrastructure.Data.Repository
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public StoreRepository (ExpensesContext context)
            :base (context)
        {
            //if (FakeDB.stores.Count == 0)
            //{
            //    var store = new Store ()
            //    {
            //        StoreId = FakeDB.idStore++,
            //        Name = "Alcampo",
            //        Logo = "Logo de alcampo"
            //    };

            //    var store2 = new Store ()
            //    {
            //        StoreId = FakeDB.idStore++,
            //        Name = "Lidl",
            //        Logo = "Logo del Lidl"
            //    };

            //    FakeDB.stores.Add(store);
            //    FakeDB.stores.Add(store);
            //}
        }

        public async Task<IEnumerable<Store>> GetAllAsync ()
        {
            return await _context.Store.ToListAsync();
        }

        //public IQueryable<Store> GetAll()
        //{
            //return FakeDB.stores;
            //return _context.Store;    
        //}

        //public Store GetById(int id)
        //{
          //  foreach (Store s in FakeDB.stores)
            //{
              //  if (s.StoreId == id)
                //{
                  //  return s;
                //}
            //}
            //return null;
        //}

        //public Store Insert(Store store)
        //{
          //  store.StoreId = FakeDB.idStore++;
          //  FakeDB.stores.Add(store);
          //  return store;
        //}

        //public Store UpdateStore(Store storeUpdate)
        //{
          //  Store store = GetById(storeUpdate.StoreId);
          //  store.Name = storeUpdate.Name;
          //  store.Logo = storeUpdate.Logo;
          //  return store;
        //}

        //public Store Delete(int id)
        //{
          //  Store s = GetById(id);
          //  if (s == null)
          //  {
          //      return null;
          //  }
          //  FakeDB.stores.Remove(s);
          //  return s;
        //}
    }
}
