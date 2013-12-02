using System;
using System.IO;

using AccessLog.Repository;

namespace AccessLog
{
    public class TransactionWatcher : ITransactionWatcher
    {
        private readonly ITransactionRepository transactionRepository;

        private readonly FileSystemWatcher accessFileWatcher;

        public TransactionWatcher(string path, ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
            this.accessFileWatcher = new FileSystemWatcher(path, "*.mdb") { EnableRaisingEvents = true };
            this.accessFileWatcher.Changed += this.OnChanged;
        }

        public event NewTransactions OnNewTransactions;

        private void OnChanged(object souruce, FileSystemEventArgs e)
        {
            if (this.OnNewTransactions != null)
            {
                var lastTransactions = this.transactionRepository.GetLastTransactions(DateTime.Now);
                this.OnNewTransactions(new TransactionEventArgs(lastTransactions));
            }
        }
    }
}