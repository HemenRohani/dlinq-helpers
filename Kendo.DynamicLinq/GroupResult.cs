using Newtonsoft.Json;

namespace Kendo.DynamicLinq
{
    public class GroupResult
    {
        //for more info check http://docs.telerik.com/kendo-ui/api/javascript/data/datasource#configuration-schema.groups

        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }

        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "aggregates")]
        public object Aggregates { get; set; }

        [JsonProperty(PropertyName = "items")]
        public dynamic Items { get; set; }

        [JsonProperty(PropertyName = "hasSubgroups")]
        public bool HasSubgroups { get; set; }

    }
}