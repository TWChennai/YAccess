using System;
using System.Collections.Generic;
using System.Linq;

using AccessLog.Domain;

namespace AccessLogWatcher
{
    public class TransactionEventArgs : EventArgs
    {
        public TransactionEventArgs(IEnumerable<Transaction> transactions)
        {
            this.Transactions = transactions.ToList();
        }

        public IList<Transaction> Transactions { get; private set; }
    }
}