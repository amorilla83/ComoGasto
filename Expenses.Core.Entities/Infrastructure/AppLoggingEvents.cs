using System;
namespace Expenses.Core.Entities.Infrastructure
{
    public static class AppLoggingEvents
    {
        //TODO: Debería estar en el proyecto de loggging
        public const int Create = 1000;
        public const int Read = 1001;
        public const int Update = 1002;
        public const int Delete = 1003;

        public const int Error = 3000;
        public const int RecordNotFound = 4000;
    }
}
