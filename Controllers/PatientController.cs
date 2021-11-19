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
                    patients = db.Patients.Include("Doctor").ToList();
                }
            }
            catch(Exception ex)
            {
                ViewData["ServerError"] = ex.Message;
            }
            return PartialView(patients);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Patient model)
        //{

        //    using (var db = new HMSEntities())
        //    {

        //        var patient = db.Patients.Where(x => x.ID == model.ID).FirstOrDefault();

        //        if (patient != null)
        //        {
        //            patient.Name = model.Name;
        //            patient.Age = model.Age;
        //            patient.Gender = model.Gender;
        //            patient.Address = model.Address;
        //            patient.Disease = model.Disease;
        //            db.SaveChanges();
        //            ViewBag.Message = "Patient details updated successfully";
        //            return RedirectToAction("List");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Invalid details! Please check the details carefully and try again";
        //            return View();
        //        }
        //    }

        //}

        [HttpPost]
        public JsonResult Update(Patient model)
        { //{"IsSuccess":"true","Message":""}
            HMSResponse res = new HMSResponse();

            try
            {

                using (var db = new HMSEntities())
                {
                    //JavaScriptSerializer deser = new JavaScriptSerializer();
                    //Room model = deser.Deserialize<Room>(modeljson);
                    var patient = db.Patients.Where(x => x.ID == model.ID).FirstOrDefault();

                    if (patient != null)
                    {
                        patient.Name = model.Name;
                        patient.Age = model.Age;
                        patient.Gender = model.Gender;
                        patient.Address = model.Address;
                        patient.Disease = model.Disease;
                        db.SaveChanges();

                        res.IsSuccess = true;
                        res.Message = "Patient details updated successfully.";
                        //res.Data = new Room { ID = 999 };
                    }
                    else
                    {
                        res.Message = "Failed to update patient details";

                    }
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Delete(Patient model)
        { //{"IsSuccess":"true","Message":""}
            HMSResponse res = new HMSResponse();

            try
            {

                using (var db = new HMSEntities())
                {
                    //JavaScriptSerializer deser = new JavaScriptSerializer();
                    //Room model = deser.Deserialize<Room>(modeljson);
                    var patient = db.Patients.Where(x => x.ID == model.ID).FirstOrDefault();

                    if (patient != null)
                    {
                        db.Patients.Remove(patient);
                        db.SaveChanges();
                        res.IsSuccess = true;
                        res.Message = "Patient record deleted successfully.";
                    }
                    else
                    {
                        res.Message = "Failed to find record to delete";

                    }
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }
            return Json(res, JsonRequestBehavior.AllowGet);

        }
    }
}