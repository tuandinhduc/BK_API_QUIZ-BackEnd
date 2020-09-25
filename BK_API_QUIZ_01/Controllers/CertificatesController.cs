using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BK_API_QUIZ_01.DAL;
using BK_API_QUIZ_01.Filters;
using BK_API_QUIZ_01.Models;

namespace BK_API_QUIZ_01.Controllers
{
    public class CertificatesController : ApiController
    {
        private APIQuizDBContext db = new APIQuizDBContext();

        // GET: api/Certificates
        [AllowAnonymous]

        public IQueryable<Certificate> GetCertificate()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Certificate;
        }

        // GET: api/Certificates/5
        [AllowAnonymous]
        [ResponseType(typeof(Certificate))]
        public async Task<IHttpActionResult> GetCertificate(int id)
        {
            Certificate certificate = await db.Certificate.FindAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }

            return Ok(certificate);
        }

        // PUT: api/Certificates/5
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCertificate(int id, Certificate certificate)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (id != certificate.Id)
            {
                return BadRequest();
            }

            var examUpdate = certificate.Exam;
            certificate.Exam = new List<Exam>();
            db.Entry(certificate).State = EntityState.Modified;

            var certificateDB = db.Certificate.Include(c => c.Exam).FirstOrDefault(x => x.Id == id);
            var examRemoved = new List<Exam>();

            // remove all except old
            foreach (var item in certificateDB.Exam)
            {
                examRemoved.Add(db.Exam.FirstOrDefault(x => x.Id == item.Id));
            }
            foreach (var item in examRemoved)
            {
                certificateDB.Exam.Remove(item);
            }

            // remove exams exists in certificate variable
            foreach (var item in examUpdate)
            {
                certificateDB.Exam.Add(db.Exam.FirstOrDefault(x => x.Id == item.Id));
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateExists(id))
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

        // POST: api/Certificates
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(Certificate))]
        public async Task<IHttpActionResult> PostCertificate(Certificate certificate)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var examadd = certificate.Exam;
            certificate.Exam = new List<Exam>();

            foreach (var item in examadd)
            {
                certificate.Exam.Add(db.Exam.FirstOrDefault(x => x.Id == item.Id));
            }

            db.Certificate.Add(certificate);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = certificate.Id }, certificate);
        }

        // DELETE: api/Certificates/5
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(Certificate))]
        public async Task<IHttpActionResult> DeleteCertificate(int id)
        {
            Certificate certificate = await db.Certificate.FindAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }

            db.Certificate.Remove(certificate);
            await db.SaveChangesAsync();

            return Ok(certificate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CertificateExists(int id)
        {
            return db.Certificate.Count(e => e.Id == id) > 0;
        }
    }
}