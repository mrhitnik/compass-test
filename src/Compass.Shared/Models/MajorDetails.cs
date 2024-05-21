using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Models
{
    public class MajorDetails: Major
    {
        public double CurriculamFlexibility { get; set; }
        public int MinimumCredit { get; set; }
        public int MinMathCourses { get; set; }
        public int MinPhysicsCourses { get; set; }
        public int MinChemistryCourses { get; set; }
        public int MinGeneralCourses { get; set; }
        public int MinElectiveCourses { get; set; }
        public bool GraduateSchoolAvailable { get; set; }
        public int TuitionInState { get; set; }
        public int TuitionOutOfState { get; set; }
        public int LabHours { get; set; }
    }
}
