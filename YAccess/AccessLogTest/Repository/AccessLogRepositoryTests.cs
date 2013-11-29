using System.Data;
using System.Data.OleDb;

using AccessLog.Repository;

using NUnit.Framework;

namespace AccessLogTests.Repository
{
    [TestFixture]
    public class AccessLogRepositoryTests
    {
        private readonly IDbConnection connection;

        private AccessLogRepository accessLogRepository;

        public AccessLogRepositoryTests()
        {
            this.connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../test.mdb;Jet OLEDB:Database Password=XsControl;");
        }

        [SetUp]
        public void SetUp()
        {
            this.accessLogRepository = new AccessLogRepository(this.connection);
        }

        [Test]
        public void ShouldBeAbleToGetLastEmployee()
        {
            this.accessLogRepository.GetLastEmployee();
        }

        [TearDown]
        public void TearDown()
        {
            this.connection.Close();
        }
    }
}
