namespace RecepiesApp.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RecepiesApp.Data.RecepiesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RecepiesApp.Data.RecepiesDbContext context)
        {
            // TODO: Add some data on every program start
            // 3 Users with random data and a default picture url
            // 1-2 Recepies per user with random data and a default picture url - the picture must be of mango fruit salad
            // Minimum 3 Tags per recepie (Not needed to be new every time, but yet they should be a bit randomish)
            // Minimum 3 Phases per recepie - each between 5-30 minutes
            // Minimum 1 comment per Recepie, from an existing user
            // Between 1 and 5 users (use old users if needed) that have put this recepie in their favourite list
        }
    }
}
