﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoSharingApp.Models
{
    public class PhotoSharingInitializer : DropCreateDatabaseAlways<PhotoSharingContext>
    {


        protected override void Seed(PhotoSharingContext context)
        {
            List<Photo> photos = new List<Photo>();
            Photo photo = new Photo();
            photo.Title = "Test Photo";
            photo.Description = "test";
            photo.Owner = "NaokiSato";
            photo.PhotoFile = System.IO.File.ReadAllBytes("C:/Users/Wafa/Desktop/Desktop (2)/Photo_sharing/photoSharingApplication/PhotoSharingApp/Images/image.jpg");
            photo.ImageMimeType = "image/jpeg";
            photo.CreateDate = DateTime.Now;
            photos.Add(photo);
            foreach (var p in photos)
            {
                context.Photos.Add(p);
            }
            context.SaveChanges();


            List<Comment> comments = new List<Comment>();
            Comment comment = new Comment();
            comment.PhotoID = 1;
            comment.UserName = "NaokiSato";
            comment.Subject = "Test Comment";
            comment.Body = "This comment should appear in photo 1";
            comments.Add(comment);
            foreach (var c in comments)
            {
                context.Comments.Add(c);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
   
}