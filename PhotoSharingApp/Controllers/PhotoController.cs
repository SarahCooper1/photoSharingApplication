﻿using PhotoSharingApp.Controllers;
using PhotoSharingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PhotoSharingApp.Controller
{
    [HandleError(View = "Error")]
    [ValueReporter]
    public class PhotoController : System.Web.Mvc.Controller
    {
        private IPhotoSharingContext context;

        public PhotoController()
        {
            PhotoSharingContext photoSharingContext = new PhotoSharingContext();
            context = photoSharingContext;
        }


        public PhotoController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        // GET: Photo
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "none")]
        public ActionResult Index()
        {

            return View("Index");

        }

        public ActionResult Display(int id)
        {

            Photo verif = context.FindPhotoById(id);
            if (verif != null)
                return View("Display", verif);
            else
                return HttpNotFound();
        }

        public ActionResult DisplayByTitle(string title)
        {
            Photo photo = context.FindPhotoByTitle(title);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

        public ActionResult Create()
        {
            Photo photo = new Photo();
            photo.CreateDate = DateTime.Now;
            return View("Create", photo);
        }
        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                    context.Add<Photo>(photo);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create", photo);
            }
        }

        public ActionResult Delete(int id)
        {
            Photo verif = context.FindPhotoById(id);
            if (verif == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("Delete", verif);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo verif = context.FindPhotoById(id);
            context.Delete<Photo>(verif);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "id")]
        public FileContentResult GetImage(int id)
        {
            Photo verif = context.FindPhotoById(id);
            if (verif != null)
            {

                return (new FileContentResult(verif.PhotoFile, verif.ImageMimeType));
            }
            else
            {
                return null;
            }
        }

        public ActionResult SlideShow()
        {
            return View("SlideShow", context.Photos.ToList());
        }
        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos = new List<Photo>();
            if (number == 0)
            {
                photos = context.Photos.ToList();
            }
            else
            {
                photos = (from p in context.Photos
                          orderby p.CreateDate descending
                          select p).Take(number).ToList();
            }
            return PartialView("_PhotoGallery", photos);
        }
    }

}