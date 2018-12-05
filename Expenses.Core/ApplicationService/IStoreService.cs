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
        //Los filtros en la lista los hacemos en el servicio
        List<Store> GetAllStoresByName(string name);

        //Update
        Store UpdateStore(Store storeUpdate);

        //Delete
        Store DeleteStore(int id);

    }
}
