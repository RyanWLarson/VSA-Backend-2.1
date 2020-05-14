﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaaApi
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class ParameterSetObject
    {
        [JsonProperty("id")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public int Id { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public string DateModified { get; set; }

        [JsonProperty("school")]
        public string SchoolName { get; set; }

        [JsonProperty("major")]
        public string Major { get; set; }

        [JsonProperty("job_type")]
        public string JobType { get; set; }

        [JsonProperty("time_preference")]
        public string TimePreference { get; set; }

        [JsonProperty("quarter_preference")]
        public string QuarterPreference { get; set; }

        [JsonProperty("number_core_courses_per_quarter")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public int NumberCoreCoursesPerQuarter { get; set; }

        [JsonProperty("maximum_number_quarters")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public int MaxNumberOfQuarters { get; set; }

        [JsonProperty("number_credits_per_quarter")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public int CreditsPerQuarter { get; set; }

        [JsonProperty("summer_preference")]
        public string SummerPreference { get; set; }

        [JsonProperty("enrollment_type_description")]
        public string EnrollmentType { get; set; }

        [JsonProperty("preferred_english_start")]
        public string EnglishStart { get; set; }

        [JsonProperty("preferred_math_start")]
        public string MathStart { get; set; }
    }
}


// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var welcome = ScheduleModel.FromJson(jsonString);

//namespace VaaApi
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Globalization;
//    using Newtonsoft.Json;
//    using Newtonsoft.Json.Converters;

//    public partial class ScheduleModel
//    {
//        [JsonProperty("id")]
//        [JsonConverter(typeof(ParseStringConverter))]
//        public long Id { get; set; }

//        [JsonProperty("date_created")]
//        public string DateCreated { get; set; }

//        [JsonProperty("date_modified")]
//        public string DateModified { get; set; }

//        [JsonProperty("academic_year")]
//        [JsonConverter(typeof(ParseStringConverter))]
//        public long AcademicYear { get; set; }

//        [JsonProperty("student_id")]
//        [JsonConverter(typeof(ParseStringConverter))]
//        public long StudentId { get; set; }

//        [JsonProperty("metadata")]
//        public Metadata Metadata { get; set; }

//        [JsonProperty("quarters")]
//        public List<Quarter> Quarters { get; set; }
//    }

//    public partial class Metadata
//    {
//        public string Rating { get; set; }
//    }

//    public partial class Quarter
//    {
//        [JsonProperty("id")]
//        public string Id { get; set; }

//        [JsonProperty("year")]
//        [JsonConverter(typeof(ParseStringConverter))]
//        public long Year { get; set; }

//        [JsonProperty("title")]
//        public string Title { get; set; }

//        [JsonProperty("courses")]
//        public List<Course> Courses { get; set; }
//    }

//    public partial class Course
//    {
//        [JsonProperty("id")]
//        public string Id { get; set; }

//        [JsonProperty("title")]
//        public string Title { get; set; }

//        [JsonProperty("description")]
//        public string Description { get; set; }
//    }

//    internal static class Converter
//    {
//        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//        {
//            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//            DateParseHandling = DateParseHandling.None,
//            Converters =
//            {
//                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
//            },
//        };
//    }

//    internal class ParseStringConverter : JsonConverter
//    {
//        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

//        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//        {
//            if (reader.TokenType == JsonToken.Null) return null;
//            var value = serializer.Deserialize<string>(reader);
//            long l;
//            if (Int64.TryParse(value, out l))
//            {
//                return l;
//            }
//            throw new Exception("Cannot unmarshal type long");
//        }

//        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//        {
//            if (untypedValue == null)
//            {
//                serializer.Serialize(writer, null);
//                return;
//            }
//            var value = (long)untypedValue;
//            serializer.Serialize(writer, value.ToString());
//            return;
//        }

//        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
//    }
//}
