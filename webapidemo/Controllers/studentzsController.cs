using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;


using webapidemo;

namespace webapidemo.Controllers
{
    public class studentzsController : ApiController
    {
        private demoazureEntities db = new demoazureEntities();

        // GET: api/studentzs
        public IQueryable<studentz> Getstudentz()
        {
            return db.studentz;
        }

        // GET: api/studentzs/5
        [ResponseType(typeof(studentz))]
        public IHttpActionResult Getstudentz(int id)
        {
            studentz studentz = db.studentz.Find(id);
            if (studentz == null)
            {
                return NotFound();
            }

            return Ok(studentz);
        }

        // PUT: api/studentzs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstudentz(int id, studentz studentz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentz.id)
            {
                return BadRequest();
            }

            db.Entry(studentz).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!studentzExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/studentzs
        [ResponseType(typeof(studentz))]
        public IHttpActionResult Poststudentz(studentz studentz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.studentz.Add(studentz);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (studentzExists(studentz.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentz.id }, studentz);
        }

        // DELETE: api/studentzs/5
        [ResponseType(typeof(studentz))]
        public IHttpActionResult Deletestudentz(int id)
        {
            studentz studentz = db.studentz.Find(id);
            if (studentz == null)
            {
                return NotFound();
            }

            db.studentz.Remove(studentz);
            db.SaveChanges();

            return Ok(studentz);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool studentzExists(int id)
        {
            return db.studentz.Count(e => e.id == id) > 0;
        }
    }
}