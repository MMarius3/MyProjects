namespace PoliHack.Service
{
    using PoliHack.Models;
    using System;
    using System.Collections.Generic;

    public class LinesToGraph
    {
        private Dictionary<(int, int), (bool, int)> alreadyNoted;
        private List<Street> streets;
        public void DoTheThing(List<Line> lines)
        {
            alreadyNoted = new Dictionary<(int, int), (bool, int)>();
            streets = new List<Street>();
            AdjustVertices(lines);
            FormVertices(lines);
            ConvertInGraph(lines);
        }
        
        private void AdjustVertices(List<Line> lines)
        {
            foreach (var line1 in lines)
            {
                foreach (var line2 in lines)
                {
                    if (Math.Abs(line1.P1.Item1 - line2.P1.Item1) < 5 & Math.Abs(line1.P1.Item2 - line2.P1.Item2) < 5)
                    {
                        line2.P1 = (line1.P1.Item1, line1.P1.Item2);
                    }
                    if (Math.Abs(line1.P1.Item1 - line2.P2.Item1) < 5 & Math.Abs(line1.P1.Item2 - line2.P2.Item2) < 5)
                    {
                        line2.P2 = (line1.P1.Item1, line1.P1.Item2);
                    }
                    if (Math.Abs(line1.P2.Item1 - line2.P1.Item1) < 5 & Math.Abs(line1.P2.Item2 - line2.P1.Item2) < 5)
                    {
                        line2.P1 = (line1.P2.Item1, line1.P2.Item2);
                    }
                    if (Math.Abs(line1.P2.Item1 - line2.P2.Item1) < 5 & Math.Abs(line1.P2.Item2 - line2.P2.Item2) < 5)
                    {
                        line2.P2 = (line1.P2.Item1, line1.P2.Item2);
                    }
                }
            }
        }

        private void FormVertices(List<Line> lines)
        {
            foreach (var line in lines)
            {
                var item = (false, 0);
                alreadyNoted.Add(line.P1, item);
                alreadyNoted.Add(line.P2, item);
            }
            var nrOfVertices = 0;
            foreach (var line in lines)
            {
                int x1, x2;
                if (!(alreadyNoted[(line.P1.Item1, line.P1.Item2)].Item1))
                {
                    x1 = nrOfVertices;
                    alreadyNoted[(line.P1.Item1, line.P1.Item2)] = (true, nrOfVertices);
                    nrOfVertices++;
                }
                else
                {
                    x1 = alreadyNoted[(line.P1.Item1, line.P1.Item2)].Item2;
                }
                if (!(alreadyNoted[(line.P2.Item1, line.P2.Item2)].Item1))
                {
                    x2 = nrOfVertices;
                    alreadyNoted[(line.P1.Item1, line.P1.Item2)] = (true, nrOfVertices);
                    nrOfVertices++;
                }
                else
                {
                    x2 = alreadyNoted[(line.P2.Item1, line.P2.Item2)].Item2;
                }
                line.Vertices = (x1, x2);
            }
        }

        private void ConvertInGraph(List<Line> lines)
        {
            var id = 0;
            foreach (var line in lines)
            {
                List<Lane> lanes = new List<Lane>();
                for (var i = 0; i < line.NrLanes.Item1; i++)
                {
                    lanes.Add(id, true, line.Cost.Item1, 0);
                }
                for (var i = 0; i < line.NrLanes.Item2; i++)
                {
                    lanes.Add(id, false, line.Cost.Item2, 0);
                }
                    
                streets.Add(id, line.Name, lanes, line.Vertices.Item1, line.Vertices.Item2, line.Modifiable, 0);
                id++;
            }
        }
    }
}