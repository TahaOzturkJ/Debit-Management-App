using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.WEBUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.Controllers
{
    public class PanelController : Controller
    {

        EsyaRepository _eRep;
        EsyaZimmetLogRepository _ezlRep;
        PersonelRepository _pRep;
        SirketRepository _sRep;
        ZimmetTipRepository _zRep;


        public PanelController()
        {
            _ezlRep = new EsyaZimmetLogRepository();
            _eRep = new EsyaRepository();
            _pRep = new PersonelRepository();
            _sRep = new SirketRepository();
            _zRep = new ZimmetTipRepository();
        }


        #region IT Panel

        #region IT Panel - Envanter

        // GET: Panel
        public ActionResult ITPanelEnvanter()
        {

            IndexVM ivm = new IndexVM
            {
                Esyalar = _eRep.Where(x => x.BagliEsyaID == 0),
                ZimmetTipleri = _zRep.GetActives()
            };

            return View(ivm);

        }

        [HttpPost]
        public ActionResult ITPanelEnvanter(IndexVM ivm, string firma, string demirbas, string aciklama, string sicilno, string personel, string serino, string cpu, string isletimsistemi, string markamodel, string disktipi, string diskkapasitesi, string ramslot, string ram, string ekran, string officelisanslari, string digerlisanslar,string zimmetdurumu,int? zimmettipi)
        {
            if (firma == "" && demirbas == "" && aciklama == "" && sicilno == "" && personel == "" && serino == "" && cpu == "" && isletimsistemi == "" && zimmettipi == null && markamodel == "" && disktipi == "" && diskkapasitesi == "" && ramslot == "" && ram == "" && ekran == "" && officelisanslari == "" && digerlisanslar == "" && zimmetdurumu == "")
            {
                ivm.Esyalar = _eRep.Where(x => x.BagliEsyaID == 0);
                ivm.ZimmetTipleri = _zRep.GetActives();
            }
            else
            {
                ivm.ZimmetTipleri = _zRep.GetActives();

                ivm.Esyalar = _eRep
                .Where(x => x.Sirket.SirketAdi == firma || firma == "")
                .Where(x => x.ZimmetTipID == zimmettipi || zimmettipi == null)
                .Where(x => x.DemirbasNo == demirbas || demirbas == "")
                .Where(x => x.BagliEsyaID == 0)
                .Where(x => x.Personel.SicilNo == sicilno || sicilno == "")
                .Where(x => x.Personel.AdSoyad == personel || personel == "")
                .Where(x => x.ZimmetTip.Aciklama == aciklama || aciklama == "")
                .Where(x => x.SeriNo == serino || serino == "")
                .Where(x => x.CPU == cpu || cpu == "")
                .Where(x => x.Marka == markamodel || markamodel == "")
                .Where(x => x.DiskTipi == disktipi || disktipi == "")
                .Where(x => x.DiskKapasitesi == diskkapasitesi || diskkapasitesi == "")
                .Where(x => x.RamSlot == ramslot || ramslot == "")
                .Where(x => x.Ram == ram || ram == "")
                .Where(x => x.Ekran == ekran || ekran == "")
                .Where(x => x.EsyaDurumu.ToString() == zimmetdurumu || zimmetdurumu == "")
                .ToList();
            }

            return View(ivm);
        }

        #endregion


        #region IT Panel - Hareketler

        public ActionResult ITPanelHareketler()
        {
            IndexVM ivm = new IndexVM
            {
                EsyaZimmetLoglar = _ezlRep.GetActives()
            };
            return View(ivm);
        }

        [HttpPost]
        public ActionResult ITPanelHareketler(IndexVM ivm, string demirbas, string aciklama, string sicilno, string personel, string serino, string markamodel)
        {
            if (demirbas == "" && aciklama == "" && sicilno == "" && personel == "" && serino == "" && markamodel == "")
            {
                ivm.EsyaZimmetLoglar = _ezlRep.GetActives();
            }
            else
            {
                ivm.EsyaZimmetLoglar = _ezlRep
                    .Where(x => x.Esya.DemirbasNo == demirbas || demirbas == "")
                    .Where(x => x.Personel.SicilNo == sicilno || sicilno == "")
                    .Where(x => x.Personel.AdSoyad == personel || personel == "")
                    .Where(x => x.Esya.SeriNo == serino || sicilno == "")
                    .Where(x => x.Aciklama == aciklama || aciklama == "")
                    .Where(x => x.Esya.Marka == markamodel || markamodel == "")
                    .ToList();
            }

            return View(ivm);
        }

        #endregion


        #region Demirbaş Ekleme (Partial-Modal)

        [HttpGet]
        public ActionResult Create()
        {
            IndexVM ivm = new IndexVM
            {
                ZimmetTipleri = _zRep.GetActives()
            };

            Session["AddSuccess"] = "error";


            return PartialView("AddItemPartial", ivm);
        }

        [HttpPost]
        public ActionResult Create(IndexVM ivm, DateTime garantibaslangic, DateTime garantibitis, DateTime faturatarihi, int zimmettipi)
        {
            ivm.ZimmetTipleri = _zRep.GetActives();

            ivm.Esya.GarantiBaslangicTarihi = garantibaslangic;
            ivm.Esya.GarantiBitisTarihi = garantibitis;
            ivm.Esya.FaturaTarihi = faturatarihi;
            ivm.Esya.ZimmetTipID = zimmettipi;

            if (ivm.Esya.ZimmetTipID == 2 || ivm.Esya.ZimmetTipID == 3 || ivm.Esya.ZimmetTipID == 7)
            {
                ivm.Esya.BagliEsyaID = 0;
            }

            _eRep.Add(ivm.Esya);

            Session["AddSuccess"] = "success";

            return PartialView("AddItemPartial", ivm);
        }

        #endregion


        #region Demirbaş Güncelleme (Partial-Modal)

        public ActionResult Edit(int id)
        {
            var Personeller = _pRep.GetActives();
            var Sirketler = _sRep.GetActives();

            Session["EditSuccess"] = "error";

            var Dates = _eRep.Find(id);
            var gbtS = Dates.GarantiBaslangicTarihi;
            var gbtE = Dates.GarantiBitisTarihi;
            var ft = Dates.FaturaTarihi;

            if (Personeller != null)
            {
                ViewBag.personelData = Personeller;
            }

            if (Sirketler != null)
            {
                ViewBag.sirketData = Sirketler;
            }

            if (gbtS != null)
            {
                ViewBag.gbtSData = gbtS;
            }

            if (gbtE != null)
            {
                ViewBag.gbtEData = gbtE;
            }

            if (ft != null)
            {
                ViewBag.ftData = ft;
            }

            IndexVM ivm = new IndexVM
            {
                Esya = _eRep.Find(id)
            };

            return PartialView("EditItemPartial", ivm);

        }

        [HttpPost]
        public ActionResult Edit(IndexVM ivm, DateTime? garantibaslangic, DateTime? garantibitis, DateTime? faturatarihi)
        {

            ivm.Esya.GarantiBaslangicTarihi = garantibaslangic;
            ivm.Esya.GarantiBitisTarihi = garantibitis;
            ivm.Esya.FaturaTarihi = faturatarihi;

            _eRep.Update(ivm.Esya);

            Session["EditSuccess"] = "success";

            return PartialView("EditItemPartial", ivm);
        }

        #endregion


        #region Demirbaş Zimmetleme (Partial-Modal)

        public ActionResult Debit(int id)
        {
            var Personeller = _pRep.GetActives();
            var Sirketler = _sRep.GetActives();
            var Dates = _eRep.Find(id);
            var gbtS = Dates.GarantiBaslangicTarihi;
            var gbtE = Dates.GarantiBitisTarihi;
            var ft = Dates.FaturaTarihi;

            Session["DebitSuccess"] = "error";


            if (Personeller != null)
            {
                ViewBag.personelData = Personeller;
            }

            if (Sirketler != null)
            {
                ViewBag.sirketData = Sirketler;
            }

            if (gbtS != null)
            {
                ViewBag.gbtSData = gbtS;
            }

            if (gbtE != null)
            {
                ViewBag.gbtEData = gbtE;
            }

            if (ft != null)
            {
                ViewBag.ftData = ft;
            }

            IndexVM ivm = new IndexVM
            {
                Esya = _eRep.Find(id),
                Personeller = _pRep.GetActives()
            };

            return PartialView("DebitItemPartial", ivm);

        }

        [HttpPost]
        public ActionResult Debit(IndexVM ivm,int zimmetlenecekpersonel, DateTime? planlananzimmet, DateTime? planlananiade, DateTime? garantibaslangic, DateTime? garantibitis, DateTime? faturatarihi)
        {
            ivm.Esya.PersonelID = zimmetlenecekpersonel;
            ivm.Esya.EsyaDurumu = ENTITIES.Enums.EsyaDurumu.ZimmetPlanlandi;
            ivm.Esya.GarantiBaslangicTarihi = garantibaslangic;
            ivm.Esya.GarantiBitisTarihi = garantibitis;
            ivm.Esya.FaturaTarihi = faturatarihi;
            ivm.Esya.PlanlananZimmetTarihi = planlananzimmet;
            ivm.Esya.PlanlananIadeTarihi = planlananiade;

            _eRep.Update(ivm.Esya);

            Session["DebitSuccess"] = "success";

            return PartialView("DebitItemPartial", ivm);
        }


        #endregion


        #region Demirbaş İlişkilendirme

        public ActionResult ITPanelIliskiler(int id)
        {
            IndexVM ivm = new IndexVM
            {
                Esyalar = _eRep.GetActives(),
                Esya = _eRep.Find(id),
            };

            Session["bagliEsyaID"] = id;


            return View(ivm);
        }

        [HttpPost]
        public ActionResult ITPanelIliskiler(int demirbasID,Esya esya)
        {
            if (_eRep.Find(demirbasID).BagliEsyaID != null)
            {
                _eRep.Find(demirbasID).BagliEsyaID = null;
                _eRep.Save();
                
            }
            else
            {
                _eRep.Find(demirbasID).BagliEsyaID = Convert.ToInt32(Session["bagliEsyaID"].ToString());
                _eRep.Save();

            }



            return RedirectToAction("ITPanelIliskiler");
        }


        #endregion

        #endregion


        #region IK Panel

        #region IK Panel - Envanter

        public ActionResult IKPanelZimmetListe()
        {
            var Personeller = _pRep.GetActives();
            var Sirketler = _sRep.GetActives();

            if (Personeller != null)
            {
                ViewBag.personelData = Personeller.Where(x=>x.AdSoyad != "admin");
            }

            if (Sirketler != null)
            {
                ViewBag.sirketData = Sirketler;
            }

            IndexVM ivm = new IndexVM
            {
                Esyalar = _eRep.GetActives(),
            };

            if (Session["selectedID"] != null)
            {
                ivm.Esya = _eRep.Find(Convert.ToInt32(Session["selectedID"].ToString()));
            }



            return View(ivm);
        }

        [HttpPost]
        public ActionResult IKPanelZimmetListe(IndexVM ivm, string sicilno, string firma, string demirbas, string zimmetdurumu, string personel, DateTime? firstdate, DateTime? lastdate)
        {
            if (firma == "" && demirbas == "" && sicilno == "" && firstdate == null && lastdate == null && personel == "" && zimmetdurumu == "")
            {
                ivm.Esyalar = _eRep.GetActives();
            }
            else
            {
                if (firstdate == null && lastdate != null)
                {
                    ivm.Esyalar = _eRep
                    .Where(x => x.Sirket.SirketAdi == firma || firma == "")
                    .Where(x => x.DemirbasNo == demirbas || demirbas == "")
                    .Where(x => x.Personel.SicilNo == sicilno || sicilno == "")
                    .Where(x => x.Personel.AdSoyad == personel || personel == "")
                    .Where(x => x.EsyaDurumu.ToString() == zimmetdurumu || zimmetdurumu == "")
                    .ToList();

                    // TODO: ViewBag.Message ile ilk tarih seçmeleri için uyar!

                    return View(ivm);
                }
                else if (firstdate != null && lastdate == null)
                {
                    ivm.Esyalar = _eRep
                    .Where(x => x.Sirket.SirketAdi == firma || firma == "")
                    .Where(x => x.DemirbasNo == demirbas || demirbas == "")
                    .Where(x => x.Personel.SicilNo == sicilno || sicilno == "")
                    .Where(x => x.Personel.AdSoyad == personel || personel == "")
                    .Where(x => x.EsyaDurumu.ToString() == zimmetdurumu || zimmetdurumu == "")
                    .Where(x => x.ZimmetTarihi >= firstdate)
                    .ToList();
                }
                else if (firstdate != null && lastdate != null)
                {
                    ivm.Esyalar = _eRep
                    .Where(x => x.Sirket.SirketAdi == firma || firma == "")
                    .Where(x => x.DemirbasNo == demirbas || demirbas == "")
                    .Where(x => x.Personel.SicilNo == sicilno || sicilno == "")
                    .Where(x => x.Personel.AdSoyad == personel || personel == "")
                    .Where(x => x.EsyaDurumu.ToString() == zimmetdurumu || zimmetdurumu == "")
                    .Where(x => x.ZimmetTarihi >= firstdate)
                    .Where(x => x.ZimmetTarihi <= lastdate)
                    .ToList();
                }
                else
                {
                    ivm.Esyalar = _eRep
                    .Where(x => x.Sirket.SirketAdi == firma || firma == "")
                    .Where(x => x.DemirbasNo == demirbas || demirbas == "")
                    .Where(x => x.Personel.SicilNo == sicilno || sicilno == "")
                    .Where(x => x.Personel.AdSoyad == personel || personel == "")
                    .Where(x => x.EsyaDurumu.ToString() == zimmetdurumu || zimmetdurumu == "")
                    .ToList();
                }

            }

            return View(ivm);
        }

        #endregion


        #region Demirbaş Zimmet Onaylama (Partial-Modal)

        public ActionResult DebitValidation(int id)
        {
            var Personeller = _pRep.GetActives();
            var Sirketler = _sRep.GetActives();

            Session["DebitValidationSuccess"] = "error";


            var Dates = _eRep.Find(id);
            var gbtS = Dates.GarantiBaslangicTarihi;
            var gbtE = Dates.GarantiBitisTarihi;
            var ft = Dates.FaturaTarihi;

            if (Personeller != null)
            {
                ViewBag.personelData = Personeller;
            }

            if (Sirketler != null)
            {
                ViewBag.sirketData = Sirketler;
            }

            if (gbtS != null)
            {
                ViewBag.gbtSData = gbtS;
            }

            if (gbtE != null)
            {
                ViewBag.gbtEData = gbtE;
            }

            if (ft != null)
            {
                ViewBag.ftData = ft;
            }

            IndexVM ivm = new IndexVM
            {
                Esya = _eRep.Find(id)
            };

            return PartialView("DebitItemValidationPartial", ivm);
        }

        [HttpPost]
        public ActionResult DebitValidation(IndexVM ivm, DateTime? garantibaslangic, DateTime? garantibitis, DateTime? faturatarihi)
        {
            ivm.Esya.EsyaDurumu = ENTITIES.Enums.EsyaDurumu.Zimmetlendi;
            ivm.Esya.ZimmetTarihi = DateTime.Now;
            ivm.Esya.GarantiBaslangicTarihi = garantibaslangic;
            ivm.Esya.GarantiBitisTarihi = garantibitis;
            ivm.Esya.FaturaTarihi = faturatarihi;

            EsyaZimmetLog ezl = new EsyaZimmetLog()
            {
                IslemTuru = "Zimmet",
                ZimmetYon = ENTITIES.Enums.ZimmetIO.Giris,
                EsyaID = ivm.Esya.ID,
                PersonelID = ivm.Esya.PersonelID,
                Aciklama = "Zimmet Girişi",
            };

            _ezlRep.Add(ezl);
            _eRep.Update(ivm.Esya);

            Session["DebitValidationSuccess"] = "success";

            return PartialView("DebitItemValidationPartial", ivm);
        }

        #endregion


        #region Demirbaş İade (Partial-Modal)

        public ActionResult Refund(int id)
        {
            var Personeller = _pRep.GetActives();
            var Sirketler = _sRep.GetActives();


            var Dates = _eRep.Find(id);
            var gbtS = Dates.GarantiBaslangicTarihi;
            var gbtE = Dates.GarantiBitisTarihi;
            var ft = Dates.FaturaTarihi;

            Session["RefundSuccess"] = "error";


            if (Personeller != null)
            {
                ViewBag.personelData = Personeller;
            }

            if (Sirketler != null)
            {
                ViewBag.sirketData = Sirketler;
            }

            if (gbtS != null)
            {
                ViewBag.gbtSData = gbtS;
            }

            if (gbtE != null)
            {
                ViewBag.gbtEData = gbtE;
            }

            if (ft != null)
            {
                ViewBag.ftData = ft;
            }

            IndexVM ivm = new IndexVM
            {
                Esya = _eRep.Find(id)
            };

            return PartialView("RefundItemPartial", ivm);
        }

        [HttpPost]
        public ActionResult Refund(IndexVM ivm, DateTime? garantibaslangic, DateTime? garantibitis, DateTime? faturatarihi)
        {
            ivm.Esya.EsyaDurumu = ENTITIES.Enums.EsyaDurumu.IadeEdildi;
            ivm.Esya.IadeTarihi = DateTime.Now;
            ivm.Esya.GarantiBaslangicTarihi = garantibaslangic;
            ivm.Esya.GarantiBitisTarihi = garantibitis;
            ivm.Esya.FaturaTarihi = faturatarihi;

            EsyaZimmetLog ezl = new EsyaZimmetLog()
            {
                IslemTuru = "İade",
                ZimmetYon = ENTITIES.Enums.ZimmetIO.Cikis,
                EsyaID = ivm.Esya.ID,
                PersonelID = ivm.Esya.PersonelID,
                Aciklama = "Demirbaş İadesi",
            };

            _ezlRep.Add(ezl);
            _eRep.Update(ivm.Esya);

            Session["RefundSuccess"] = "success";

            return PartialView("RefundItemPartial", ivm);
        }

        #endregion

        #endregion



    }
}