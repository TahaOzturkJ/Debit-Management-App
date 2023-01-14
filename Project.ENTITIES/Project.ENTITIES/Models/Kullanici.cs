using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Kullanici:BaseEntity
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public UserRole Role { get; set; }

        //Relational Properties

        public virtual Personel Personel { get; set; }
    }
}
