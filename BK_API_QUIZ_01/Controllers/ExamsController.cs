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

    public class ExamsController : ApiController
    {
        
        private APIQuizDBContext db = new APIQuizDBContext();

        // GET: api/Exams
        [AllowAnonymous]
        public IQueryable<Exam> GetExam()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Exam.Include(x => x.Quizs);
        }
        [AllowAnonymous]
        // GET: api/Exams/5
        [ResponseType(typeof(Exam))]
        public async Task<IHttpActionResult> GetExam(int id)
        {
            Exam exam = await db.Exam.FindAsync(id);


            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

        // PUT: api/Exams/5
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExam(int id, Exam exam)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (id != exam.Id)
            {
                return BadRequest();
            }
            var quizUpdate = exam.Quizs;
            exam.Quizs = new List<Quiz>();
            db.Entry(exam).State = EntityState.Modified;
            var examDB = db.Exam.Include(c => c.Quizs).FirstOrDefault(x => x.Id == id);

            var quizRemoved = new List<Quiz>();
            // remove all
            foreach (var item in examDB.Quizs)
            {
                quizRemoved.Add(db.Quiz.FirstOrDefault(x => x.Id == item.Id));
            }
            foreach (var item in quizRemoved)
            {
                examDB.Quizs.Remove(item);
            }

            // add quizs exits in exam variable
            foreach (var item in quizUpdate)
            {
                examDB.Quizs.Add(db.Quiz.FirstOrDefault(x => x.Id == item.Id));
            }


            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
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

        // POST: api/Exams
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(Exam))]
        public async Task<IHttpActionResult> PostExam(Exam exam)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var quizadd = exam.Quizs;
            exam.Quizs = new List<Quiz>();


            foreach (var item in quizadd)
            {
                exam.Quizs.Add(db.Quiz.FirstOrDefault(x => x.Id == item.Id));
            }
            db.Exam.Add(exam);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = exam.Id }, exam);
        }

        // DELETE: api/Exams/5
        [JwtAuthentication]
        [Authorize(Roles = "gv")]
        [ResponseType(typeof(Exam))]
        public async Task<IHttpActionResult> DeleteExam(int id)
        {
            Exam exam = await db.Exam.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            db.Exam.Remove(exam);
            await db.SaveChangesAsync();

            return Ok(exam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamExists(int id)
        {
            return db.Exam.Count(e => e.Id == id) > 0;
        }
    }
}