using Project.ENTITIES.Models;
using Project.MAP.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Context
{
    public class MyContext:DbContext
    {
        public MyContext():base("myConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EsyaMap());
            modelBuilder.Configurations.Add(new EsyaPersonelOnarimJTMap());
            modelBuilder.Configurations.Add(new KullaniciMap());
            modelBuilder.Configurations.Add(new PersonelMap());
            modelBuilder.Configurations.Add(new SirketMap());
            modelBuilder.Configurations.Add(new EsyaZimmetLogMap());
            modelBuilder.Configurations.Add(new ZimmetTipMap());

            modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(50));
        }

        public DbSet<Esya> Esyalar { get; set; }
        public DbSet<EsyaPersonelOnarimJT> EsyaPersonelOnarimJTs { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Sirket> Sirketler { get; set; }
        public DbSet<EsyaZimmetLog> EsyaZimmetLoglar { get; set; }
        public DbSet<ZimmetTip> ZimmetTipleri { get; set; }
    }
}
