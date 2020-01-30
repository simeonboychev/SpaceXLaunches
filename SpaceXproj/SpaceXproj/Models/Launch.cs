using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXproj.Models
{
    public class Launch
    {
        [JsonProperty]
        public string Site_id { get; set; }
        [JsonProperty]
        public string Site_name { get; set; }
        [JsonProperty]
        public string Site_name_long { get; set; }
    }
}
