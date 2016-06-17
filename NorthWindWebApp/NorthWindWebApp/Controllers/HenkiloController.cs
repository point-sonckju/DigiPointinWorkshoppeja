using NorthWindWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWindWebApp.Controllers
{
    public class HenkiloController : Controller
    {
        // GET: Henkilo
        public ActionResult Index()
        {
            

            ProjektitEntities entities = new ProjektitEntities();
            List<Henkilot> model = entities.Henkilot.ToList();
            entities.Dispose();

            return View(model);
        }
        public ActionResult HaeTunnit(string id)
        {
            int iid;
            iid = int.Parse(id);
            DateTime vertailuPvm = DateTime.Today.AddDays(-150);
            //vertailuPvm = Convert.ToDateTime("01/05/2016");

            ProjektitEntities entities = new ProjektitEntities();
            List<Tunnit> tunnit = (from tt in entities.Tunnit
                                   where tt.Henkilo_id == iid
                                   && tt.Pvm > vertailuPvm
                                   select tt).ToList();
            entities.Dispose();

            List<SimpleTuntiData> result = new List<SimpleTuntiData>();
            foreach (Tunnit tunti in tunnit)
            {
                SimpleTuntiData data = new SimpleTuntiData();
                data.tunnit = tunti.Tunnit1.ToString();
                data.paivamaara = tunti.Pvm.ToString();
                data.henkilonumero = tunti.Henkilo_id.ToString();
                data.projektinumero = tunti.Projekti_id.ToString();

                result.Add(data);

            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}