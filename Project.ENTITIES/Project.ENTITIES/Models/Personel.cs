using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Personel : BaseEntity
    {
        public string AdSoyad { get; set; }

        public int Yas { get; set; }

        public string Pozisyon { get; set; }

        //Foreign Key
        public int? SirketID { get; set; }

        //Foreign Key
        public int? KullaniciID { get; set; }

        public string SicilNo  { get; set; }

        public Departman Departman { get; set; }

        //Relational Properties

        public virtual Sirket Sirket { get; set; }

        public virtual List<Esya> Esyalar { get; set; }

        public virtual List<EsyaPersonelOnarimJT> EsyaPersonelOnarimJTs { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual List<EsyaZimmetLog> EsyaZimmetLoglar { get; set; }


    }
}
