using HMSAppWIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HMSAppWIN.Controllers
{
    public class RoomController : Controller
    {
//        public RoomController(){
//            TempData["Controller"] = "Room";

//}
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
           
            IList<HMSAppWIN.Models.Room> rooms = null;
            try
            {
                using (var db = new HMSEntities())
                {
                    rooms = db.Rooms.ToList();

                }
            }
            catch(Exception ex)
            {
                ViewData["ServerError"] = ex.Message;
            }
            return PartialView(rooms);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room model)
        {

            using (var db = new HMSEntities())
            {

                var room = db.Rooms.Where(x => x.ID == model.ID).FirstOrDefault();

                if (room != null)
                {
                    room.RoomNo = model.RoomNo;
                    room.RoomType = model.RoomType;
                    room.Status = model.Status;
                    db.SaveChanges();
                    ViewBag.Message = "Room updated successfully";
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.Message = "Invalid room! Update failed";
                    return View();
                }
            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {

                using (var db = new HMSEntities())
                {

                    var room = db.Rooms.Where(x => x.ID == id).FirstOrDefault();

                    if (room != null)
                    {
                        db.Rooms.Remove(room);
                        db.SaveChanges();
                        ViewBag.Message = "Room deleted successfully";
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid room! Delete action failed";
                        return View();
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public JsonResult Update(Room model)
        { //{"IsSuccess":"true","Message":""}
            HMSResponse res = new HMSResponse();
            
            try
            {
                
                using (var db = new HMSEntities())
                {
                    //JavaScriptSerializer deser = new JavaScriptSerializer();
                    //Room model = deser.Deserialize<Room>(modeljson);
                    var room = db.Rooms.Where(x => x.ID == model.ID).FirstOrDefault();

                    if (room != null)
                    {
                        room.RoomNo = model.RoomNo;
                        room.RoomType = model.RoomType;
                        room.Status = model.Status;
                        db.SaveChanges();

                        res.IsSuccess = true;
                        res.Message = "Room updated successfully.";
                        //res.Data = new Room { ID = 999 };
                    }
                    else
                    {
                        res.Message = "Failed to update";
                        
                    }
                }
            }
            catch(Exception ex)
            {
                res.Message = ex.Message;
            }
            return Json(res, JsonRequestBehavior.AllowGet);

        }

    }
}