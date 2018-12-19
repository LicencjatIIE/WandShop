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
    public class WandShopContext : DbContext
    {
        public WandShopContext() : base("WandShop")
        {

        }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameParam> GamesParm { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<PlayerPart> PlayerPart { get; set; }

        
    }
}
