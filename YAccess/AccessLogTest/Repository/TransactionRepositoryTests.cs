using System.Data;
using System.Data.OleDb;
using System.Globalization;

using AccessLog.Repository;

using NUnit.Framework;

namespace AccessLogTests.Repository
{
    [TestFixture]
    public class TransactionRepositoryTests
    {
        private readonly IDbConnection connection;

        private ITransactionRepository transactionRepository;

        public TransactionRepositoryTests()
        {
            this.connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../test.mdb;Jet OLEDB:Database Password=XsControl");
        }

        [SetUp]
        public void SetUp()
        {
            this.transactionRepository = new TransactionRepository(this.connection);
        }

        [Test]
        public void ShouldBeAbleToGetLastTransactionForAGivenGate()
        {
            var transaction = this.transactionRepository.GetLastTransaction("02");

            Assert.NotNull(transaction);
            Assert.That(transaction.ControllerId, Is.EqualTo("02"));
            Assert.That(transaction.GateNumber, Is.EqualTo("02"));
            Assert.That(transaction.CardId, Is.EqualTo("1"));
            Assert.That(transaction.EmployeeId, Is.EqualTo("1"));
            Assert.That(transaction.CreatedAt.ToString(CultureInfo.InvariantCulture), Is.EqualTo("11/29/2013 10:35:00"));
            Assert.That(transaction.UpdatedAt.ToString(CultureInfo.InvariantCulture), Is.EqualTo("11/29/2013 10:35:00"));
        }

        [TearDown]
        public void TearDown()
        {
            this.connection.Close();
        }
    }
}
