using System.Data.OleDb;

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

        public Startup()
        {
            this.connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\test.mdb;Jet OLEDB:Database Password=XsControl");
            this.transactionRepository = new TransactionRepository(this.connection);
        }

        public void Configuration(IAppBuilder app)
        {
            var transactionWatcher = new TransactionWatcher(@"C:\", this.transactionRepository);
            GlobalHost.DependencyResolver.Register(typeof(ChatHub), () => new ChatHub(transactionWatcher));

            app.MapSignalR();
        }
    }
}