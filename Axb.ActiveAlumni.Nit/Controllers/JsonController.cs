//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web;
//using System.Web.Http;
//using Axb.ActiveAlumni.Nit.Entities;
//using Axb.ActiveAlumni.Nit.Models.Axb.ActiveAlumni;

//namespace Axb.ActiveAlumni.Nit.Controllers
//{
//    public class JsonController : ApiController
//    {
//        private NitContext db = new NitContext();

//        // GET api/Json
//        public IEnumerable<Event> GetEvents()
//        {
//            return db.Events.AsEnumerable();
//        }

//        // GET api/Json/5
//        public Event GetEvent(int id)
//        {
//            Event event = db.Events.Find(id);
//            if (event == null)
//            {
//                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
//            }

//            return event;
//        }

//        // PUT api/Json/5
//        public HttpResponseMessage PutEvent(int id, Event event)
//        {
//            if (ModelState.IsValid && id == event.EventId)
//            {
//                db.Entry(event).State = EntityState.Modified;

//                try
//                {
//                    db.SaveChanges();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    return Request.CreateResponse(HttpStatusCode.NotFound);
//                }

//                return Request.CreateResponse(HttpStatusCode.OK);
//            }
//            else
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest);
//            }
//        }

//        // POST api/Json
//        public HttpResponseMessage PostEvent(Event event)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Events.Add(event);
//                db.SaveChanges();

//                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, event);
//                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = event.EventId }));
//                return response;
//            }
//            else
//            {
//                return Request.CreateResponse(HttpStatusCode.BadRequest);
//            }
//        }

//        // DELETE api/Json/5
//        public HttpResponseMessage DeleteEvent(int id)
//        {
//            Event event = db.Events.Find(id);
//            if (event == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound);
//            }

//            db.Events.Remove(event);

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound);
//            }

//            return Request.CreateResponse(HttpStatusCode.OK, event);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            db.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}