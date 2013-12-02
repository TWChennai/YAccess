using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;

using AccessLog;
using AccessLog.Domain;
using AccessLog.Repository;

using Dapper;

using Moq;

using NUnit.Framework;

namespace AccessLogTests
{
    [TestFixture]
    public class TransactionWatcherTests
    {
        private ITransactionWatcher transactionWatcher;

        private Mock<ITransactionRepository> transactionRepositoryMock;

        private IList<Transaction> transactions;

        private OleDbConnection connection;

        [SetUp]
        public void SetUp()
        {
            var path = Path.GetFullPath(".");
            this.transactionRepositoryMock = new Mock<ITransactionRepository>();
            this.transactionWatcher = new TransactionWatcher(path, this.transactionRepositoryMock.Object);
            this.transactionWatcher.OnNewTransactions += this.OnNewTransactions;
            this.connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=test.mdb;Jet OLEDB:Database Password=XsControl");
        }

        [Test]
        public void ShouldReturnTheNewTransaction()
        {
            var when = DateTime.Now;
            var transaction = new Transaction { CreatedAt = when, UpdatedAt = when, EmployeeId = "1", EmployeeName = "Tom" };
            var expectedTransaction = new List<Transaction> { transaction };
            this.transactionRepositoryMock.Setup(repository => repository.GetLastTransactions(It.IsAny<DateTime>())).Returns(expectedTransaction);

            this.connection.Execute("insert into trans(empID, empname) values(@employeeId, @employeeName)", transaction);

            Assert.NotNull(this.transactions);
            Assert.That(this.transactions, Is.Not.Null.After(100));
            Assert.That(this.transactions, Is.Not.Empty);
            Assert.That(this.transactions, Is.EqualTo(expectedTransaction));
        }

        private void OnNewTransactions(TransactionEventArgs transactionEventArgs)
        {
            this.transactions = transactionEventArgs.Transactions;
        }
    }
}
