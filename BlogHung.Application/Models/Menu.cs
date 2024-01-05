using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHung.Application.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int ParentID { get; set; }
        public int Orders { get; set; }
        public string Position { get; set; }
        public int Status { get; set; }
    }
}
