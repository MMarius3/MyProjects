using System.Collections.Generic;
using System.Linq;

namespace PoliHack.Service.LanesConfiguration
{
    public abstract class LanesConfiguration
    {
        protected List<List<bool>> _lanesConfigurations = null;
        protected abstract void InitConfigurations();

        public List<List<bool>> GetLanesConfigurations()
        {
            if (_lanesConfigurations == null)
            {
                InitConfigurations();
            }

            return _lanesConfigurations;
        }

        public RoadSystemConfiguration ApplyNewConfigurationToRoadSystem(
            RoadSystemConfiguration roadSystemConfiguration, int streetID, int configurationID)
        {
            RoadSystemConfiguration newRoadSystemConfiguration =
                (RoadSystemConfiguration) roadSystemConfiguration.ShallowCopy();

            for (int i = 0; i < _lanesConfigurations[configurationID].Count; i++)
            {
                newRoadSystemConfiguration.CurrentRoadSystemConfiguration.ElementAt(streetID).LanesListOnStreet
                    .ElementAt(i).setLaneOrientation(_lanesConfigurations[configurationID][i]);
            }

            return newRoadSystemConfiguration;
        }
    }
}