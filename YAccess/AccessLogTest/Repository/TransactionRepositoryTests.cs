using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;

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
            this.connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=test.mdb;Jet OLEDB:Database Password=XsControl");
        }

        [SetUp]
        public void SetUp()
        {
            this.transactionRepository = new TransactionRepository(this.connection);
        }

        [Test]
        public void ShouldBeAbleToGetLastTransactionForAGivenGate()
        {
            var when = new DateTime(2013, 11, 29, 10, 00, 00);
            var whenStart = new DateTime(2013, 11, 29, 9, 58, 00);
            var transactions = this.transactionRepository.GetLastTransactions(when).ToList();

            Assert.IsNotEmpty(transactions);
            Assert.That(transactions, Has.Count.EqualTo(2));
            Assert.That(transactions.All(transaction => transaction.CreatedAt <= when), Is.True);
            Assert.That(transactions.All(transaction => transaction.CreatedAt >= whenStart), Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            this.connection.Close();
        }
    }
}
