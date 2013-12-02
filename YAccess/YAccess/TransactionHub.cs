using System.Data.OleDb;

using AccessLog;
using AccessLog.Repository;

using Microsoft.AspNet.SignalR;

namespace YAccess
{
    public class TransactionHub : Hub
    {
        private readonly TransactionWatcher transactionWatcher;

        public TransactionHub()
        {
            var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\test.mdb;Jet OLEDB:Database Password=XsControl");
            this.transactionWatcher = new TransactionWatcher(@"C:\", new TransactionRepository(connection));
            this.transactionWatcher.OnNewTransactions += this.OnNewTransactions;
        }

        private void OnNewTransactions(TransactionEventArgs transactionEventArgs)
        {
            Clients.All.hello();
        }
    }
}