using System.Collections.Generic;
using System.Threading.Tasks;
using Expenses.Core.Entities;

namespace Expenses.Core.DomainService
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        //Create Data
        //No Id when enter, Id when exist
        //Store Insert(Store store);
        //Read Data
        //Store GetById(int id);
        Task<IEnumerable<Store>> GetAllAsync();
        //Update Data
        //Store UpdateStore(Store storeUpdate);
        //Delete Data
        //Store Delete(int id);
    }
}
