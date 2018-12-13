using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstWebBlog.Models
{
    public class BlogModel
    {
        [Key]
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string Author { get; set; }
        public bool Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual ICollection<BlogSectionModel> BlogSections { get; set; }

    }
}