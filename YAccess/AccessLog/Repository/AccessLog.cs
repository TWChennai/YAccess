using System.Data;
using System.Linq;

using AccessLog.Domain;

using Dapper;

namespace AccessLog.Repository
{
    public class AccessLog
    {
        private readonly IDbConnection connection;

        public AccessLog(IDbConnection connection)
        {
            this.connection = connection;
        }

        public Employee GetLastEmployee()
        {
            return this.connection.Query<Employee>("select * from emp").FirstOrDefault();
        }
    }
}
