using System;

namespace PoliHack.Models
{
    public class Line
    {
        private (int, int ) P1 { get; set; }
        private (int, int) P2 { get; set; }
        private int Cost { get; set; }
        
        public Line((int, int) p1, (int, int) p2, int cost)
        {
            this.P1 = p1;
            this.P2 = p1;
            this.Cost = cost;
        }
    }
}