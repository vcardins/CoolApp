using CoolChat.Infraestructure.Data;

namespace CoolChat.Infraestructure.Seeders
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