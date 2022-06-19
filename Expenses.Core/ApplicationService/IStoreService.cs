using Expenses.Core.Entities;
using Expenses.Core.Entities.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Core.ApplicationService
{
    public interface IStoreService
    {
        //POST
        //Store NewStore(string name, string logo);

        Task<StoreResponse> SaveStoreAsync(Store store);
        
        //GET
        Task<StoreResponse> FindStoreByIdAsync(int id);

        Task<IEnumerable<Store>> GetAllStoresAsync();
        ////Los filtros en la lista los hacemos en el servicio
        ////List<Store> GetAllStoresByName(string name);

        //PUT
        Task<StoreResponse> UpdateStoreAsync(int id, Store storeUpdate);
        
        //DELETE
        Task<StoreResponse> DeleteStoreAsync(int id);

    }
}
