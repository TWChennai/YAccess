using System.Data;
using System.Linq;

using AccessLog.Domain;

using Dapper;

namespace AccessLog.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private const string TransactionQuery = "select CID as ControllerId, "
                                                + "GtNo as GateNumber, "
                                                + "EmpID as EmployeeId, "
                                                + "CardID as CardId, " 
                                                + "Dt as CreatedAt, " 
                                                + "updatedon as UpdatedAt "
                                                + "from Trans where GtNo = @GateNumber";

        private readonly IDbConnection connection;

        public TransactionRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public Transaction GetLastTransaction(string gateNumber)
        {
            return this.connection.Query<Transaction>(TransactionQuery, new { GateNumber = gateNumber }).FirstOrDefault();
        }
    }
}
