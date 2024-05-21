using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Models
{
    public class CourseDetails: Course
    {
        public string Major { get; set; }
        public int Hours { get; set; }
        public double TotalA { get; set; }
        public double TotalB { get; set; }
        public double TotalC { get; set; }
        public double TotalD { get; set; }
        public double TotalF { get; set; }
        public double TotalW { get; set; }
        public int TotalStudents { get; set; }
        public double AverageClassSize { get; set; }
        public string RecommendedSem { get; set; }
        public IEnumerable<string> Prerequisites { get; set; }
    }
}
