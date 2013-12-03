using System.Configuration;
using System.Data.OleDb;
using System.IO;
using AccessLog;
using AccessLog.Repository;

using Microsoft.AspNet.SignalR;
using Microsoft.Owin;

using Owin;

using YAccess;

[assembly: OwinStartup(typeof(Startup))]

namespace YAccess
{
    public class Startup
    {
        private readonly OleDbConnection connection;

        private readonly TransactionRepository transactionRepository;

        private readonly string dbFile;

        public Startup()
        {
            this.dbFile = Path.GetFullPath(ConfigurationManager.AppSettings["DbFile"]);

            var connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Jet OLEDB:Database Password=XsControl", this.dbFile);
            this.connection = new OleDbConnection(connectionString);
            this.transactionRepository = new TransactionRepository(this.connection);
        }

        public void Configuration(IAppBuilder app)
        {
            var transactionWatcher = new TransactionWatcher(this.dbFile, this.transactionRepository);
            GlobalHost.DependencyResolver.Register(typeof(AccessHub), () => new AccessHub(transactionWatcher));

            app.MapSignalR();
        }
    }
}