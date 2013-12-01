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
            var when = new DateTime(2013, 11, 29, 12, 00, 00);
            var transactions = this.transactionRepository.GetLastTransactions(when).ToList();

            Assert.IsNotEmpty(transactions);
            Assert.That(transactions, Has.Count.EqualTo(3));

            foreach (var hour in Enumerable.Range(0, 3))
            {
                Assert.That(transactions.Any(x => x.CreatedAt == when.AddHours(hour * -1)), Is.True, string.Format("{0}", when));
                Assert.That(transactions.Any(x => x.UpdatedAt == when.AddHours(hour * -1)), Is.True, string.Format("{0}", when));
            }
        }

        [TearDown]
        public void TearDown()
        {
            this.connection.Close();
        }
    }
}
