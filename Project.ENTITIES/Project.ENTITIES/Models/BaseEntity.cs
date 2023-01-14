using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime YaratilmaTarihi { get; set; }
        public DateTime? DuzenlenmeTarihi { get; set; }
        public DateTime? SilinmeTarihi { get; set; }
        public DataStatus Statu { get; set; }

        public BaseEntity()
        {
            YaratilmaTarihi = DateTime.Now;
            Statu = DataStatus.Inserted;
        }
    }
}
