using System;
using System.Collections.Generic;

using AccessLog.Domain;

namespace AccessLog.Repository
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetLastTransactions(DateTime when);
    }
}