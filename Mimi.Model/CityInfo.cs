using System;
using System.Collections.Generic;
using System.Text;

namespace Mimi.Model
{
    public class CityInfo
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public List<CityPointOfInterest> PointOfInterests { get; set; }
    }
}
