using MvcFirmaCagri1.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcFirmaCagri1.Controllers
{
    public class LoginController : Controller
    {
        dbis_TakipEntities4 db=new dbis_TakipEntities4();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblfirmalar p)
        {
            var bilgiler = db.tblfirmalar.FirstOrDefault(x => x.Mail == p.Mail && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail,false);
                Session["Mail"] = bilgiler.Mail.ToString();
                return RedirectToAction("", "Default1");
            }
            else
            {
                return RedirectToAction("Index");
            }
                 
        }
    }
}