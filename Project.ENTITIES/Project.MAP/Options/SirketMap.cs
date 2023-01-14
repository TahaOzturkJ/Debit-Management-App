using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class SirketMap:BaseMap<Sirket>
    {
        public SirketMap()
        {
            ToTable("Sirketler");
        }
    }
}
