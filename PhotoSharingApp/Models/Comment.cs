using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoSharingApp.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public String UserName { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public int PhotoID { get; set; }
        public virtual  Photo Photo { get; set; }
    }
}