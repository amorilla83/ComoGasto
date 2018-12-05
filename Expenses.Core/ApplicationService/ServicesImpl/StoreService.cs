using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expenses.Core.DomainService;
using Expenses.Core.Entities;

namespace Expenses.Core.ApplicationService.ServicesImpl
{
    public class StoreService : IStoreService
    {
        readonly IStoreRepository _storeRepo;

        public StoreService (IStoreRepository storeRepostory)
        {
            _storeRepo = storeRepostory;
        }

        public Store DeleteStore(int id)
        {
            return _storeRepo.Delete(id);
        }

        public Store FindStoreById(int id)
        {
            return _storeRepo.GetById(id);
        }

        public List<Store> GetAllStores()
        {
            return _storeRepo.GetAll().ToList();
        }

        public List<Store> GetAllStoresByName (string name)
        {
            return _storeRepo.GetAll().Where(a => a.Name == name).ToList();
        }

        public Store NewStore(string name, string logo)
        {
            Store store = new Store()
            {
                Name = name,
                Logo = logo
            };
            return store;
        }

        public Store SaveStore(Store store)
        {
            return _storeRepo.Insert(store);
        }

        public Store UpdateStore(Store storeUpdate)
        {
            Store store = FindStoreById(storeUpdate.IdStore);
            store.Logo = storeUpdate.Logo;
            store.Name = storeUpdate.Name;
            return store;
        }
    }
}
