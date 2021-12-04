using System.Collections.Generic;
using System.Linq;

namespace PoliHack.Service
{
    public class PopularityAnalyzer
    {
        private List<List<Road>> roadsListList;
        private RoadSystemConfiguration _roadSystemConfiguration;

        public PopularityAnalyzer(RoadSystemConfiguration roadSystemConfiguration)
        {
            this._roadSystemConfiguration = roadSystemConfiguration;
        }

        public void AddRoadsList(List<Road> roadsList)
        {
            roadsListList = new List<List<Road>>();
            roadsListList.Add(roadsList);
        }

        /*
         *  generates an ascending list based on popularity with all roads 
         */
        public List<Road> AnalyzePopularity()
        {
            foreach (List<Road> currentRoadsList in roadsListList)
            {
                foreach (Road currentRoad in currentRoadsList)
                {
                    _roadSystemConfiguration.incrementRoadPopularity(currentRoad);
                }
            }

            return _roadSystemConfiguration.CurrentRoadSystemConfiguration
                .OrderByDescending(road => road.RoadPopularity).ToList();
        }
    }
}