using mah9.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace mah9.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        // GET: Subjects
        ApplicationDbContext context;
        private ApplicationUserManager _userManager;

        public SubjectController()
        {
            context = new ApplicationDbContext();
        }

        public SubjectController(ApplicationUserManager userManager)
        {
            UserManager = userManager;

        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var subjects = context.Subject_.ToList();
            return View(subjects);
        }

        public ActionResult CreateSubject()
        {
            var subjects = new SubjectModel();
            return View(subjects);
        }

        [HttpPost]
        public ActionResult CreateSubject(SubjectModel subjects, IdentityRole Role)
        {
            subjects.Oldsubject = subjects.subject;
            DateTime date = DateTime.Now;
            subjects.Date = date;
            context.Subject_.Add(subjects);
            context.SaveChanges();

            Role.Name = subjects.subject;
            context.Roles.Add(Role);

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        //GET
        public ActionResult Edit(int id)
        {
            SubjectModel model = context.Subject_.SingleOrDefault(x => x.subjectID == id);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SubjectModel model)
        {
            var v = context.Subject_.FirstOrDefault(x => x.subjectID == id);
            v.subject = model.subject;
            v.Oldsubject = model.subject;
             var r = context.Roles.FirstOrDefault(x => x.Name == model.Oldsubject);
            r.Name = model.subject;
            context.Entry(r).State = EntityState.Modified;
            context.Entry(v).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            SubjectModel model = context.Subject_.SingleOrDefault(x => x.subjectID == id);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, SubjectModel model)
        {
            var v = context.Subject_.FirstOrDefault(x => x.subjectID == id);
            v.subject = model.subject;
            v.Oldsubject = model.subject;

            var r = context.Roles.FirstOrDefault(x => x.Name == model.subject);

            var users = context.Users.ToList();
            foreach (var i in users) 
            {

                if (await this.UserManager.IsInRoleAsync(i.Id, model.subject) == true)
                {
                    await this.UserManager.RemoveFromRoleAsync(i.Id, model.subject);
                }
            }
            context.Subject_.Remove(v);
            context.Roles.Remove(r);

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}