using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PoliHack.Service;
using PoliHack.Service.Algorithms;

namespace PoliHack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();

            // Dijkstra.TestDijsktra();
            // DifferenceBetweenShortestAndSecondShortestPath.Run();

            // TrafficSimulator trafficSimulator = new TrafficSimulator(RoadSystemConfiguration.GetDummyValue(), 10);
            // trafficSimulator.analyzeJourneys(trafficSimulator.Simulate());
            RoadModificationsGenerator roadModificationsGenerator =
                new RoadModificationsGenerator(RoadSystemConfiguration.GetDummyValue());
            roadModificationsGenerator.initBacktracking();
            roadModificationsGenerator.back(0);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}