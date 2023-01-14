using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class ZimmetTip : BaseEntity
    {
        public string ZimmetTipi { get; set; }

        public string Aciklama { get; set; }

        //Relational Properties

        public virtual List<Esya> Esyalar { get; set; }

    }
}
