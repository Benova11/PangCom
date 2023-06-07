using System.Collections.Generic;
using Game.Configs.Screens.LeaderboardPopup;
using UnityEngine;

namespace Models.Screens.LeaderboardPopup
{
    [CreateAssetMenu(fileName = "LeaderboardChartModel", menuName = "Models/Screens/LeaderboardCharModel")]
    public class LeaderboardChartModel : ScriptableObject
    {
        public List<LeaderboardPlayer> LeaderboardChart;
    }
}