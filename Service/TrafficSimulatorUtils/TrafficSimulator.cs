using System;
using System.Linq;

namespace PoliHack.Service
{
    public class TrafficSimulator
    {
        private RoadSystemConfiguration _currentRoadSystemConfiguration;
        private int _currentRoadSystemTotalTime;
        private int _carsToSimulate;
        private int _nrScenarios;

        public TrafficSimulator(RoadSystemConfiguration currentRoadSystemConfiguration, int carsToSimulate,
            int nrScenarios)
        {
            this._currentRoadSystemConfiguration = currentRoadSystemConfiguration;

            this._currentRoadSystemTotalTime =
                this._currentRoadSystemConfiguration.CurrentRoadSystemConfiguration.Sum(currentRoad =>
                    currentRoad.RoadTime);

            this._carsToSimulate = carsToSimulate;
            this._nrScenarios = nrScenarios;
        }

        private int GetCurrentRoadSystemTotalTime()
        {
            return this._currentRoadSystemTotalTime;
        }

        public void Simulate()
        {
            Dijkstra dijsktra;

            for (int scenarioIndex = 0; scenarioIndex < _nrScenarios; scenarioIndex++)
            {
                for (int carIndex = 0; carIndex < _carsToSimulate; carIndex++)
                {
                    (int, int) randomSimulationEndpoints = GetRandomSimulationEndpoints();
                    int startVertex = randomSimulationEndpoints.Item1;
                    int endVertex = randomSimulationEndpoints.Item2;

                    dijsktra = new Dijkstra(_currentRoadSystemConfiguration, startVertex, endVertex);
                    dijsktra.RunDijkstra();
                    dijsktra.GetShortestDistanceFromStartToEnd();
                    // dijsktra.getShortestDistanceRoadsList();
                }
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