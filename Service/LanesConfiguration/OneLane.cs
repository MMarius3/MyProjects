using System.Collections.Generic;

namespace PoliHack.Service.LanesConfiguration
{
    public class OneLane : LanesConfiguration
    {
        protected override void InitConfigurations()
        {
            _lanesConfigurations = new List<List<bool>>();
            
            List<bool> currentLaneConfiguration = new List<bool> {true};
            _lanesConfigurations.Add(currentLaneConfiguration);

            currentLaneConfiguration = new List<bool> {false};
            _lanesConfigurations.Add(currentLaneConfiguration);
        }
    }
}