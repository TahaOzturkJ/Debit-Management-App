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
    public class HomeController : Controller
    {
        KullaniciRepository _kuRep;
        PersonelRepository _pRep;

        public HomeController()
        {
            _kuRep = new KullaniciRepository();
            _pRep = new PersonelRepository();
        }

        #region Login

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Kullanici kullanici,string username,string password)
        {
            Kullanici ku = _kuRep.FirstOrDefault(x => x.KullaniciAdi == username && x.Sifre == password);

            Personel p = _pRep.FirstOrDefault(x => x.KullaniciID == ku.ID);

            if (ku != null)
            {
                if (p.Departman == ENTITIES.Enums.Departman.IT)
                {
                    Session["Admin"] = ku;
                    Session["LoggedPersonelName"] = p.AdSoyad;
                    Session["LoggedPersonelPosition"] = p.Pozisyon;
                    return RedirectToAction("ITPanelEnvanter", "Panel");
                }

                Session["LoggedPersonel"] = p.AdSoyad;
                Session["LoggedPersonelPosition"] = p.Pozisyon;
                return RedirectToAction("IKPanelZimmetListe", "Panel");
            }
            return View();
        }

        //DevExpress Telerik incele

        //Toplu Seçim (ik)

        //Excel Import/Export
        #endregion

        #region Register

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Kullanici kullanici,string username,string password,int personeldepartman)
        {
            if (kullanici.Sifre is null)
            {
                ViewBag.Message = "Lütfen Şifre Giriniz";
            }
            else
            {
                Kullanici ku = _kuRep.FirstOrDefault(x => x.KullaniciAdi == kullanici.KullaniciAdi);
                if (ku == null)
                {
                    _kuRep.Add(kullanici);
                    return RedirectToAction("Login");
                }
                ViewBag.Message("Bu Kullanıcı Adı Sistemde Mevcut");
            }

            return View();
        }

        #endregion
    }
}