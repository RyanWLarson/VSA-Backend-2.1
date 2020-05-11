using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using Scheduler;

namespace VaaApi.Controllers
{
    using System.Web.Http.Cors;
    using System.Data;
    using System.Web.Http.Results;
    using ApiCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    public class TestController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string Get(int id)
        {
            var query = "select *"+
                        $" from ParameterSet JOIN GeneratedPlan on GeneratedPlan.ParameterSetID = ParameterSet.ParameterSetID where GeneratedPlanID = {id} for JSON AUTO";
            var connection = new DBConnection();
            var results = connection.ExecuteToString(query);
           
            if(results.Equals("[]"))
            {
                return results;
            }

            var preferencesObj = new Scheduler.Preferences(results);

            var model = new ParameterSetObject
            {
                Id = preferencesObj.getParamId(),
                CreditsPerQuarter = preferencesObj.getCreditsPerQuarter(),
                MaxNumberOfQuarters = preferencesObj.getQuarters(),
                NumberCoreCoursesPerQuarter = preferencesObj.getCoreCredits(),
                SummerPreference = preferencesObj.getSummerPref(),
            };
            int majorID = preferencesObj.getMajor();
            int schoolID = preferencesObj.getSchool();
            int jobTypeID = preferencesObj.getJobType();
            int timePreferenceID = preferencesObj.getTimePreference();
            int quarterPreferenceID = preferencesObj.getQuarterPreference();
            int enrollmentID = preferencesObj.getEnrollmentID();
            model.Major = connection.ExecuteToString($"select Name from Major where MajorID = {majorID}");
            model.SchoolName = connection.ExecuteToString($"select Name from School where SchoolID = {schoolID}");
            model.JobType = connection.ExecuteToString($"select JobType from JobType where JobTypeID = {jobTypeID}");
            model.TimePreference = connection.ExecuteToString($"select TimePeriod from TimePreference where TimePreferenceID = {timePreferenceID}");
            model.QuarterPreference = connection.ExecuteToString($"select Quarter from Quarter where QuarterID= {quarterPreferenceID}");
            model.EnrollmentType = connection.ExecuteToString($"select EnrollmentDescription from EnrollmentType where EnrollmentTypeID= {enrollmentID}");
            var response = JsonConvert.SerializeObject(model);
            
            return response;
        }
    }
}
