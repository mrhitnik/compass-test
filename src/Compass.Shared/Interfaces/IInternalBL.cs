using Compass.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Interfaces
{
    public interface IInternalBL
    {
        public IEnumerable<MajorDetails> MajorsGet(string? code = null, string? name = null);
        public IEnumerable<CourseDetails> CoursesGet(string? code = null, string? name = null);
        public IEnumerable<Course> CoursesForMajorGet(string major);
        public IEnumerable<CriteriaGroup> CriteriaGet();
        //public Dictionary<string, IEnumerable<string>> CriteriaGet();
        public IEnumerable<AllCriteria> CriteriaForMajorsGet(IEnumerable<string> majorCodes);
        public IEnumerable<Alumni> AlumniForMajorGet(string major);
        public IEnumerable<Faculty> FacultyForDepartmentGet(string dept);
        public IEnumerable<JobTitle> JobsForMajorGet(string major);
    }
}
