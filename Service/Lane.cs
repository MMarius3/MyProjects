namespace PoliHack.Service
{
    public struct Lane
    {
        private int _streetID;
        private bool _laneOrientation;
        private int _laneCost;
        private int _lanePopularity;

        public Lane(int streetID, bool laneOrientation, int laneCost, int lanePopularity = 0)
        {
            _streetID = streetID;
            _laneOrientation = laneOrientation;
            _laneCost = laneCost;
            _lanePopularity = lanePopularity;
        }

        public void IncrementPopularity()
        {
            _lanePopularity++;
        }

        public object ShallowCopy()
        {
            return MemberwiseClone();
        }

        public int StreetID => _streetID;

        public bool LaneOrientation => _laneOrientation;

        public int LaneCost => _laneCost;

        public int LanePopularity => _lanePopularity;

        public void setLaneOrientation(bool value)
        {
            _laneOrientation = value;
        }
        
        public void setLaneCost(int value)
        {
            _laneCost = value;
        }
    }
}
