﻿using PhotoSharingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoSharingApp.Controller
{
    public class PhotoController : System.Web.Mvc.Controller

    {
        // GET: Photo
        public ActionResult Index()
        {
            var photo = new Photo();
            return View(photo);
        }
    }
}