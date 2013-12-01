using System;
using System.Collections.Generic;
using System.Data;

using AccessLog.Domain;

using Dapper;

namespace AccessLog.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private const string TransactionQuery = "select CID as ControllerId, "
                                                + "GtNo as GateNumber, "
                                                + "EmpID as EmployeeId, "
                                                + "empName as EmployeeName, "
                                                + "CardID as CardId, "
                                                + "Dt as CreatedAt, "
                                                + "updatedon as UpdatedAt "
                                                + "from Trans where updatedon <= @when";

        private readonly IDbConnection connection;

        public TransactionRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<Transaction> GetLastTransactions(DateTime when)
        {
            return this.connection.Query<Transaction>(TransactionQuery, new { when });
        }
    }
}
