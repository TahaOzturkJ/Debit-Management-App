using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Esya : BaseEntity
    {
        public string SeriNo { get; set; }

        //Foreign Key
        public int? PersonelID { get; set; }

        //Foreign Key
        public int? SirketID { get; set; }

        //Foreign Key
        public int? ZimmetTipID { get; set; }

        public int? BagliEsyaID { get; set; }
        public string Marka { get; set; }
        public string CPU { get; set; }
        public string Ram { get; set; }
        public string RamSlot { get; set; }
        public string Anakart { get; set; }
        public string DiskTipi { get; set; }
        public string DiskKapasitesi { get; set; }
        public string TakmaAd { get; set; }
        public string DemirbasNo { get; set; }
        public EsyaDurumu EsyaDurumu { get; set; }
        public GarantiDurumu GarantiDurumu { get; set; }
        public DateTime? GarantiBaslangicTarihi { get; set; }
        public DateTime? GarantiBitisTarihi { get; set; }
        public string Ekran { get; set; }
        public string IP { get; set; }
        public string MacETH { get; set; }
        public string MacWiFi { get; set; }
        public float? Bedel { get; set; }
        public string ParaBirimi { get; set; }
        public string FaturaNo { get; set; }
        public DateTime? FaturaTarihi { get; set; }
        public string DemirbasAdi { get; set; }
        public DateTime? PlanlananZimmetTarihi { get; set; }
        public DateTime? ZimmetTarihi { get; set; }
        public DateTime? PlanlananIadeTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }
        public string IMEI { get; set; }
        public string ZimmetleyenPersonel { get; set; }


        //Relational Properties

        public virtual Personel Personel { get; set; }

        public virtual List<EsyaPersonelOnarimJT> EsyaPersonelOnarimJTs { get; set; }

        public virtual Sirket Sirket { get; set; }

        public virtual List<EsyaZimmetLog> EsyaZimmetLoglar { get; set; }

        public virtual ZimmetTip ZimmetTip { get; set; }



        public Esya()
        {
            GarantiDurumu = GarantiDurumu.GarantisiVar;
            PersonelID = 1;
            SirketID = 1;
            EsyaDurumu = EsyaDurumu.Boşta;
            if (DateTime.Now > GarantiBitisTarihi)
            {
                GarantiDurumu = ENTITIES.Enums.GarantiDurumu.GarantisiBitti;
            }
        }




    }
}
