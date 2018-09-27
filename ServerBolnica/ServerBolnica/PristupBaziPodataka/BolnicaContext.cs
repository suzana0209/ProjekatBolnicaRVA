using Common.Dodatno;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBolnica.PristupBaziPodataka
{
    public class BolnicaContext : DbContext
    {
        public BolnicaContext() : base("dbConnection2015")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BolnicaContext, Konfiguracija>());
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 10 * 60;
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Bolnica> Bolnice { get; set; }
        public DbSet<Ljekar> Ljekari { get; set; }
        public DbSet<Pacijent> Pacijenti { get; set; }

    }
}
