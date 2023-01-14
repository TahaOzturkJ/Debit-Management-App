using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class EsyaZimmetLog : BaseEntity
    {
        public string IslemTuru { get; set; }

        public ZimmetIO ZimmetYon { get; set; }

        //Foreign Key
        public int EsyaID { get; set; }

        //Foreign Key 
        public int? PersonelID { get; set; }

        public string Aciklama { get; set; }

        public DateTime IslemTarihi { get; set; }

        //Relational Properties

        public virtual Esya Esya { get; set; }

        public virtual Personel Personel { get; set; }


        public EsyaZimmetLog()
        {
            IslemTarihi = DateTime.Now;
        }

    }
}
