using System;

namespace Domain
{
    public interface IConnectionStringProvider
    {
        string ConnectionString { get; set; }
    }
}
