using Compass.Shared;
using Compass.Shared.Interfaces;
using Compass.Shared.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace Compass.DataAccess
{
    public class InternalDAL: IInternalDAL
    {
        //private string connectionString;
        private readonly ILogger<InternalDAL> _logger;

        public InternalDAL(IConfiguration config, ILogger<InternalDAL> logger)
        {
            AppSettings.SetConfig(config);
            //connectionString = GetConnectionString();
            _logger = logger;
        }

        public string GetConnectionString()
        {
            if (AppSettings.netId.ToLower().Contains("uic.edu"))
            {
                return AppSettings.GetConnectionString("CompassConnection");
            }
            else
            {
                return AppSettings.GetConnectionString("CompassConnectionUIUC");
            }
        }

        public IEnumerable<MajorDetails> MajorsGet(string? code, string? name)
        {
            IEnumerable<MajorDetails> majors = new List<MajorDetails>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_MajorsGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("code", code));
                    command.Parameters.Add(new SqlParameter("name", name));

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    majors = dataSet.Tables[0].ToList<MajorDetails>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in MajorsGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return majors ?? Enumerable.Empty<MajorDetails>();
        }

        public IEnumerable<CourseDetails> CoursesGet(string? code, string? name)
        {
            List<CourseDetails> courses = new List<CourseDetails>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_CoursesGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("code", code));
                    command.Parameters.Add(new SqlParameter("name", name));

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    courses = dataSet.Tables[0].ToList<CourseDetails>();
                }
                // To get the prerequisites only while trying to get a specific course details
                if (!String.IsNullOrEmpty(code) && courses.Count() == 1)
                {
                    courses[0].Prerequisites = CoursePrerequisitesGet(code);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in CoursesGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return courses ?? Enumerable.Empty<CourseDetails>();
        }

        public IEnumerable<string> CoursePrerequisitesGet(string course)
        {
            IEnumerable<string> prerequisites = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_CoursePrerequisitesGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("course", course));

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    prerequisites = dataSet.Tables[0].AsEnumerable().Select(row => row[0].ToString()).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in CoursesForMajorGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return prerequisites ?? Enumerable.Empty<string>();
        }

        public IEnumerable<Course> CoursesForMajorGet(string major)
        {
            IEnumerable<Course> courses = new List<CourseDetails>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_CoursesForMajorGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("major", major));

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    courses = dataSet.Tables[0].ToList<Course>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in CoursesForMajorGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return courses ?? Enumerable.Empty<Course>();
        }

        /// <summary>
        /// Getting the set of criterias using dictionary
        /// </summary>
        /// <param name="majorCodes"></param>
        /// <returns></returns>
        //public Dictionary<string, IEnumerable<string>> CriteriaGet()
        //{
        //    Dictionary<string, IEnumerable<string>> criteriaGroups = new Dictionary<string, IEnumerable<string>>()
        //    {
        //        { "PreUniversity", Enumerable.Empty<string>() },
        //        { "University", Enumerable.Empty<string>() },
        //        { "PostUniversity", Enumerable.Empty<string>() },
        //        { "StudentsInMajor", Enumerable.Empty<string>() }
        //    };

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            SqlCommand command = new SqlCommand("usp_CriteriaGet", connection);
        //            command.CommandType = System.Data.CommandType.StoredProcedure;

        //            var dataAdapter = new SqlDataAdapter(command);
        //            var dataSet = new DataSet();
        //            dataAdapter.Fill(dataSet);
        //            var criteria = dataSet.Tables[0].ToList<Criteria>();
        //            foreach (var item in criteria)
        //            {
        //                criteriaGroups[item.Table!] = criteriaGroups[item.Table].Append(item.Criterion);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return criteriaGroups;
        //}

        public IEnumerable<CriteriaGroup> CriteriaGet()
        {
            List<CriteriaGroup> criteriaGroups = new List<CriteriaGroup>();
            CriteriaGroup tempGroup;

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_CriteriaGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    var criteria = dataSet.Tables[0].ToList<Criteria>();
                    foreach (var item in criteria)
                    {
                        if(criteriaGroups.Any(x => x.TableName == item.Table))
                        {
                            tempGroup = criteriaGroups.FirstOrDefault(x =>  x.TableName == item.Table);
                            tempGroup.CriteriaList.Add(item.Criterion);
                        }
                        else
                        {
                            tempGroup = new CriteriaGroup() { TableName = item.Table, CriteriaList = new List<string> { item.Criterion } };
                            criteriaGroups.Add(tempGroup);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in CriteriaGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return criteriaGroups;
        }

        public IEnumerable<AllCriteria> CriteriaForMajorsGet(IEnumerable<string> majorCodes)
        {
            IEnumerable<AllCriteria> criteria = new List<AllCriteria>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_CriteriaForMajorsGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var majorsListParam = new SqlParameter("majorsList", SqlDbType.Structured);
                    // Convert majorCodes to DataTable
                    majorsListParam.Value = majorCodes.ToDataTable();
                    majorsListParam.TypeName = "dbo.ut_List";
                    command.Parameters.Add(majorsListParam);

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    criteria = dataSet.Tables[0].ToList<AllCriteria>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in CriteriaForMajorsGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return criteria ?? Enumerable.Empty<AllCriteria>();
        }

        public IEnumerable<Alumni> AlumniForMajorGet(string major)
        {
            IEnumerable<Alumni> alumni = new List<Alumni>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_AlumniForMajorGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("major", major));

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    alumni = dataSet.Tables[0].ToList<Alumni>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in AlumniForMajorGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return alumni ?? Enumerable.Empty<Alumni>();
        }

        public IEnumerable<Faculty> FacultyForDepartmentGet(string dept)
        {
            IEnumerable<Faculty> faculty = new List<Faculty>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_FacultyForDepartmentGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("dept", dept));

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    faculty = dataSet.Tables[0].ToList<Faculty>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in FacultyForDepartmentGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return faculty ?? Enumerable.Empty<Faculty>();
        }

        public IEnumerable<JobTitle> JobsForMajorGet(string major)
        {
            IEnumerable<JobTitle> alumni = new List<JobTitle>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_JobsForMajorGet", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("major", major));

                    var dataAdapter = new SqlDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    alumni = dataSet.Tables[0].ToList<JobTitle>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{AppSettings.netId}: Error in JobsForMajorGet DAL - {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return alumni ?? Enumerable.Empty<JobTitle>();
        }
    }
}