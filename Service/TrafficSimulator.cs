using System;
using System.Collections.Generic;
using System.Linq;
using PoliHack.Service.Algorithms;

namespace PoliHack.Service
{
    public class TrafficSimulator
    {
        private RoadSystemConfiguration _currentRoadSystemConfiguration;
        private int _currentRoadSystemTotalTime;
        private int _nrSimulations;

        private List<JourneyResult> journiesResultList;

        public TrafficSimulator(RoadSystemConfiguration currentRoadSystemConfiguration, int nrSimulations)
        {
            this._currentRoadSystemConfiguration = currentRoadSystemConfiguration;

            this._currentRoadSystemTotalTime =
                this._currentRoadSystemConfiguration.StreetsListToLanesList().Sum(lane => lane.LaneCost);

            this._nrSimulations = nrSimulations;
        }

        private int GetCurrentRoadSystemTotalTime()
        {
            return _currentRoadSystemTotalTime;
        }

        public List<JourneyResult> Simulate()
        {
            List<JourneyResult> journeyResults = new List<JourneyResult>();
            Dijkstra dijsktra;

            for (int simulationIndex = 0; simulationIndex < _nrSimulations; simulationIndex++)
            {
                (int, int) randomSimulationEndpoints = GetRandomSimulationEndpoints();
                int startVertex = randomSimulationEndpoints.Item1;
                int endVertex = randomSimulationEndpoints.Item2;

                dijsktra = new Dijkstra(_currentRoadSystemConfiguration, _currentRoadSystemConfiguration.NrVertices,
                    startVertex, endVertex);

                bool journeyIsPossible = true;
                int shortestDistance = 0;
                List<int> path = new List<int>();

                try
                {
                    dijsktra.RunDijkstra();
                    shortestDistance = dijsktra.GetShortestDistanceFromStartToEnd();
                    path = dijsktra.GetShortestDistanceVerticesList();
                }
                catch (Exception ex)
                {
                    journeyIsPossible = false;
                    simulationIndex--;
                    continue;
                }

                journeyResults.Add(new JourneyResult(path, shortestDistance, startVertex, endVertex,
                    journeyIsPossible));
            }

            return journeyResults;
        }

        public void analyzeJourneys(List<JourneyResult> journeyResults)
        {
            int averageCost = journeyResults.Sum(result => result.JourneyValue) / _nrSimulations;
            Console.WriteLine(averageCost);

            int[][] popularityMatrix = new int[_currentRoadSystemConfiguration.NrVertices][];

            for (int i = 0; i < _currentRoadSystemConfiguration.NrVertices; i++)
            {
                popularityMatrix[i] = new int[_currentRoadSystemConfiguration.NrVertices];
            }

            journeyResults.ForEach(result =>
                {
                    for (int i = 0; i < result.PathsList.Count - 1; i++)
                    {
                        popularityMatrix[result.PathsList[i]][result.PathsList[i + 1]]++;
                    }
                }
            );

            for (int i = 0; i < _currentRoadSystemConfiguration.NrVertices; i++)
            {
                for (int j = 0; j < _currentRoadSystemConfiguration.NrVertices; j++)
                {
                    Console.Write(popularityMatrix[i][j] + " ");
                }

                Console.WriteLine();
            }
        }

        /*
         * returns a tuple (startVertexID, endVertexID);
         */
        private (int, int) GetRandomSimulationEndpoints()
        {
            Random random = new Random();
            int startVertexID = random.Next(0, _currentRoadSystemConfiguration.NrVertices);
            int endVertexID = random.Next(0, _currentRoadSystemConfiguration.NrVertices);

            // since we randomly generate the endVertexID, we have to make sure it does not coincide with startVertexID
            while (startVertexID == endVertexID)
            {
                endVertexID = random.Next(0, this._currentRoadSystemConfiguration.NrVertices);
            }

            return (startVertexID, endVertexID);
        }
    }
}