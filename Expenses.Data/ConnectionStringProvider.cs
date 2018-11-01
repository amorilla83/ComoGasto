using Domain;
using System;

namespace Expenses.Data
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string connectionString = string.Empty;

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
    }
}
