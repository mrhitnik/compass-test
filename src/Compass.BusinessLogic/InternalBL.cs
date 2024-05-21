using Compass.DataAccess;
using Compass.Shared.Interfaces;
using Compass.Shared.Models;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace Compass.BusinessLogic
{
    public class InternalBL: IInternalBL
    {
        IInternalDAL internalDAL;
        private readonly ILogger<InternalBL> _logger;

        public InternalBL(IInternalDAL internalDAL, ILogger<InternalBL> logger)
        {
            this.internalDAL = internalDAL;
            _logger = logger;
        }

        public IEnumerable<MajorDetails> MajorsGet(string? code, string? name)
        {
            IEnumerable<MajorDetails> majors = internalDAL.MajorsGet(code, name);
            return majors;
        }

        public IEnumerable<CourseDetails> CoursesGet(string? code, string? name)
        {
            IEnumerable<CourseDetails> courseDetails = internalDAL.CoursesGet(code, name);
            return courseDetails;
        }

        public IEnumerable<Course> CoursesForMajorGet(string major)
        {
            IEnumerable<Course> courses = internalDAL.CoursesForMajorGet(major);
            return courses;
        }

        //public Dictionary<string, IEnumerable<string>> CriteriaGet()
        public IEnumerable<CriteriaGroup> CriteriaGet()
        {
            var criteriaGroup = internalDAL.CriteriaGet();
            return criteriaGroup;
        }

        public IEnumerable<AllCriteria> CriteriaForMajorsGet(IEnumerable<string> majorCodes)
        {
            var criteria = internalDAL.CriteriaForMajorsGet(majorCodes);
            return criteria;
        }

        public IEnumerable<Alumni> AlumniForMajorGet(string major)
        {
            var alumni = internalDAL.AlumniForMajorGet(major);
            return alumni;
        }

        public IEnumerable<Faculty> FacultyForDepartmentGet(string dept)
        {
            var faculty = internalDAL.FacultyForDepartmentGet(dept);
            return faculty;
        }

        public IEnumerable<JobTitle> JobsForMajorGet(string major)
        {
            var jobs = internalDAL.JobsForMajorGet(major);
            return jobs;
        }
    }
}