using Expenses.Core.DomainService;
using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Infrastructure.Data.Repository
{
    public class StoreRepository : IStoreRepository
    {
        public StoreRepository ()
        {
            if (FakeDB.stores.Count == 0)
            {
                var store = new Store ()
                {
                    IdStore = FakeDB.idStore++,
                    Name = "Alcampo",
                    Logo = "Logo de alcampo"
                };

                var store2 = new Store ()
                {
                    IdStore = FakeDB.idStore++,
                    Name = "Lidl",
                    Logo = "Logo del Lidl"
                };

                FakeDB.stores.Add(store);
                FakeDB.stores.Add(store);
            }
        }

        public IEnumerable<Store> GetAll()
        {
            return FakeDB.stores;
        }

        public Store GetById(int id)
        {
            foreach (Store s in FakeDB.stores)
            {
                if (s.IdStore == id)
                {
                    return s;
                }
            }
            return null;
        }

        public Store Insert(Store store)
        {
            store.IdStore = FakeDB.idStore++;
            FakeDB.stores.Add(store);
            return store;
        }

        public Store Update(Store storeUpdate)
        {
            Store store = GetById(storeUpdate.IdStore);
            store.Name = storeUpdate.Name;
            store.Logo = storeUpdate.Logo;
            return store;
        }

        public Store Delete(int id)
        {
            Store s = GetById(id);
            if (s == null)
            {
                return null;
            }
            FakeDB.stores.Remove(s);
            return s;
        }
    }
}
