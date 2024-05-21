using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Models
{
    public class AllCriteria: MajorDetails
    {
        public string Major { get; set; }

        #region StudentsInMajor
        //public double Freshman { get; set; }
        //public double Transfer { get; set; }
        //public double White { get; set; }
        //public double AfricanAmerican { get; set; }
        //public double AmericanIndian { get; set; }
        //public double Asian { get; set; }
        //public double OtherRaces { get; set; }
        //public double Male { get; set; }
        //public double Female { get; set; }
        //public double OtherGenders { get; set; }
        //public int Total { get; set; }
        //public double AverageGPA { get; set; }
        //public int GraduatedIn5years { get; set; }
        //public int AverageSalary { get; set; }
        //public double RetensionYear1 { get; set; }
        //public double RetensionYear2 { get; set; }
        //public double RetensionYear3 { get; set; }
        //public double RetensionYear4 { get; set; }
        //public double FreshmanRetension { get; set; }
        //public double TransferRetension { get; set; }
        //public double AverageGPAFreshman { get; set; }
        //public double AverageGPATransfer { get; set; }
        //public double UGResearch { get; set; }
        //public double GPIPEligible { get; set; }
        //public double GPIPInterns { get; set; }
        //public double GraduatedWithJobs { get; set; }
        //public double JoinedGraduateSchool { get; set; }
        //public string StudentsTo1Faculty { get; set; }
        #endregion

        #region PreUniversity
        public string HighSchoolPrep { get; set; }
        public string ClassTypes { get; set; }
        #endregion

        #region University
        public string SampleCurriculam { get; set; }
        public int DegreesAwarded { get; set; }
        public int DegreesAwardedBS { get; set; }
        public int DegreesAwardedMS { get; set; }
        public int DegreesAwardedPhD { get; set; }
        public double White { get; set; }
        public double AfricanAmerican { get; set; }
        public double AmericanIndian { get; set; }
        public double Asian { get; set; }
        public double OtherRaces { get; set; }
        public double AcceptanceRate { get; set; }
        public double AverageGPA { get; set; }
        public string StudentsTo1Faculty { get; set; }
        public double GraduationRate4Year { get; set; }
        public double GraduationRate6Year { get; set; }
        public double RetentionRate { get; set; }
        public double AverageClassSize { get; set; }
        public double PlacementRate { get; set; }
        public double CostOfAttendance { get; set; }
        public double TuitionInState { get; set; }
        public double TuitionOutOfState { get; set; }
        public double BoardCosts { get; set; }
        public string FinancialAid { get; set; }
        public string Safety { get; set; }
        #endregion

        #region PostUniversity
        public int MedianSalaryPerYear { get; set; }
        public double MedianSalaryPerHour { get; set; }
        public string Requirements { get; set; }
        public string WorkExperience { get; set; }
        public int JobsCount { get; set; }
        public int CurrentWorkforce { get; set; }
        public string AverageAge { get; set; }
        public double ExpectedGrowth { get; set; }
        public string WorkEnvironment { get; set; }
        public string WorkSchedule { get; set; }
        public int DegreesAwards { get; set; }
        public double DegreesGrowth { get; set; }
        #endregion
    }
}
