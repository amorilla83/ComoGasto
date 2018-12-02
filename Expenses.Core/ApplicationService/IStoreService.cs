using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.ApplicationService
{
    public interface IStoreService
    {
        //Create
        Store NewStore(string name, string logo);

        Store SaveStore(Store store);
        
        //Read
        Store FindStoreById(int id);

        List<Store> GetAllStores();

        //Update
        Store UpdateStore(Store storeUpdate);

        //Delete
        Store DeleteStore(int id);

    }
}
