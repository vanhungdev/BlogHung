using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Application.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Position { get; set; }
        public string Img { get; set; }
        public int Orders { get; set; }
        public int Status { get; set; }
    }
}
