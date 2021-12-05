using System.Collections.Generic;

namespace PoliHack.Service.LanesConfiguration
{
    public class TwoLanes : LanesConfiguration
    {
        protected override void InitConfigurations()
        {
            _lanesConfigurations = new List<List<bool>>();

            List<bool> currentLaneConfiguration = new List<bool> {true, true};
            _lanesConfigurations.Add(currentLaneConfiguration);

            currentLaneConfiguration = new List<bool> {false, false};
            _lanesConfigurations.Add(currentLaneConfiguration);

            currentLaneConfiguration = new List<bool> {true, false};
            _lanesConfigurations.Add(currentLaneConfiguration);
        }
    }
}
