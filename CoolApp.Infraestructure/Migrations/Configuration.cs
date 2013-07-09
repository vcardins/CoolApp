using CoolApp.Infraestructure.Seeders;
using CoolApp.Infraestructure.Data;
using System.Data.Entity.Migrations;

namespace CoolApp.Infraestructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;   
        }

        protected override void Seed(DataContext context)
        {
            //context.Database.CreateUniqueIndex<User>(x => x.Username);
            AppDataSeeder.Seed(context);
        }
    }
}
