using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Application.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int Status { get; set; }
    }
}
