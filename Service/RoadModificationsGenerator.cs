using System.Collections;
using System.Collections.Generic;

namespace PoliHack.Service
{
    public class RoadModificationsGenerator
    {
        private List<Road>  _initialRoadSystemConfiguration;

        public RoadModificationsGenerator(List<Road>  initialRoadSystemConfiguration)
        {
            this._initialRoadSystemConfiguration = initialRoadSystemConfiguration;
        }

        public List<Road>  GetNextConfiguration()
        {
            return null;
        }
    }
}