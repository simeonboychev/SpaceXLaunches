using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXproj.Models
{
    public class SpaceX
    {
        private string date;
        [JsonProperty]
        public string Flight_number { get; set; }
        [JsonProperty]
        public string Mission_name { get; set; }
        [JsonProperty]
        public string Details { get; set; }
        [JsonProperty]
        public string Launch_date_utc
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value.Substring(0, 10);
            }
        }
        [JsonProperty]
        public RocketX Rocket { get; set; }
        [JsonProperty]
        public Launch Launch_site { get; set; }
        public Links Links { get; set; }
    }
}
