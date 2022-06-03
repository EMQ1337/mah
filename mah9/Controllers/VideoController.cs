using mah9.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mah9.Controllers
{
    public class VideoController : Controller
    {

        Models.ApplicationDbContext context;
        private ApplicationDbContext db = new ApplicationDbContext();

        public VideoController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");
            // string n = "Math";
            var videos = context.Video_.ToList();
            // ViewBag.UserID = HttpContext.User.Identity.GetUserId();
            return View(videos);


        }
        //GET
        public ActionResult Watch(int id)
        {
            var r = context.Video_.SingleOrDefault(x => x.VideoID == id);
            return View(r);
        }

        [HttpPost]
        public ActionResult Watch(VideoModel videos)
        {
           

            return RedirectToAction("Index");
        }



        public ActionResult CreateVideo()
        {
            ViewBag.ClassName = new SelectList(context.Class_.ToList(), "ClassName", "ClassName");
            ViewBag.SubjectName = new SelectList(context.Subject_.ToList(), "subject", "subject");
            var videos = new VideoModel();
            return View(videos);
        }

        [HttpPost]
        public ActionResult CreateVideo(VideoModel videos)
        {
            DateTime date = DateTime.Now;
            videos.Date = date;
            context.Video_.Add(videos);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {

            var r = context.Video_.SingleOrDefault(x => x.VideoID == id);
            return View(r);
        }

        [HttpPost]
        public ActionResult Edit(int id, VideoModel model)
        {
            var v = context.Video_.FirstOrDefault(x => x.VideoID == id);
            v.VideoClass = model.VideoClass;
            v.VideoName = model.VideoName;
            v.VideoSubject = model.VideoSubject;
            v.VideoURL = model.VideoURL;
            
            context.Entry(v).State = EntityState.Modified;
            
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            var r = context.Video_.SingleOrDefault(x => x.VideoID == id);
            return View(r);
        }

        [HttpPost]
        public ActionResult Details()
        {





            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {

            var r = context.Video_.SingleOrDefault(x => x.VideoID == id);
            return View(r);
        }

        [HttpPost]
        public ActionResult Delete(int id, VideoModel model)
        {
            var r = context.Video_.SingleOrDefault(x => x.VideoID == id);

            context.Video_.Remove(r);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // [HttpPost]
        //  public ActionResult Details_()
        // {




        //   return RedirectToAction("Index");
        // }
    }
}