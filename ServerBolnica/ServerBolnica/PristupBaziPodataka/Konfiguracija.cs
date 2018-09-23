using System.Data.Entity.Migrations;

namespace ServerBolnica.PristupBaziPodataka
{
    public class Konfiguracija : DbMigrationsConfiguration<BolnicaContext>
    {
        public Konfiguracija()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "BolnicaDBContext";
        }
    }
}
