using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class EsyaPersonelOnarimJTMap:BaseMap<EsyaPersonelOnarimJT>
    {
        public EsyaPersonelOnarimJTMap()
        {
            ToTable("Onarimlar");
            Ignore(x => x.ID);
            HasKey(x => new
            {
                x.PersonelID,
                x.EsyaID
            });
        }
    }
}
