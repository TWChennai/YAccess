using System;
using System.IO;

using AccessLog.Repository;

namespace AccessLog
{
    public class TransactionWatcher
    {
        private readonly ITransactionRepository transactionRepository;

        private readonly FileSystemWatcher accessFileWatcher;

        public TransactionWatcher(string path, ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
            this.accessFileWatcher = new FileSystemWatcher(path, "*.*") { EnableRaisingEvents = true };
            this.accessFileWatcher.Changed += this.OnChanged;
        }

        public delegate void NewTransactions(TransactionEventArgs transactionEventArgs);

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