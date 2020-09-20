using Expenses.Core;

namespace Expenses.Infrastructure.Data
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
