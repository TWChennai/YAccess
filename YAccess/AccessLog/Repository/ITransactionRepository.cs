using AccessLog.Domain;

namespace AccessLog.Repository
{
    public interface ITransactionRepository
    {
        Transaction GetLastTransaction(string gateNumber);
    }
}