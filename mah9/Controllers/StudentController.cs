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
    public class StudentController : Controller
    {
        // GET: Student
        // GET: Students
        private ApplicationUserManager _userManager;
        ApplicationDbContext context;

        public StudentController()
        {
            context = new ApplicationDbContext();
        }

        public StudentController(ApplicationUserManager userManager)
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

            //

            var u = (from user in context.Users
                     select new
                     {
                         UserId = user.Id,
                         Username = user.FullName,
                         Email = user.Email,
                         SeconID = user.SeconID,
                         Password = user.Password,



                         PhoneNumber = user.PhoneNumber,
                         Class = user.Class,

                         Role = (from userRole in user.Roles

                                 join role in context.Roles on userRole.RoleId

                                 equals role.Id

                                 where role.Name != "1st Grade"
                                 where role.Name != "2nd Grade"
                                 where role.Name != "3rd Grade"
                                 where role.Name != "4th Grade"
                                 where role.Name != "5th Grade"
                                 where role.Name != "6th Grade"
                                 where role.Name != "7th Grade"
                                 where role.Name != "8th Grade"
                                 where role.Name != "9th Grade"
                                 where role.Name != "10th Grade"
                                 where role.Name != "Awl Thanawi"
                                 where role.Name != "Thani Thanawi"
                                 where role.Name != "Collage"
                                 where role.Name != "Student"



                                 select role.Name).ToList()
                     }).ToList().Select(p => new StudentModel()

                     {
                         // UserId = p.UserId,
                         Username = p.Username,
                         Email = p.Email,
                         SeconID = p.SeconID,

                         PhoneNumber = p.PhoneNumber,
                         Class = p.Class,
                         //
                         Password = p.Password,
                         

                         Role = string.Join(" | ", p.Role)

                     });
            
            
            return View(u);

        }

        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {

            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");
            //var r = context.Users.FirstOrDefault(x => x.SeconID == id);
            var r = context.Users.SingleOrDefault(x => x.SeconID == id);


            var s = new StudentModel
            {
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                FullName = r.FullName,
                Class = r.Class,
                Password = r.Password,
                SeconID = r.SeconID,
                UserId = r.Id,
                PasswordHash = r.PasswordHash,
                SecurityStamp = r.SecurityStamp,
                PhoneNumberConfirmed = r.PhoneNumberConfirmed,
                twofac = r.TwoFactorEnabled,
                lockout = r.LockoutEnabled,
                lockoutend = r.LockoutEndDateUtc.ToString(),
                AcecssFailed = r.AccessFailedCount



            };
            return View(s);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StudentModel r)
        {

            var s = context.Users.SingleOrDefault(x => x.SeconID == r.SeconID);
            string oldclass = s.Class;
            s.Email = r.Email;
            s.PhoneNumber = r.PhoneNumber;
            s.FullName = r.FullName;
            s.Class = r.Class;
            s.Password = r.Password;
            s.SeconID = r.SeconID;
            

            if (s.Class != null) 
            {
                context.Entry(s).State = EntityState.Modified;
                if (oldclass != null && s.Class!=null) 
                {
                    await this.UserManager.RemoveFromRoleAsync(s.Id, oldclass);
                    await this.UserManager.AddToRoleAsync(s.Id, s.Class);
                }
                
                
                context.SaveChanges();
            }
           
            //  var eyad1 = r.fuck;
            /*  student.Email = r.Email;
                   student.PhoneNumber = r.PhoneNumber;
                   student.FullName = r.FullName;
                  student.Class = r.Class;
                   student.Password = r.Password;
                   student.SeconID = r.SeconID;
                   student.UserId = r.Id;
                   student.PasswordHash = r.PasswordHash;
                   student.SecurityStamp = r.SecurityStamp;
                   student.PhoneNumberConfirmed = r.PhoneNumberConfirmed;
                   student.twofac = r.TwoFactorEnabled;
                   student.lockout = r.LockoutEnabled;
                   student.lockoutend = r.LockoutEndDateUtc.ToString();
              student.AcecssFailed = r.AccessFailedCount;
              context.Users.Remove(r);
              context.Users.Add(student);

      */
            //  r.FullName = s.FullName;
            //  r.Email = s.Email+"sadsad";

            // context.Users.Remove(r);
            // 
            // context.Users.
            //



            return RedirectToAction("Index");

        }

        //Get
        public async Task<ActionResult> Delete(int id)
        {

            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");

            var r = context.Users.SingleOrDefault(x => x.SeconID == id);
            var s = new StudentModel
            {
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                FullName = r.FullName,
                Class = r.Class,
                Password = r.Password,
                SeconID = r.SeconID,
                UserId = r.Id,
                PasswordHash = r.PasswordHash,
                SecurityStamp = r.SecurityStamp,
                PhoneNumberConfirmed = r.PhoneNumberConfirmed,
                twofac = r.TwoFactorEnabled,
                lockout = r.LockoutEnabled,
                lockoutend = r.LockoutEndDateUtc.ToString(),
                AcecssFailed = r.AccessFailedCount



            };

            bool flag = false;
            String Roles = "|";

            var roless = context.Roles.ToList();
            foreach (var i in roless)
            {
                flag = await this.UserManager.IsInRoleAsync(r.Id, i.Name);
                if (flag == true)
                {
                    if (i.Name != r.Class && i.Name != "Student")
                    {
                        Roles = Roles + i.Name + "|";
                        flag = false;
                    }


                }

            }
            s.Role = Roles;

            if (await this.UserManager.IsInRoleAsync(r.Id, "Student") == true)
            {
                s.userroless = "Student";
            }



            return View(s);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(StudentModel r)
        {

            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");
            var s = context.Users.SingleOrDefault(x => x.SeconID == r.SeconID);

            s.Email = r.Email;
            s.PhoneNumber = r.PhoneNumber;
            s.FullName = r.FullName;
            s.Class = r.Class;
            s.Password = r.Password;
            s.SeconID = r.SeconID;
            string userid = s.Id;

            if (s.Class != null)
            {
                await this.UserManager.RemoveFromRoleAsync(userid, r.Class);
            }


            context.Users.Remove(s);
            context.SaveChanges();


            return RedirectToAction("Index");
        }


        //GET
        public async Task<ActionResult> Details(int id)
        {

            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");

            var r = context.Users.SingleOrDefault(x => x.SeconID == id);
            var s = new StudentModel
            {
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                FullName = r.FullName,
                Class = r.Class,
                Password = r.Password,
                SeconID = r.SeconID,
                UserId = r.Id,
                PasswordHash = r.PasswordHash,
                SecurityStamp = r.SecurityStamp,
                PhoneNumberConfirmed = r.PhoneNumberConfirmed,
                twofac = r.TwoFactorEnabled,
                lockout = r.LockoutEnabled,
                lockoutend = r.LockoutEndDateUtc.ToString(),
                AcecssFailed = r.AccessFailedCount,
                Date = r.Date



            };
            bool flag = false;
            String Roles = "|";
            
            var roless = context.Roles.ToList();
            foreach (var i in roless)
            {
                flag = await this.UserManager.IsInRoleAsync(r.Id, i.Name);
                if (flag == true)
                {
                    if (i.Name != r.Class && i.Name != "Student")
                    {
                        Roles = Roles + i.Name + "|";
                        flag = false;
                    }
                  

                }

            }
            s.Role = Roles;

            if (await this.UserManager.IsInRoleAsync(r.Id, "Student") == true)
            {
                s.userroless = "Student";
            }
            



            return View(s);
        }

        [HttpPost]
        public ActionResult Details()
        {





            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Delsub(int id)
        {

            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            ViewBag.UserName = new SelectList(context.Users.ToList(), "UserName", "UserName");
            ViewBag.subject = new SelectList(context.Subject_.ToList(), "subject", "subject");

            var r = context.Users.SingleOrDefault(x => x.SeconID == id);
           
            var s = new StudentModel
            {
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                FullName = r.FullName,
                Class = r.Class,
                Password = r.Password,
                SeconID = r.SeconID,
                UserId = r.Id,
                PasswordHash = r.PasswordHash,
                SecurityStamp = r.SecurityStamp,
                PhoneNumberConfirmed = r.PhoneNumberConfirmed,
                twofac = r.TwoFactorEnabled,
                lockout = r.LockoutEnabled,
                lockoutend = r.LockoutEndDateUtc.ToString(),
                AcecssFailed = r.AccessFailedCount



            };
            bool flag = false;
            String Roles = " | ";
            var roless = context.Roles.ToList();
            foreach (var i in roless)
            {
                flag = await this.UserManager.IsInRoleAsync(r.Id, i.Name);
                if (flag == true)
                {
                    if (i.Name != r.Class && i.Name != "Student") 
                    {
                        Roles = Roles + i.Name + " | ";
                        flag = false;
                    }
                   
                }

            }
            s.Roles = Roles;






            return View(s);
        }

        [HttpPost]
        public async Task<ActionResult> Delsub(StudentModel r)
        {


            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");
            var s = context.Users.SingleOrDefault(x => x.SeconID == r.SeconID);


            s.Class = r.Class;

            s.SeconID = r.SeconID;
            string userid = s.Id;

            

            if (r.Subject != null)
            {
                await this.UserManager.RemoveFromRoleAsync(s.Id, r.Subject);

                
            }



            context.SaveChanges();


            return RedirectToAction("Index");




        }

        //GET
        public async Task<ActionResult> AddSub(int id)
        {
            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");
            ViewBag.subject = new SelectList(context.Subject_.ToList(), "subject", "subject");
            

            var r = context.Users.SingleOrDefault(x => x.SeconID == id);
            var s = new StudentModel
            {
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                FullName = r.FullName,
                Class = r.Class,
                Password = r.Password,
                SeconID = r.SeconID,
                UserId = r.Id,
                PasswordHash = r.PasswordHash,
                SecurityStamp = r.SecurityStamp,
                PhoneNumberConfirmed = r.PhoneNumberConfirmed,
                twofac = r.TwoFactorEnabled,
                lockout = r.LockoutEnabled,
                lockoutend = r.LockoutEndDateUtc.ToString(),
                AcecssFailed = r.AccessFailedCount
            };

            bool flag = false;
            String Roles = " | ";
            var roless = context.Roles.ToList();
            foreach (var i in roless)
            {
                flag = await this.UserManager.IsInRoleAsync(r.Id, i.Name);
                if (flag == true)
                {
                    if (i.Name != r.Class && i.Name != "Student")
                    {
                        Roles = Roles + i.Name + " | ";
                        flag = false;
                    }

                }

            }
            s.Roles = Roles;

            return View(s);
        }

                    
        [HttpPost]
        public async Task<ActionResult> AddSub(StudentModel r)
        {


            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");
            var s = context.Users.SingleOrDefault(x => x.SeconID == r.SeconID);


            s.Class = r.Class;

            s.SeconID = r.SeconID;
            string userid = s.Id;

            if (r.Subject != null)
            {
                await this.UserManager.AddToRoleAsync(s.Id, r.Subject);
            }



            context.SaveChanges();


            return RedirectToAction("Index");




        }
    }
}