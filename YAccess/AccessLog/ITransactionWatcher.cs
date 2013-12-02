namespace AccessLog
{
    public delegate void NewTransactions(TransactionEventArgs transactionEventArgs);

    public interface ITransactionWatcher
    {
        event NewTransactions OnNewTransactions;
    }
}