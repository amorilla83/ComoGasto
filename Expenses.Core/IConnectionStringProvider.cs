namespace Expenses.Core
{
    public interface IConnectionStringProvider
    {
        string ConnectionString { get; set; }
    }
}
