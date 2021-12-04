using System.Collections.Generic;
using System.Linq;

namespace PoliHack.Service
{
    public class RoadSystemConfiguration
    {
        public List<Road> CurrentRoadSystemConfiguration;
        public int NrVertices;
        private Dictionary<Road, int> roadDictionary = new Dictionary<Road, int>();

        public void InitDictionary()
        {
            for (int i = 0; i < CurrentRoadSystemConfiguration.Count; i++)
            {
                roadDictionary.Add(CurrentRoadSystemConfiguration.ElementAt(i), i);
            }
        }

        public void incrementRoadPopularity(Road road)
        {
            CurrentRoadSystemConfiguration.ElementAt(roadDictionary[road]).incrementPopularity();
        }

        public static RoadSystemConfiguration GetDummyValue()
        {
            RoadSystemConfiguration roadSystemConfiguration = new RoadSystemConfiguration();
            roadSystemConfiguration.NrVertices = 6;

            roadSystemConfiguration.CurrentRoadSystemConfiguration = new List<Road>();

            Road road = new Road(0, 1, 10, true, 0);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(0, 1, 10, true, 1);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(0, 2, 100, true, 0);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(0, 3, 40);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(2, 4, 60);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(2, 5, 20);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(3, 4, 80, true, 0);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(4, 3, 50, true, 1);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(4, 5, 30);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(5, 0, 200);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            road = new Road(5, 1, 50);
            roadSystemConfiguration.CurrentRoadSystemConfiguration.Add(road);

            roadSystemConfiguration.InitDictionary();

            return roadSystemConfiguration;
        }
    }
}