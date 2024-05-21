using Microsoft.AspNetCore.Mvc;
using Compass.Shared;
using Compass.Shared.Models;
using Compass.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Compass.DataAccess;
using Compass.BusinessLogic;

namespace Compass.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InternalAPI : Controller
    {
        private readonly ILogger<InternalAPI> _logger;

        IInternalBL internalBL;
        public InternalAPI(IConfiguration config, IInternalBL internalBL, ILogger<InternalAPI> logger)
        {
            this.internalBL = internalBL;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Major> MajorsGet(string? code, string? name, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: GetMajors Request with parameters {code} & {name}");
            var majorDetails = internalBL.MajorsGet(code, name);
            var majors = new List<Major>(majorDetails);
            _logger.LogInformation($"{netId}: GetMajors returns {majors.Count} rows");
            return majors;
        }

        [HttpGet]
        public MajorDetails MajorDetailsGet([BindRequired]string code, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: MajorDetailsGet Request with parameter {code}");
            var majorDetails = internalBL.MajorsGet(code);
            _logger.LogInformation($"{netId}: MajorDetailsGet returns {majorDetails.Count()} rows");
            return majorDetails.FirstOrDefault();
        }

        [HttpGet]
        public IEnumerable<Course> CoursesGet(string? code, string? name, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: CoursesGet Request with parameters {code} & {name}");
            var courseDetails = internalBL.CoursesGet(code, name);
            var courses = new List<Course>(courseDetails);
            _logger.LogInformation($"{netId}: CoursesGet returns {courses.Count} rows");
            return courses;
        }

        [HttpGet]
        public IEnumerable<Course> CoursesForMajorGet([BindRequired]string major, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: CoursesForMajorGet Request with parameter {major}");
            IEnumerable<Course> courses = internalBL.CoursesForMajorGet(major);
            _logger.LogInformation($"{netId}: CoursesForMajorGet returns {courses.Count()} rows");
            return courses;
        }

        [HttpGet]
        public IEnumerable<CourseDetails> CourseDetailsGet([BindRequired]string code, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: CourseDetailsGet Request with parameter {code}");
            var courseDetails = internalBL.CoursesGet(code);
            _logger.LogInformation($"{netId}: CourseDetailsGet returns {courseDetails.Count()} rows");
            return courseDetails;
        }

        [HttpGet]
        //public Dictionary<string, IEnumerable<string>> CriteriaGet()
        public IEnumerable<CriteriaGroup> CriteriaGet(string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: CriteriaGet Request");
            var criteriaGroup = internalBL.CriteriaGet();
            _logger.LogInformation($"{netId}: CriteriaGet returns {criteriaGroup.Count()} rows");
            return criteriaGroup;
        }

        [HttpPost]
        public IEnumerable<AllCriteria> CriteriaForMajorsGet(IEnumerable<string> majorCodes, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: CriteriaForMajorsGet Request with parameter {majorCodes.ToString}");
            var criteriaGroup = internalBL.CriteriaForMajorsGet(majorCodes);
            _logger.LogInformation($"{netId}: CriteriaForMajorsGet returns {criteriaGroup.Count()} rows");
            return criteriaGroup;
        }

        [HttpGet]
        public IEnumerable<Alumni> AlumniForMajorGet(string? major, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: AlumniForMajorGet Request with parameter {major}");
            var alumni = internalBL.AlumniForMajorGet(major);
            _logger.LogInformation($"{netId}: AlumniForMajorGet returns {alumni.Count()} rows");
            return alumni;
        }

        [HttpGet]
        public IEnumerable<Faculty> FacultyForDepartmentGet(string? dept, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: FacultyForDepartmentGet Request with parameter {dept}");
            var faculty = internalBL.FacultyForDepartmentGet(dept);
            _logger.LogInformation($"{netId}: FacultyForDepartmentGet returns {faculty.Count()} rows");
            return faculty;
        }

        [HttpGet]
        public IEnumerable<JobTitle> JobsForMajorGet(string? major, string? netId = "DefaultUser@uic.edu")
        {
            AppSettings.netId = netId;
            _logger.LogInformation($"{netId}: JobsForMajorGet Request with parameter {major}");
            var jobs = internalBL.JobsForMajorGet(major);
            _logger.LogInformation($"{netId}: JobsForMajorGet returns {jobs.Count()} rows");
            return jobs;
        }
    }
}
