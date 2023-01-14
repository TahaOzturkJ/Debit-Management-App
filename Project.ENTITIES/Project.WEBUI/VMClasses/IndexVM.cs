using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WEBUI.VMClasses
{
    public class IndexVM
    {
        public List<Esya> Esyalar { get; set; }

        public Esya Esya { get; set; }

        public List<EsyaPersonelOnarimJT> EsyaPersonelOnarimJTs { get; set; }

        public EsyaPersonelOnarimJT EsyaPersonelOnarimJT { get; set; }

        public List<Kullanici> Kullanicilar { get; set; }

        public Kullanici Kullanici { get; set; }

        public List<Personel> Personeller { get; set; }

        public Personel Personel { get; set; }

        public List<Sirket> Sirketler { get; set; }

        public Sirket Sirket { get; set; }

        public List<EsyaZimmetLog> EsyaZimmetLoglar { get; set; }

        public EsyaZimmetLog EsyaZimmetLog { get; set; }

        public List<ZimmetTip> ZimmetTipleri { get; set; }

        public ZimmetTip ZimmetTip { get; set; }
    }
}