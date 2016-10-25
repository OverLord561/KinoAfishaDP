using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KinoAfishaDP.Models
{
    
        public class AFISHAContext: DbContext
        {
            public AFISHAContext() : base("AFISHAContext") { }

            public DbSet<Film> Films { get; set; }
            public DbSet<MovieHouse> MovieHouses { get; set; }
            public DbSet<Session> Sessions { get; set; }
            public DbSet<UserComment> UserComments { get; set; }
            public DbSet<UserPhoto> UserPhotoes { get; set; }

        }
    
}