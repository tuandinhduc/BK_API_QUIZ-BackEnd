using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using BK_API_QUIZ_01.Models;
using BK_API_QUIZ_01.Filters;
using BK_API_QUIZ_01.DAL;

namespace BK_API_QUIZ_01.Controllers
{
    [JwtAuthentication]
    public class UsersController : ApiController
    {
        private APIQuizDBContext db = new APIQuizDBContext();
        // GET: api/Users
        [HttpGet]
        [Route("api/logged/{username}")]
        public bool logged(string username)
        {
            var Username = System.Web.HttpContext.Current.User.Identity.Name;
            return username == Username;

        }
        [Authorize(Roles = "admin")] //chi admin moi co quyen lay toan bo user
        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        // GET: api/getprofile
        //lay thong tin nguoi dung hien tai dua tren token jwt
        [Route("api/getprofile")] 
        public User getProfile()
        {
            var Username = System.Web.HttpContext.Current.User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.UserName == Username);
            return user;
        }
        [Authorize(Roles = "admin")]
        [Route("api/users/{Id}")]
        public User GetUser(int Id)
        {
            User user = db.Users
            .FirstOrDefault(u => u.Id == Id);
            return user;
        }

        // PUT: api/updateprofile/{id} chi user hien tai moi co quyen update ban than
        //admin co quyen update moi user
        //[Authorize(Roles = "admin")]
        [Route("api/updateprofile/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Users = System.Web.HttpContext.Current.User.Identity.Name;
            if (id != user.Id ||(user.UserName != Users && Users!="admin"))
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        //chi user hien tai co quyen update pw
        //pw so trung truoc

        [Route("api/updatepassword/{Id}/{Pwc}/{Pwm}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int Id, string Pwc, string Pwm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Users = System.Web.HttpContext.Current.User.Identity.Name;
            User usercu = db.Users
                .FirstOrDefault(u => u.Id == Id);
            if (usercu.Password != Pwc || (usercu.UserName != Users && Users != "admin"))
            {
                return BadRequest();
            }
            usercu.Password = Pwm;
            db.Entry(usercu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(usercu.Id))
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

        // POST: api/createuser
        [Authorize(Roles = "admin")]
        [Route("api/createuser")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user1 = db.Users
            .FirstOrDefault(u => u.UserName == user.UserName);
            if (user1 != null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            var useradd = new User()
            {
                UserName = user.UserName,
                Password = user.Password,
                Image = user.Image,
                Birthday = user.Birthday,
                Sex = user.Sex,
                Address = user.Address,
                Phonenumber = user.Phonenumber,
                Email = user.Email,
                TypeId = user.TypeId
            };
            db.Users.Add(useradd);
            db.SaveChanges();
            return Created("Thanh cong", useradd);
        }

        // DELETE: api/Users/5
        //Xoa user theo ID -> chi admin co quyen nay
        [Authorize(Roles = "admin")]
        [Route("api/deleteuser/{id}")]
        [ResponseType(typeof(User))]

        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}