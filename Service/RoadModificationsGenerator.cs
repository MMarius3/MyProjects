using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PoliHack.Service.LanesConfiguration;

namespace PoliHack.Service
{
    public class RoadModificationsGenerator
    {
        private RoadSystemConfiguration _roadSystemConfiguration;

        public RoadModificationsGenerator(RoadSystemConfiguration roadSystemConfiguration)
        {
            _roadSystemConfiguration = roadSystemConfiguration;
        }

        public RoadSystemConfiguration GetNextConfiguration()
        {
            return null;
        }

        public int n = 4;
        public int[] st = new int[30];
        public int[] vec = {40, 10, 20, 30};

        private void tipar(int p)
        {
            int i;
            for (i = 1; i <= p; i++)
            {
                // Console.Write(vec[st[i]] + " ");
                Console.Write(st[i] + " ");
            }

            Console.WriteLine();
        }

        private List<LanesConfiguration.LanesConfiguration> streetsLaneConfigurations =
            new List<LanesConfiguration.LanesConfiguration>();

        public void initBacktracking()
        {
            // for (int streetIndex = 0;
            //     streetIndex < _roadSystemConfiguration.CurrentRoadSystemConfiguration.Count;
            //     streetIndex++)
            // {
            //     int nrLanesOnStreet = _roadSystemConfiguration.CurrentRoadSystemConfiguration.ElementAt(streetIndex)
            //         .LanesListOnStreet.Count;
            //     streetsLaneConfigurations.Add(GetInstanceFromNrLanes(nrLanesOnStreet));
            // }

            arr = new int[_roadSystemConfiguration.CurrentRoadSystemConfiguration.Count];

            for (int i = 0; i < _roadSystemConfiguration.CurrentRoadSystemConfiguration.Count; i++)
            {
                arr[i] = streetsLaneConfigurations.ElementAt(i).GetLanesConfigurations().Count;
            }
        }

        private int[] arr;

        public void back(int vf)
        {
            for (int k = 0; k < _roadSystemConfiguration.CurrentRoadSystemConfiguration.Count; k++)
            {
                st[vf] = arr[k];

                if (vf == n)
                {
                    tipar(vf);
                }
                else
                {
                    back(vf + 1);
                }
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