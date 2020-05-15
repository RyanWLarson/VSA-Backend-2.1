using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Scheduler;
using Models;

namespace VaaApi.Controllers
{
    using System.Web.Http.Cors;
    using System.Data;
    using System.Web.Http.Results;
    using ApiCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    public class PrerequisitesController : ApiController
    {   
        //Get method to fetch the prerequisite courses for all the courses in a school major-wise
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string Get(int id)
        {   //get the list of all majors and their names for the given schoolId
            var majorQuery = $"select distinct MajorSchoolBool.MajorID as MajorID, Major.Name as MajorName from MajorSchoolBool join Major on MajorSchoolBool.MajorID = Major.MajorID where MajorSchoolBool.SchoolID={id} and MajorSchoolBool.Avaliable=1";
            var connection = new DBConnection();
            var majorResults = connection.ExecuteToDT(majorQuery);
            var results = "";
            foreach(DataRow row in majorResults.Rows)
            {
                var majorID = (int)row["MajorID"];
                var coursesQuery = $"select Course.CourseID as CourseID, Course.CourseNumber as CourseNumber from Course join AdmissionRequiredCourses on AdmissionRequiredCourses.CourseID=Course.CourseID where AdmissionRequiredCourses.MajorID={majorID}";
                var coursesResults = connection.ExecuteToDT(coursesQuery);

                foreach(DataRow courseRow in coursesResults.Rows)
                {
                    var courseID = (int)courseRow["CourseID"];
                    var prereqQuery = $"select * from Course join Prerequisite on Prerequisite.PrerequisiteCourseID=Course.CourseID where Prerequisite.CourseID={courseID}";
                    var prereqresults = connection.ExecuteToString(prereqQuery);
                    results = results + '\n' + prereqresults;
                }
                //results = results + '\n' + coursesResults;
            }          
            
            return results;
        }
    }
}
