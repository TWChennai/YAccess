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
            this.accessFileWatcher = GetFileSystemWatcher(path);
        }

        private FileSystemWatcher GetFileSystemWatcher(string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            
            if (directoryName == null)
                return null;

            var fileSystemWatcher = new FileSystemWatcher(directoryName, fileName) {EnableRaisingEvents = true};
            fileSystemWatcher.Changed += this.OnChanged;
            return fileSystemWatcher;
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