namespace RecepiesApp.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using RecepiesApp.Data.Generators;
    using RecepiesApp.Data.Repository;
    using RecepiesApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RecepiesApp.Data.RecepiesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RecepiesApp.Data.RecepiesDbContext context)
        {
        }
    }
}
