using System.Collections.Generic;
using System.Linq;

namespace PoliHack.Service
{
    public class Street
    {
        private int _streetID;
        private string _streetName;
        private List<Lane> _lanesListOnStreet;

        private int _vertex0;
        private int _vertex1;

        private bool _isModifiable;
        private int _streetPopularity;

        public Street(int streetID, string streetName, int vertex0, int vertex1, bool isModifiable = true)
        {
            _streetID = streetID;
            _streetName = streetName;
            _vertex0 = vertex0;
            _vertex1 = vertex1;
            _isModifiable = isModifiable;
            _lanesListOnStreet = new List<Lane>();
        }

        public object ShallowCopy()
        {
            return MemberwiseClone();
        }

        public void AddLaneToStreet(Lane lane)
        {
            _lanesListOnStreet.Add(lane);
        }

        public void ComputeStreetPopularity()
        {
            _streetPopularity = _lanesListOnStreet.Sum(lane => lane.LanePopularity);
        }

        public int StreetID => _streetID;

        public string StreetName => _streetName;

        public List<Lane> LanesListOnStreet => _lanesListOnStreet;

        public int Vertex0 => _vertex0;

        public int Vertex1 => _vertex1;

        public bool IsModifiable => _isModifiable;

        public int StreetPopularity => _streetPopularity;
    }
}