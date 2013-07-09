using CoolApp.Infraestructure.Data;

namespace CoolApp.Infraestructure.Seeders
{

    public static partial class AppDataSeeder
    {   
        public static void Seed(DataContext context)
        {
            SeedMembership(context);
            SeedFriendship(context);
        }
        
    }
}