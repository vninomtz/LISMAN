using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.Web.Mvc;

namespace WebLisman.Controllers {
    public class HomeController : Controller {
        public ActionResult Index()
        {
           
            
            return View();
        }
            
        public ActionResult About(String token)
        {
            if (token == null) {
                ViewBag.Title = "Registrate a Lisman";
                ViewBag.Message = "Descarga LISMAN y unite a la experiencia Multijugador";
                return View();
            }
            int result = -1;
            try {
                using (var dataBase = new EntityModelContainer()) {
                    int exist = dataBase.AccountSet.Where(u => u.Key_confirmation == token).Count();
                    if (exist > 0) {
                        var accountValidate = dataBase.AccountSet.Where(u => u.Key_confirmation == token).FirstOrDefault();
                        accountValidate.Key_confirmation = "";
                        result = dataBase.SaveChanges();
                    }
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            if (result == -1) {
                ViewBag.Message = "No se pudo confirmar tu registro.";
            } else {
                ViewBag.Message = "Se confirmo tu registro con Éxito";
            }

            return View();
        }

        public ActionResult Contact()
        {
            
            return View();
        }
    }
}