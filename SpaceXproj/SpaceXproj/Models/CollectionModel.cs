using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXproj.Models
{
    public class CollectionModel
    {
        public IEnumerable<SpaceX> Collection { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }

    }
}
