using System;
using System.Collections.Generic;

namespace PoliHack.Service
{
    public class Dijsktra2
    {
        private RoadSystemConfiguration _roadSystemConfiguration;
        private int startVertex;
        private int endVertex;
        private int[][] _adjacencyMatrix;

        private int[] distance;
        private int[] parents;


        public Dijsktra2(RoadSystemConfiguration roadSystemConfiguration, int startVertex, int endVertex)
        {
            _roadSystemConfiguration = roadSystemConfiguration;
            RoadSystemConfigurationToAdjacencyMatrix();

            this.startVertex = startVertex;
            this.endVertex = endVertex;

            this.distance = new int[_roadSystemConfiguration.NrVertices];
            this.parents = new int[_roadSystemConfiguration.NrVertices];
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

        int minDistance(bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int currentVertexIndex = 0;
                currentVertexIndex < _roadSystemConfiguration.NrVertices;
                currentVertexIndex++)

                if (sptSet[currentVertexIndex] == false && distance[currentVertexIndex] <= min)
                {
                    min = distance[currentVertexIndex];
                    min_index = currentVertexIndex;
                }

            return min_index;
        }

        void printSolution(int[] distance)
        {
            Console.Write("Vertex \t\t Distance "
                          + "from Source\n");
            for (int i = 0; i < _roadSystemConfiguration.NrVertices; i++)
            {
                Console.Write(i + " \t\t " + distance[i] + "\n");
                
                // printPath(i, )
            }
        }
        
        // Function to print shortest path
        // from source to currentVertex
        // using parents array
        private void printPath(int currentVertex,
            int[] parents)
        {
            // Base case : Source node has
            // been processed
            if (currentVertex == -1)
            {
                return;
            }

            printPath(parents[currentVertex], parents);
            Console.Write(currentVertex + " ");
        }

        public void RunDijkstra()
        {
            int nrVertices = _roadSystemConfiguration.NrVertices;
            bool[] sptSet = new bool[nrVertices];

            parents[startVertex] = -1;
            
            for (int i = 0; i < nrVertices; i++)
            {
                distance[i] = int.MaxValue;
                sptSet[i] = false;
            }

            distance[startVertex] = 0;

            for (int count = 0; count < nrVertices - 1; count++)
            {
                int u = minDistance(sptSet);

                sptSet[u] = true;

                for (int currentVertexID = 0; currentVertexID < nrVertices; currentVertexID++)
                {
                    if (!sptSet[currentVertexID] && _adjacencyMatrix[u][currentVertexID] != 0 &&
                        distance[u] != int.MaxValue &&
                        distance[u] + _adjacencyMatrix[u][currentVertexID] < distance[currentVertexID])
                    {
                        distance[currentVertexID] = distance[u] + _adjacencyMatrix[u][currentVertexID];
                    }
                }
            }

            printSolution(distance);
        }

        public int GetShortestDistanceFromStartToEnd()
        {
            return distance[endVertex];
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < _adjacencyMatrix.Length; i++)
            {
                Console.Write(i);
                Console.Write(":");
                for (int j = 0; j < _adjacencyMatrix[i].Length; j++)
                {
                    Console.Write(_adjacencyMatrix[i][j]);
                    Console.Write(" ");
                }

                Console.Write("\n");
            }
        }

        public static void Run()
        {
            Dijsktra2 dijsktra = new Dijsktra2(RoadSystemConfiguration.GetDummyValue(), 0, 4);
            dijsktra.RunDijkstra();
            dijsktra.PrintMatrix();
        }
    }
}