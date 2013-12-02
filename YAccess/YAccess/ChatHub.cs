using AccessLog;

using Microsoft.AspNet.SignalR;

namespace YAccess
{
    public class ChatHub : Hub
    {
        private readonly ITransactionWatcher transactionWatcher;

        public ChatHub(ITransactionWatcher transactionWatcher)
        {
            this.transactionWatcher = transactionWatcher;
            this.transactionWatcher.OnNewTransactions += this.OnNewTransactions;
        }

        private void OnNewTransactions(TransactionEventArgs transactionEventArgs)
        {
            this.Clients.All.broadcastMessage("System", "File updated");
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            this.Clients.All.broadcastMessage(name, message);
        }
    }
}