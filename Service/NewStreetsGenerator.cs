using System;
using System.Linq;
using PoliHack.Service.LanesConfiguration;

namespace PoliHack.Service
{
    public class NewStreetsGenerator
    {
        // original
        private RoadSystemConfiguration _roadSystemConfiguration;

        public NewStreetsGenerator(RoadSystemConfiguration roadSystemConfiguration)
        {
            _roadSystemConfiguration = roadSystemConfiguration;
        }

        public void generateNewConfiguration()
        {
            RoadSystemConfiguration roadSystemConfigurationCopy =
                (RoadSystemConfiguration) _roadSystemConfiguration.ShallowCopy();

            for (int i = 0; i < roadSystemConfigurationCopy.CurrentRoadSystemConfiguration.Count; i++)
            {
                int nrLanes = roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet.Count;

                LanesConfiguration.LanesConfiguration newConfiguration = GetInstanceFromNrLanes(nrLanes);

                int costsSumForTrueOrientation = roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                    .LanesListOnStreet
                    .Sum(lane => lane.LaneOrientation ? lane.LaneCost : 0);

                int costsSumForFalseOrientation = roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                    .LanesListOnStreet
                    .Sum(lane => !lane.LaneOrientation ? lane.LaneCost : 0);

                // daca e diferenta mare intre sensuri
                if (Math.Abs(costsSumForTrueOrientation - costsSumForFalseOrientation) >
                    0.3 * Math.Max(costsSumForTrueOrientation, costsSumForFalseOrientation))
                {
                    // daca exista sensuri in ambele parti
                    if (costsSumForTrueOrientation > 0 && costsSumForFalseOrientation > 0)
                    {
                        // daca costul mai mare e pe sensul true
                        if (costsSumForTrueOrientation > costsSumForFalseOrientation)
                        {
                            Lane foundLane = roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                                .LanesListOnStreet
                                .Find(lane => !lane.LaneOrientation);

                            // parcurgem toate
                            for (int j = 0;
                                j < roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet
                                    .Count;
                                j++)
                            {
                                if (!roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                    .LaneOrientation)
                                {
                                    roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                        .setLaneOrientation(true);
                                    break;
                                }
                            }

                            for (int j = 0;
                                j < roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet
                                    .Count;
                                j++)
                            {
                                if (!roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                    .LaneOrientation)
                                {
                                    roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                        .setLaneCost(roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                                            .LanesListOnStreet[j].LaneCost * 2);
                                }
                                else if (roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                                    .LanesListOnStreet[j]
                                    .LaneOrientation)
                                {
                                    roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                        .setLaneCost(roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                                            .LanesListOnStreet[j].LaneCost / 2);
                                }
                            }
                        }
                    }

                    // daca costul mai mare e pe sensul false
                    if (costsSumForTrueOrientation < costsSumForFalseOrientation)
                    {
                        Lane foundLane = roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                            .LanesListOnStreet
                            .Find(lane => lane.LaneOrientation);

                        // parcurgem toate
                        for (int j = 0;
                            j < roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet
                                .Count;
                            j++)
                        {
                            if (roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                .LaneOrientation)
                            {
                                roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                    .setLaneOrientation(false);
                                break;
                            }
                        }

                        for (int j = 0;
                            j < roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet
                                .Count;
                            j++)
                        {
                            if (roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                .LaneOrientation)
                            {
                                roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                    .setLaneCost(roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                                        .LanesListOnStreet[j].LaneCost * 2);
                            }
                            else if (!roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                                .LanesListOnStreet[j]
                                .LaneOrientation)
                            {
                                roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i].LanesListOnStreet[j]
                                    .setLaneCost(roadSystemConfigurationCopy.CurrentRoadSystemConfiguration[i]
                                        .LanesListOnStreet[j].LaneCost / 2);
                            }
                        }
                    }

                    if (costsSumForTrueOrientation == 0)
                    {
                    }

                    if (costsSumForFalseOrientation == 0)
                    {
                    }
                }

                // applyConfigurationToStreet(newConfiguration, )
            }
        }

        private LanesConfiguration.LanesConfiguration GetInstanceFromNrLanes(int nrLanes)
        {
            switch (nrLanes)
            {
                case 1:
                    return new OneLane();
                case 2:
                    return new TwoLanes();
                case 3:
                    return new ThreeLanes();
                default:
                    return null;
            }
        }
    }
}
