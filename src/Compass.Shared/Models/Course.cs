using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Models
{
    public class Course
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Summary { get; set; }
        public int Credits { get; set; }
    }
}
