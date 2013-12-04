using System.Threading.Tasks;
using AccessLog;
using Microsoft.AspNet.SignalR;

namespace YAccess
{
    public class AccessHub : Hub
    {
        private static ITransactionWatcher TransactionWatcher;

        private readonly static object LockObject = new object();

        public AccessHub(ITransactionWatcher transactionWatcher)
        {
            lock (LockObject)
            {
                if (TransactionWatcher == null)
                {
                    TransactionWatcher = transactionWatcher;
                    TransactionWatcher.OnNewTransactions += this.OnNewTransactions;
                }
            }
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        private void OnNewTransactions(TransactionEventArgs transactionEventArgs)
        {
            foreach(var transaction in transactionEventArgs.Transactions)
            {
                var group = string.Format("{0}-{1}", transaction.ControllerId, transaction.GateNumber);
                this.Clients.Group(group).identifyUser(transaction.EmployeeId);
            }
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            this.Clients.All.broadcastMessage(name, message);
        }
    }
}