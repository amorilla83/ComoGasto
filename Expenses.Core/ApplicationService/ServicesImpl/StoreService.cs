using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public Store FindStoreById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Store> GetAllStores()
        {
            throw new NotImplementedException();
        }

        public Store NewStore(string name, string logo)
        {
            throw new NotImplementedException();
        }

        public Store SaveStore(Store store)
        {
            throw new NotImplementedException();
        }

        public Store UpdateStore(Store storeUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
