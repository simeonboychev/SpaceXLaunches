using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXproj.Models
{
    public class RocketX
    {
        [JsonProperty]
        public string Rocket_id { get; set; }
        [JsonProperty]
        public string Rocket_name { get; set; }
        [JsonProperty]
        public string Rocket_type { get; set; }
    }
}
