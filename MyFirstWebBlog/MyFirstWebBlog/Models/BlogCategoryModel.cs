using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFirstWebBlog.Models
{
    public class BlogCategoryModel
    {
        [Key]
        [Column(Order = 1)]
        public int BlogId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CategoryId { get; set; }
        public virtual BlogModel Blog { get; set; }
        public virtual CategoryModel Category { get; set; }
    }
}