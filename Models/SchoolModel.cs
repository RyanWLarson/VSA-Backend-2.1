//This model is used to denote a school object, major object
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
        using System;
        using System.Collections.Generic;
        using System.Data;
        using System.Globalization;
        using System.Linq;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;
    public partial class SchoolModel
    {
        [JsonProperty("schools")]
        public List<School> Schools { get; set; }
    }
    public partial class School
    {
        [JsonProperty("id")]
        public string schoolId { get; set; }

        [JsonProperty("SchoolName")]
        public string schoolName { get; set; }

        [JsonProperty("Acronym")]
        public string acronym { get; set; }

    }
    public partial class SchoolMajor
    {
        [JsonProperty("schools")]
        public List<Major> majors { get; set; }
    }
    public partial class Major
    {
        [JsonProperty("id")]
        public string majorId { get; set; }

        [JsonProperty("MajorName")]
        public string majorName { get; set; }
    }

}
