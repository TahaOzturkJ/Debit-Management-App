using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class ZimmetTipMap:BaseMap<ZimmetTip>
    {
        public ZimmetTipMap()
        {
            ToTable("ZimmetTipleri");
        }
    }
}
