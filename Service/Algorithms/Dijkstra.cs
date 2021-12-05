using System.Collections.Generic;
using System.Linq;

namespace PoliHack.Service.Algorithms
{
    using System;

    public class Dijkstra
    {
        private RoadSystemConfiguration _roadSystemConfiguration;
        private int _nrVertices;
        private int _startVertex;
        private int _endVertex;
        private int[][] _adjacencyMatrix;

        private int[] _distance;
        private int[] _parents;

        private readonly int NO_PARENT = -1;

        private List<int> _shortestPath = new List<int>();

        public Dijkstra(RoadSystemConfiguration roadSystemConfiguration, int nrVertices, int startVertex,
            int endVertex)
        {
            _roadSystemConfiguration = roadSystemConfiguration;
            _nrVertices = nrVertices;
            RoadSystemConfigurationToAdjacencyMatrix();

            _startVertex = startVertex;
            _endVertex = endVertex;
            _distance = new int[_nrVertices];
            _parents = new int[_nrVertices];
        }

        public void RunDijkstra()
        {
            bool[] added = new bool[_nrVertices];

            for (int vertexIndex = 0; vertexIndex < _nrVertices; vertexIndex++)
            {
                _distance[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }

            _distance[_startVertex] = 0;

            _parents[_startVertex] = NO_PARENT;

            for (int i = 1; i < _nrVertices; i++)
            {
                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;

                for (int vertexIndex = 0; vertexIndex < _nrVertices; vertexIndex++)
                {
                    if (!added[vertexIndex] && _distance[vertexIndex] < shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = _distance[vertexIndex];
                    }
                }

                added[nearestVertex] = true;

                for (int vertexIndex = 0; vertexIndex < _nrVertices; vertexIndex++)
                {
                    int edgeDistance = _adjacencyMatrix[nearestVertex][vertexIndex];

                    if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < _distance[vertexIndex]))
                    {
                        _parents[vertexIndex] = nearestVertex;
                        _distance[vertexIndex] = shortestDistance + edgeDistance;
                    }
                }
            }

            // PrintSolution();
        }

        private void PrintSolution()
        {
            int nVertices = _distance.Length;
            Console.Write("Vertex\t Distance\tPath");

            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                if (vertexIndex != _startVertex)
                {
                    Console.Write("\n" + _startVertex + " -> ");
                    Console.Write(vertexIndex + " \t\t ");
                    Console.Write(_distance[vertexIndex] + "\t\t");
                    printPath(vertexIndex, _parents);
                }
            }
        }

        private void printPath(int currentVertex, int[] parents)
        {
            if (currentVertex == NO_PARENT)
            {
                return;
            }

            printPath(parents[currentVertex], parents);
            Console.Write(currentVertex + " ");
        }

        private void GetShortestPath(int currentVertex, int[] parents)
        {
            if (currentVertex == NO_PARENT)
            {
                return;
            }

            GetShortestPath(parents[currentVertex], parents);
            _shortestPath.Add(currentVertex);
        }

        private void RoadSystemConfigurationToAdjacencyMatrix()
        {
            _adjacencyMatrix = new int[_nrVertices][];

            for (int i = 0; i < _nrVertices; i++)
            {
                _adjacencyMatrix[i] = new int[_nrVertices];
            }

            _roadSystemConfiguration.StreetsListToLanesList()
                .ForEach(lane =>
                {
                    int startVertex = _roadSystemConfiguration.CurrentRoadSystemConfiguration.ElementAt(lane.StreetID)
                        .Vertex0;
                    int endVertex = _roadSystemConfiguration.CurrentRoadSystemConfiguration.ElementAt(lane.StreetID)
                        .Vertex1;

                    if (lane.LaneOrientation)
                    {
                        (startVertex, endVertex) = (endVertex, startVertex);
                    }

                    Console.WriteLine(startVertex + " " + endVertex + " " + lane.LaneCost);

                    _adjacencyMatrix[startVertex][endVertex] = lane.LaneCost;
                });
        }

        public int GetShortestDistanceFromStartToEnd()
        {
            return _distance[_endVertex];
        }

        public List<int> GetShortestDistanceVerticesList()
        {
            GetShortestPath(_endVertex, _parents);
            List<int> shortest = new List<int>();

            for (int i = 0; i < this._shortestPath.Count; i++)
            {
                shortest.Add(_shortestPath[i]);
            }

            return shortest;
        }

        public List<Lane> GetShortestDistanceLanesList()
        {
            List<Lane> shortestDistanceLaneList = new List<Lane>();

            GetShortestPath(_endVertex, _parents);

            // _roadSystemConfiguration.CurrentRoadSystemConfiguration.ForEach(street =>
            //     Console.WriteLine(street.Vertex0 + " " + street.Vertex1));

            for (int i = 0; i < _shortestPath.Count - 1; i++)
            {
                Street lanesStreet = _roadSystemConfiguration.CurrentRoadSystemConfiguration.Find(street =>
                    street.Vertex0 == _shortestPath[i] && street.Vertex1 == _shortestPath[i + 1] ||
                    street.Vertex0 == _shortestPath[i + 1] && street.Vertex1 == _shortestPath[i]);

                Lane lane = lanesStreet.LanesListOnStreet.Find(lane =>
                    !lane.LaneOrientation &&
                    lanesStreet.Vertex0 == _shortestPath[i] &&
                    lanesStreet.Vertex1 == _shortestPath[i + 1]
                    ||
                    lane.LaneOrientation &&
                    lanesStreet.Vertex0 == _shortestPath[i + 1] &&
                    lanesStreet.Vertex1 == _shortestPath[i]
                );

                shortestDistanceLaneList.Add(lane);
            }

            return shortestDistanceLaneList;
        }

        public static void TestDijsktra()
        {
            Dijkstra dijkstrasAlgorithm =
                new Dijkstra(RoadSystemConfiguration.GetDummyValue(), 5, 0, 4);
            dijkstrasAlgorithm.RunDijkstra();

            // Console.WriteLine();

            List<Lane> lanes = dijkstrasAlgorithm.GetShortestDistanceLanesList();

            // lanes.ForEach(lane => Console.WriteLine(lane.LaneCost));
        }
    }
}