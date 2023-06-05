using System.Collections.Generic;
using Game.Configs.Screens.LeaderboardPopup;
using Screens.Scripts;
using UnityEngine;

namespace Models.Screens.LeaderboardPopup
{
    [CreateAssetMenu(fileName = "LeaderboardPopupModel", menuName = "Models/Screens/LeaderboardPopupModel")]
    public class LeaderboardPopupModel :ScriptableObject
    {
        [SerializeField] private PlayerLeaderboardView _playerLeaderboardViewPrefab;

        public List<PlayerLeaderboardView> GetLeaderboardChart()
        {
            var leaderboardChart = GetLeaderboardChartData();
            var leaderboardPlayersList = PopulateLeaderboardPlayersList(leaderboardChart);
            return leaderboardPlayersList;
        }
        
        private List<LeaderboardPlayer> GetLeaderboardChartData()
        {
            return null;

        }
        
        private List<PlayerLeaderboardView> PopulateLeaderboardPlayersList(List<LeaderboardPlayer> leaderboardChart)
        {
            return null;
        }
        
        public PlayerLeaderboardView PlayerLeaderboardViewPrefab => _playerLeaderboardViewPrefab;
    }
}