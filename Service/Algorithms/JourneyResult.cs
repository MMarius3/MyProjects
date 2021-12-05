using System.Collections.Generic;

namespace PoliHack.Service.Algorithms
{
    public class JourneyResult
    {
        private int _startingVertex;
        private int _endVertex;
        private bool _journeyIsPossible;
        private List<int> _pathsList;
        private int _journeyValue;

        public JourneyResult(List<int> pathsList, int journeyValue, int startingVertex, int endVertex, bool journeyIsPossible)
        {
            _pathsList = pathsList;
            _journeyValue = journeyValue;
            _startingVertex = startingVertex;
            _endVertex = endVertex;
            _journeyIsPossible = journeyIsPossible;
        }

        public List<int> PathsList => _pathsList;

        public int JourneyValue => _journeyValue;

        public int StartingVertex => _startingVertex;

        public int EndVertex => _endVertex;

        public bool JourneyIsPossible => _journeyIsPossible;
    }
}
