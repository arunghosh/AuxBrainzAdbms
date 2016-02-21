using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class ConnectController : BaseController
    {
        //
        // GET: /Connect/

        ConnectService _service = new ConnectService();

        [ChildActionOnly]
        public PartialViewResult ReqButton(int id)
        {
            return PartialView(id);
        }

        [ChildActionOnly]
        public PartialViewResult DeleteBtn(int id)
        {
            return PartialView(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {
            var msg = string.Empty;
            try
            {
                _service.RemoveConnection(id);
            }
            catch (SimpleException ex)
            {
                msg = ex.Message;
            }
            return Json(new { status = "Removed", errorMsg = msg });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(int id)
        {
            var status = true;
            var msg = string.Empty;
            try
            {
                _service.CreateConnectReq(id);
            }
            catch (SimpleException ex)
            {
                status = false;
                msg = ex.Message;
            }
            return Json(new { status = status, errorMsg =  msg });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(int ConnectId, ConnectStatusType Status)
        {
            var connect = _db.Connections.Find(ConnectId);
            connect.Status = Status;
            connect.RespondedOn = DateTime.UtcNow;
            _db.Entry(connect).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
            return Json(new { status = "Request " + Status.ToString() });
        }


    }
}
