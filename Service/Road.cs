namespace PoliHack.Service
{
    public struct Road
    {
        private int vertex0ID;
        private int vertex1ID;
        private int roadTime;
        private int roadPopularity;
        private bool isModifiable;

        private int _roadGroupID;

        public Road(int vertex0ID, int vertex1ID, int roadTime, bool isModifiable = true, int roadGroupID = 0,
            int roadPopularity = 0)
        {
            this.vertex0ID = vertex0ID;
            this.vertex1ID = vertex1ID;
            this.roadTime = roadTime;
            this.roadPopularity = roadPopularity;
            this.isModifiable = isModifiable;
            this._roadGroupID = roadGroupID;
        }

        public int Vertex0ID => vertex0ID;

        public int Vertex1ID => vertex1ID;

        public int RoadTime => roadTime;

        public int RoadPopularity => roadPopularity;

        public int RoadGroupID => _roadGroupID;

        public bool IsModifiable => isModifiable;

        public void incrementPopularity()
        {
            this.roadPopularity++;
        }
    }
}