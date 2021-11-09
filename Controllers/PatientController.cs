using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMSAppWIN.Models;

namespace HMSAppWIN.Controllers
{
    public class PatientController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            IList<HMSAppWIN.Models.Patient> patients = null;

            try
            {
                using (var db = new HMSEntities())
                {
                    patients = db.Patients.ToList();
                }
            }
            catch(Exception ex)
            {
                ViewData["ServerError"] = ex.Message;
            }
            return PartialView(patients);
        }
    }
}