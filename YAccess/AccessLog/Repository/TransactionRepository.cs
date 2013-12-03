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
                                                + "from Trans where Dt <= @accessDateTime";

        private readonly IDbConnection connection;

        public TransactionRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<Transaction> GetLastTransactions(DateTime when)
        {
            var accessDateTime = new DateTime(when.Year, when.Month, when.Day, when.Hour, when.Minute, when.Second);
            return this.connection.Query<Transaction>(TransactionQuery, new { accessDateTime });
        }
    }
}
