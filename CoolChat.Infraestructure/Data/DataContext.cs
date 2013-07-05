using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CoolChat.Core.Models;

namespace CoolChat.Infraestructure.Data
{
    public class DataContext : BaseContext<DataContext>
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); 
        }
                   
    }
}