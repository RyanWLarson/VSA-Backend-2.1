﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var welcome = ScheduleModel.FromJson(jsonString);

// This class was copied from the VaaApi coreapi created by Rigdha.
// Feel free to alter the fields of this class to better store important information that
// we need.

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ScheduleModel
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public string DateModified { get; set; }

        [JsonProperty("academic_year")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AcademicYear { get; set; }

        [JsonProperty("student_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StudentId { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("quarters")]
        public List<Quarter> Quarters { get; set; }
        [JsonIgnore]
        public Preferences PreferenceSet { get; set; }

        public static ScheduleModel ConvertFromDatabase(DataTable results, int id, Models.Preferences preferences)
        {
            var model = new ScheduleModel
            {
                Quarters = new List<Quarter>(),
                Id = id
            };
            foreach (DataRow row in results.Rows)
            {
                var courseName = (string)row["CourseNumber"];
                var quarter = (int)row["QuarterID"];
                var year = (int)row["YearID"];
                var courseId = (int)row["CourseId"];
                var departmentId = (int) row["DepartmentID"];
                var quarterItem = model.Quarters.FirstOrDefault(s => s.Id == $"{year}{quarter}" && s.Year == year);
                if (quarterItem == null)
                {
                    model.Quarters.Add(new Quarter() { Id = $"{year}{quarter}", Title = $"{year}-{quarter}", Year = year });
                    quarterItem = model.Quarters.First(s => s.Id == $"{year}{quarter}" && s.Year == year);
                }

                if (quarterItem.Courses == null)
                {
                    quarterItem.Courses = new List<Course>();
                }
                quarterItem.Courses.Add(new Course() { Description = courseName + $"({courseId})", Id = courseId.ToString(), Title = courseName + $"({courseId})", DepartmentID = departmentId});
            }

            model.PreferenceSet = preferences;
            return model;
        }    
    }

    public partial class Metadata
    {
        public string Rating { get; set; }
    }

    public partial class Quarter : IComparable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("year")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Year { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("courses")]
        public List<Course> Courses { get; set; }

        public int QuarterKey { get; set; }

        // Allows Quarters to be sorted chronologically
        public int CompareTo(object other)
        {
            Quarter q = other as Quarter;
            if (this.Year < q.Year)
            {
                return -1;
            }
            else if (this.Year > q.Year)
            {
                return 1;
            }

            var otherQuarterStr = q.Title.Split('-')[1];
            var thisQuarterStr = this.Title.Split('-')[1];
            var thisQuarter = Int32.Parse(thisQuarterStr);
            var otherQuarter = Int32.Parse(otherQuarterStr);

            if (thisQuarter < otherQuarter)
            {
                return -1;
            }
            else if (thisQuarter > otherQuarter)
            {
                return 1;
            }

            return 0;
        }
    }

    public partial class Course
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("departmentID")]
        public int DepartmentID { get; set; }
        [JsonProperty("timeOfDay")]
        public string TimeOfDay { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}