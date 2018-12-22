using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Concrete;
using Logic.Entities;

namespace Logic.Concrete
{
    public class EfWandShopContext : DbContext
    {
        public EfWandShopContext() : base("WandShop")
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerRound> PlayerRounds { get; set; }
        public DbSet<PlayerPart> PlayerParts { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Round> Rounds { get; set; }
        
    }
}
