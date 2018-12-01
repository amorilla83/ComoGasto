using Expenses.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.DomainService
{
    public interface ICustomerRepository
    {
        //Create Data
        //No Id when enter, Id when exist
        Customer Create(Customer customer);
        //Read Data
        Customer ReadById(int id);
        List<Customer> ReadAll();
        //Update Data
        Customer Update(Customer customerUpdate);
        //Delete Data
        Customer Delete(int id);
    }
}
