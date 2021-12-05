using System;

namespace PoliHack.Models
{
    public class Line
    {
        public (int, int ) P1 { get; set; }
        public (int, int) P2 { get; set; }
        public (int, int) Cost { get; set; }
        public (int, int) NrLanes { get; set; }
        public string Name { get; set; }
        public bool Modifiable { get; set; }
        public (int, int) Vertices { get; set; }

        public Line((int, int) p1, (int, int) p2, (int, int) cost, (int, int) nrLanes, string name, bool modifiable, (int, int) vertices)
        {
            this.P1 = p1;
            this.P2 = p1;
            this.Cost = cost;
            this.NrLanes = nrLanes;
            this.Name = name;
            this.Modifiable = modifiable;
            this.Vertices = vertices;
        }
    }
}