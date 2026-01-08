using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcFirmaCagri1.Models.Entity;

namespace MvcFirmaCagri1.Controllers
{
    [Authorize]
    public class Default1Controller : Controller
    {
        // GET: Default1
        public ActionResult Index()
        {
            return View();
        }
        dbis_TakipEntities4 db = new dbis_TakipEntities4();



        public ActionResult AktifCagrilar()
        {
            var mail = (string)Session["Mail"];
            var id = db.tblfirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var cagrilar = db.tblcagrilar.Where(x => x.Durum == true).ToList();
            return View(cagrilar);
        }
        public ActionResult PasifCagrilar()
        {
            var mail = (string)Session["Mail"];
            var id = db.tblfirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();

            var cagrilar = db.tblcagrilar.Where(x => x.Durum == false).ToList();
            return View(cagrilar);
        }
        [HttpGet]
        public ActionResult YeniCagri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCagri(tblcagrilar p)
        {
            var mail = (string)Session["Mail"];
            var id = db.tblfirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            p.Durum = true;

            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.CagriFirma = id;
            db.tblcagrilar.Add(p);
            db.SaveChanges();
            return RedirectToAction("AktifCagrilar");
        }

        public ActionResult CagriDetay(int id)
        {
            var degerler = db.tblcagridetay.Where(x => x.Cagri == id).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult CagriGetir(int id)
        {
            var cagri = db.tblcagrilar.Find(id);
            return View("CagriGetir", cagri);
        }
        [HttpPost]
        public ActionResult CagriDüzenle(tblcagrilar p)
        {
            var cagri = db.tblcagrilar.Find(p.ID);
            cagri.Konu = p.Konu;
            cagri.Aciklama = p.Aciklama;
            db.SaveChanges();
            return RedirectToAction("AktifCagrilar");
        }
        [HttpGet]
        public ActionResult ProfilDuzenle()
        {
            var mail = (string)Session["Mail"];
            var id = db.tblfirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();

            var profil = db.tblfirmalar.Where(x => x.ID == id).FirstOrDefault();
            return View(profil);
        }
        public ActionResult AnaSayfa()
        {
            var mail = (string)Session["Mail"];
            var id = db.tblfirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var toplamcagri = db.tblcagrilar.Where(x => x.CagriFirma == id).Count();
            var aktifcagri = db.tblcagrilar.Where(x => x.CagriFirma == id && x.Durum == true).Count();
            var pasifcagri = db.tblcagrilar.Where(x => x.CagriFirma == id && x.Durum == false).Count();
            var yetkili = db.tblfirmalar.Where(x => x.ID == id).Select(y =>y.Yetkili).FirstOrDefault();
            var sektor = db.tblfirmalar.Where(x => x.ID == id).Select(y => y.Sektor).FirstOrDefault();
            var firmaadi = db.tblfirmalar.Where(x => x.ID == id).Select(y => y.Ad).FirstOrDefault();
            ViewBag.c1 = toplamcagri;
            ViewBag.c2 = aktifcagri;
            ViewBag.c3= pasifcagri;
            ViewBag.c4= yetkili;
            ViewBag.c5 = sektor;
            ViewBag.c6= firmaadi;
            return View();
        }
        public PartialViewResult Pantial1()
        {
            var mail = (string)Session["Mail"];
            var id = db.tblfirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var mesajlar = db.tblmesajlar.Where(x => x.Alici == mail).ToList();
            var mesajsayisi = db.tblmesajlar.Where(x => x.Alici == mail && x.Durum == true).Count();
            ViewBag.ml = mesajsayisi;
            return PartialView(mesajlar);
        }
        public PartialViewResult Partial2()
        {
            var mail = (string)Session["Mail"];
            var id = db.tblfirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var cagrilar = db.tblcagrilar.Where(x => x.CagriFirma == id && x.Durum==true).ToList();
            var cagrisayisi = db.tblcagrilar.Where(x => x.CagriFirma == id && x.Durum == true).Count();
            ViewBag.m1 = cagrisayisi;
            return PartialView(cagrilar);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult Partial3()
        {
            return PartialView();
        }
    } 
}