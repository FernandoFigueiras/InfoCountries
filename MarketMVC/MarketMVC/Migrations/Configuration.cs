namespace MarketMVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MarketMVC.Data.MarketMVCContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //permitir que quando sejam feitas alteracoes aos tipos de dados haja perca de dados
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MarketMVC.Data.MarketMVCContext";
        }

        protected override void Seed(MarketMVC.Data.MarketMVCContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
