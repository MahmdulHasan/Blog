using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstWebBlog.Models
{
    public class BlogSectionModel
    {
        [Key]
        public int BlogSectionId { get; set; }
        public int BlogId { get; set; }
        public string SectionTitle { get; set; }
        public string SectionContent { get; set; }
        public string ImageUrl { get; set; }
        public virtual BlogModel Blog { get; set; }
    }
}