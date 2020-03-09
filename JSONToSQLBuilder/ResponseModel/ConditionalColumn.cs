using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JSONToSQLBuilder
{
    public class ConditionalColumn
    {
        public string FieldName { get; set; }
        [JsonProperty("operator")]
        public string OperatorName { get; set; }
        public string FieldValue { get; set; }
        [JsonProperty("fieldType")]
        public string FieldType { get; set; }
    }
}
