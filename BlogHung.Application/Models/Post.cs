using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Application.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int? TopID { get; set; }
        public string Title { get; set; }
        public string DecriptionShort { get; set; }
        public string Slug { get; set; }
        public string Detail { get; set; }
        public string Img { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
