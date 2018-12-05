using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.DomainService
{
    public interface IStoreRepository
    {
        //Create Data
        //No Id when enter, Id when exist
        Store Insert(Store store);
        //Read Data
        Store GetById(int id);
        IEnumerable<Store> GetAll();
        //Update Data
        Store Update(Store storeUpdate);
        //Delete Data
        Store Delete(int id);
    }
}
