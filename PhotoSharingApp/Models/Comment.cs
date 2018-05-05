using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoSharingApp.Models
{
    public class Comment
    {
        private int CommentID { get; set; }
        private String UserName { get; set; }
        private String Subject { get; set; }
        private String Body { get; set; }
        private int PhotoID { get; set; }
        public virtual  Photo Photo { get; set; }
    }
}