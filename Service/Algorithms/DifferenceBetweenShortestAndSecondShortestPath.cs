using System;
using System.Collections.Generic;

namespace PoliHack.Service
{
    public class DifferenceBetweenShortestAndSecondShortestPath
    {
        // DFS function to find all possible paths.
        private void dfs(List<List<int>> graph, int s, int e, List<int> v, int count, List<int> dp)
        {
            if (s == e)
            {
                // Push the number of nodes required for
                // one of the possible paths
                dp.Add(count);
                return;
            }

            foreach (int i in graph[s])
            {
                if (v[i] != 1)
                {
                    // Mark the node as visited and
                    // call the function to search for
                    // possible paths and unmark the node.
                    v[i] = 1;
                    dfs(graph, i, e, v, count + 1, dp);
                    v[i] = 0;
                }
            }
        }

        // Function to find the difference between the
        // shortest and second shortest path
        public void findDifference(int n, int m, int[,] arr)
        {
            // Construct the graph
            List<List<int>> graph = new List<List<int>>();
            for (int i = 0; i < n; i++)
            {
                graph.Add(new List<int>());
            }

            for (int i = 0; i < m; i++)
            {
                int a, b;
                a = arr[i, 0];
                b = arr[i, 1];
                graph[a - 1].Add(b - 1);
                graph[b - 1].Add(a - 1);
            }

            // Vector to mark the nodes as visited or not.
            List<int> v = new List<int>();
            for (int i = 0; i < n; i++)
            {
                v.Add(0);
            }

            // Vector to store the count of all possible paths.
            List<int> dp = new List<int>();

            // Mark the starting node as visited.
            v[0] = 1;

            // Function to find all possible paths.
            dfs(graph, 0, n - 1, v, 0, dp);

            // Sort the vector
            dp.Sort();

            // Print the difference
            if (dp.Count != 1) Console.Write(dp[1] - dp[0]);
            else Console.Write(0);
        }

        public static void Run()
        {
            int n, m;
            n = 6;
            m = 8;
            int[,] arr
                =
                {
                    {1, 2}, {1, 3},
                    {2, 6}, {2, 3},
                    {2, 4}, {3, 4},
                    {3, 5}, {4, 6}
                };

            DifferenceBetweenShortestAndSecondShortestPath diff = new DifferenceBetweenShortestAndSecondShortestPath();
            diff.findDifference(n, m, arr);
        }
    }
}
