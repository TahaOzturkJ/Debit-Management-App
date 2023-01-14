using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class KullaniciMap:BaseMap<Kullanici>
    {
        public KullaniciMap()
        {
            ToTable("Kullanicilar");
            HasOptional(x => x.Personel).WithRequired(x=>x.Kullanici);
        }
    }
}
