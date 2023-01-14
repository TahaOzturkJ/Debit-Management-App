using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Sirket : BaseEntity
    {
        public string SirketAdi { get; set; }
        public string SirketTelefonNo { get; set; }
        public string IrtibatSahibi { get; set; }

        //Relational Properties

        public virtual List<Personel> Personeller { get; set; }

        public virtual List<Esya> Esyalar { get; set; }
    }
}
