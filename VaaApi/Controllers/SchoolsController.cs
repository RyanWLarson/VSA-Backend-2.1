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
    public class SchoolsController : ApiController
    {
        //API to get the list of all the schools in the database along with their IDs and acronymns
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string Get()
        {
            var query = "select SchoolID, Name, Acronym from School";
            var connection = new DBConnection();
            var results = connection.ExecuteToDT(query);
            var model = new SchoolModel
            {
                Schools = new List<School>()
            };
            foreach (DataRow row in results.Rows)
            {
                var schoolID = (int)row["SchoolID"];
                var schoolName = (string)row["Name"];
                var acronym = (string)row["Acronym"];
                model.Schools.Add(new School { schoolId = schoolID.ToString(), schoolName = schoolName, acronym = acronym });
            }
            var response = JsonConvert.SerializeObject(model);
            return response;
        }

        //Api to get the list of all the majors associated with a school ID
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string Get(int id)
        {
            var query = $"select distinct MajorSchoolBool.MajorID as MajorID, Major.Name as MajorName from MajorSchoolBool join Major on MajorSchoolBool.MajorID = Major.MajorID where MajorSchoolBool.SchoolID ={id} and MajorSchoolBool.Avaliable = 1";
            var connection = new DBConnection();
            var results = connection.ExecuteToDT(query);
            var model = new SchoolMajor
            {
                majors = new List<Major>()
            };
            foreach (DataRow row in results.Rows)
            {
                var majorID = (int)row["MajorID"];
                var majorName = (string)row["MajorName"];
                model.majors.Add(new Major { majorId = majorID.ToString(), majorName = majorName});
            }
            var response = JsonConvert.SerializeObject(model);
            return response;

        }

    }
}
