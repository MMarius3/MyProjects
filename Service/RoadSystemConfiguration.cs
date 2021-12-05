using System.Collections.Generic;
using System.Linq;

namespace PoliHack.Service
{
    public class RoadSystemConfiguration
    {
        public List<Street> CurrentRoadSystemConfiguration;
        public int NrVertices;

        // public void incrementRoadPopularity(Road road)
        // {
        //     CurrentRoadSystemConfiguration.ElementAt(roadDictionary[road]).incrementPopularity();
        // }

        public List<Lane> StreetsListToLanesList()
        {
            List<Lane> lanesList = new List<Lane>();

            CurrentRoadSystemConfiguration.ForEach(street =>
                street.LanesListOnStreet.ForEach(lane => lanesList.Add((Lane) lane.ShallowCopy())));

            return lanesList;
        }
        
        public object ShallowCopy()
        {
            return MemberwiseClone();
        }

        public static RoadSystemConfiguration GetDummyValue()
        {
            Street street0 = new Street(0, "0", 0, 1);
            Lane lane0 = new Lane(0, false, 60);
            Lane lane1 = new Lane(0, true, 50);
            street0.AddLaneToStreet(lane0);
            street0.AddLaneToStreet(lane1);

            Street street1 = new Street(1, "1", 0, 3);
            Lane lane2 = new Lane(1, false, 100);
            Lane lane3 = new Lane(1, false, 100);
            street1.AddLaneToStreet(lane2);
            street1.AddLaneToStreet(lane3);

            Street street2 = new Street(2, "2", 1, 4);
            Lane lane4 = new Lane(2, false, 10);
            street2.AddLaneToStreet(lane4);

            Street street3 = new Street(3, "3", 1, 2);
            Lane lane5 = new Lane(3, false, 40);
            street3.AddLaneToStreet(lane5);

            Street street4 = new Street(4, "4", 1, 3);
            Lane lane6 = new Lane(4, false, 70);
            street4.AddLaneToStreet(lane0);

            Street street5 = new Street(5, "5", 3, 4);
            Lane lane7 = new Lane(5, false, 80);
            Lane lane8 = new Lane(5, true, 10);
            street5.AddLaneToStreet(lane7);
            street5.AddLaneToStreet(lane8);

            Street street6 = new Street(6, "6", 2, 3);
            Lane lane9 = new Lane(6, false, 10);
            Lane lane10 = new Lane(6, false, 10);
            Lane lane11 = new Lane(6, false, 10);
            street6.AddLaneToStreet(lane9);
            street6.AddLaneToStreet(lane10);
            street6.AddLaneToStreet(lane11);

            List<Street> streetsList = new List<Street>()
                {street0, street1, street2, street3, street4, street5, street6};

            RoadSystemConfiguration roadSystemConfiguration = new RoadSystemConfiguration
            {
                NrVertices = 5,
                CurrentRoadSystemConfiguration = streetsList
            };

            return roadSystemConfiguration;
        }
    }
}