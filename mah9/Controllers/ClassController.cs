using mah9.Models;
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
    public class ClassController : Controller
    {

        ApplicationDbContext context;
        private ApplicationUserManager _userManager;

        public ClassController()
        {
            context = new ApplicationDbContext();
        }

        public ClassController(ApplicationUserManager userManager)
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
            var Classes = context.Class_.ToList();
            return View(Classes);
        }

        public ActionResult CreateClass()
        {
            var class1 = new ClassModel();
            return View(class1);
        }

        [HttpPost]
        public ActionResult CreateClass(ClassModel class1, IdentityRole Role)
        {
            class1.OldClassName = class1.ClassName;
            DateTime date = DateTime.Now;
            class1.Date = date;
            context.Class_.Add(class1);
            context.SaveChanges();

            Role.Name = class1.ClassName;
            context.Roles.Add(Role);

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        //GET
        public ActionResult Edit(int id)
        {
            ClassModel model = context.Class_.SingleOrDefault(x => x.ClassID == id);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ClassModel model)
        {
            var v = context.Class_.FirstOrDefault(x => x.ClassID == id);
            v.ClassName = model.ClassName;
            v.OldClassName= model.ClassName;
            var r = context.Roles.FirstOrDefault(x => x.Name == model.OldClassName);
            r.Name = model.ClassName;

            var users = context.Users.ToList();
            foreach (var i in users)
            {

                if (await this.UserManager.IsInRoleAsync(i.Id, model.OldClassName) == true)
                {
                    
                    var z = context.Users.FirstOrDefault(x => x.Id == i.Id);
                    z.Class = model.ClassName;
                    context.Entry(z).State = EntityState.Modified;
                }
            }

            context.Entry(r).State = EntityState.Modified;
            context.Entry(v).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            ClassModel model = context.Class_.SingleOrDefault(x => x.ClassID == id);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, ClassModel model)
        {
            var v = context.Class_.FirstOrDefault(x => x.ClassID == id) ;
            v.ClassName = model.ClassName;
            v.OldClassName = model.ClassName;

            var r = context.Roles.FirstOrDefault(x => x.Name == model.ClassName);

            var users = context.Users.ToList();
            foreach (var i in users)
            {

                if (await this.UserManager.IsInRoleAsync(i.Id, model.ClassName) == true)
                {
                    await this.UserManager.RemoveFromRoleAsync(i.Id, model.ClassName);
                    var z = context.Users.FirstOrDefault(x => x.Id == i.Id);
                    z.Class = "-NULL-";
                    context.Entry(z).State = EntityState.Modified;
                }
            }
            context.Class_.Remove(v);
            context.Roles.Remove(r);

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}