using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using AccessLog;
using AccessLog.Domain;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace YAccess
{
    public class AccessHub : Hub
    {
        private readonly ITransactionWatcher transactionWatcher;

        public AccessHub(ITransactionWatcher transactionWatcher)
        {
            this.transactionWatcher = transactionWatcher;
            this.transactionWatcher.OnNewTransactions += this.OnNewTransactions;
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