using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class EsyaPersonelOnarimJT : BaseEntity
    {
        public int PersonelID { get; set; }
        public int EsyaID { get; set; }

        public string Onaran { get; set; }
        public string Ariza { get; set; }
        public string Aciklama { get; set; }
        public double OnarimBedeli { get; set; }
        public string ParaBirimi { get; set; }
        public DateTime OnarimTarihi { get; set; }


        //Relational Properties

        public virtual Esya Esya { get; set; }
        public virtual Personel Personel { get; set; }
    }
}
