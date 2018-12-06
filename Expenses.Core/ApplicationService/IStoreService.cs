using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.ApplicationService
{
    public interface IStoreService
    {
        //POST
        Store NewStore(string name, string logo);

        Store SaveStore(Store store);
        
        //GET
        Store FindStoreById(int id);

        List<Store> GetAllStores();
        //Los filtros en la lista los hacemos en el servicio
        List<Store> GetAllStoresByName(string name);

        //PUT
        Store UpdateStore(Store storeUpdate);

        //DELETE
        Store DeleteStore(int id);

    }
}
