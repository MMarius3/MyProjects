namespace PoliHack.Service
{
    using System;

    public class Dijkstra
    {
        private RoadSystemConfiguration _roadSystemConfiguration;
        private int startVertex;
        private int endVertex;
        private int[][] _adjacencyMatrix;

        private int[] distance;
        private int[] parents;

        private readonly int NO_PARENT = -1;

        public Dijkstra(RoadSystemConfiguration roadSystemConfiguration, int startVertex, int endVertex)
        {
            _roadSystemConfiguration = roadSystemConfiguration;
            RoadSystemConfigurationToAdjacencyMatrix();

            this.startVertex = startVertex;
            this.endVertex = endVertex;

            this.distance = new int[_roadSystemConfiguration.NrVertices];
            this.parents = new int[_roadSystemConfiguration.NrVertices];
        }

        public void RunDijkstra()
        {
            int nrVertices = _roadSystemConfiguration.NrVertices;
            bool[] added = new bool[nrVertices];

            for (int vertexIndex = 0;
                vertexIndex < nrVertices;
                vertexIndex++)
            {
                distance[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }

            distance[startVertex] = 0;

            parents[startVertex] = NO_PARENT;

            for (int i = 1; i < nrVertices; i++)
            {
                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;
                for (int vertexIndex = 0;
                    vertexIndex < nrVertices;
                    vertexIndex++)
                {
                    if (!added[vertexIndex] &&
                        distance[vertexIndex] <
                        shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = distance[vertexIndex];
                    }
                }

                added[nearestVertex] = true;

                for (int vertexIndex = 0;
                    vertexIndex < nrVertices;
                    vertexIndex++)
                {
                    int edgeDistance = _adjacencyMatrix[nearestVertex][vertexIndex];

                    if (edgeDistance > 0
                        && ((shortestDistance + edgeDistance) <
                            distance[vertexIndex]))
                    {
                        parents[vertexIndex] = nearestVertex;
                        distance[vertexIndex] = shortestDistance +
                                                edgeDistance;
                    }
                }
            }

            printSolution(startVertex, distance, parents);
        }

        private void printSolution(int startVertex,
            int[] distances,
            int[] parents)
        {
            int nVertices = distances.Length;
            Console.Write("Vertex\t Distance\tPath");

            for (int vertexIndex = 0;
                vertexIndex < nVertices;
                vertexIndex++)
            {
                if (vertexIndex != startVertex)
                {
                    Console.Write("\n" + startVertex + " -> ");
                    Console.Write(vertexIndex + " \t\t ");
                    Console.Write(distances[vertexIndex] + "\t\t");
                    printPath(vertexIndex, parents);
                }
            }
        }

        private void printPath(int currentVertex,
            int[] parents)
        {
            // Base case : Source node has
            // been processed
            if (currentVertex == NO_PARENT)
            {
                return;
            }

            printPath(parents[currentVertex], parents);
            Console.Write(currentVertex + " ");
        }

        private void RoadSystemConfigurationToAdjacencyMatrix()
        {
            _adjacencyMatrix = new int[_roadSystemConfiguration.NrVertices][];

            for (int i = 0; i < _roadSystemConfiguration.NrVertices; i++)
            {
                _adjacencyMatrix[i] = new int[_roadSystemConfiguration.NrVertices];
            }

            _roadSystemConfiguration.CurrentRoadSystemConfiguration.ForEach(road =>
                _adjacencyMatrix[road.Vertex0ID][road.Vertex1ID] = road.RoadTime);
        }

        public int GetShortestDistanceFromStartToEnd()
        {
            return distance[endVertex];
        }

        public static void TestDijsktra()
        {
            Dijkstra dijkstrasAlgorithm =
                new Dijkstra(RoadSystemConfiguration.GetDummyValue(), 0, 4);
            dijkstrasAlgorithm.RunDijkstra();
        }
    }
}