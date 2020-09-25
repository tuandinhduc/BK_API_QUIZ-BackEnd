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
using BK_API_QUIZ_01.Models;
using BK_API_QUIZ_01.Filters;
namespace BK_API_QUIZ_01.Controllers
{

    public class Exam_attempController : ApiController
    {
        private APIQuizDBContext db = new APIQuizDBContext();

        // GET: api/Exam_attemp
        [AllowAnonymous]
        public IQueryable<Exam_attemp> GetExam_attemp()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Exam_attemp;
        }

        // GET: api/Exam_attemp/5
        [AllowAnonymous]
        [ResponseType(typeof(Exam_attemp))]
        public async Task<IHttpActionResult> GetExam_attemp(int id)
        {
            Exam_attemp exam_attemp = await db.Exam_attemp.FindAsync(id);
            if (exam_attemp == null)
            {
                return NotFound();
            }

            return Ok(exam_attemp);
        }

        // PUT: api/Exam_attemp/5
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExam_attemp(int id, Exam_attemp exam_attemp)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exam_attemp.id)
            {
                return BadRequest();
            }

            db.Entry(exam_attemp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exam_attempExists(id))
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

        // POST: api/Exam_attemp
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(Exam_attemp))]
        public async Task<IHttpActionResult> PostExam_attemp(Exam_attemp exam_attemp)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exam_attemp.Add(exam_attemp);
            await db.SaveChangesAsync();

            var dto = new
            {
                id = exam_attemp.id,
                UserId = exam_attemp.UserId,
                ExamId = exam_attemp.ExamId,
                attempt = exam_attemp.attempt,
                SumGrade = exam_attemp.SumGrade,
                TimeStart = exam_attemp.TimeStart,
                TimeFinish = exam_attemp.TimeFinish,
                Preview = exam_attemp.Preview
            };

            return CreatedAtRoute("DefaultApi", new { id = exam_attemp.id }, dto);
        }

        // DELETE: api/Exam_attemp/5
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(Exam_attemp))]
        public async Task<IHttpActionResult> DeleteExam_attemp(int id)
        {
            Exam_attemp exam_attemp = await db.Exam_attemp.FindAsync(id);
            if (exam_attemp == null)
            {
                return NotFound();
            }

            db.Exam_attemp.Remove(exam_attemp);
            await db.SaveChangesAsync();

            return Ok(exam_attemp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Exam_attempExists(int id)
        {
            return db.Exam_attemp.Count(e => e.id == id) > 0;
        }
    }
}