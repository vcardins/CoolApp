using CoolChat.Core.Models;
using CoolChat.Infraestructure.Data;
using CoolChat.Infraestructure.Seeders;

namespace CoolChat.Infraestructure.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            //context.Database.CreateUniqueIndex<User>(x => x.Username);
            AppDataSeeder.Seed(context);
        }
    }
}
