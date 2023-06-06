using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Screens.LeaderboardPopup;
using Screens.Scripts;
using UnityEngine;
using Utils.Storage;

namespace Models.Screens.LeaderboardPopup
{
    [CreateAssetMenu(fileName = "LeaderboardPopupModel", menuName = "Models/Screens/LeaderboardPopupModel")]
    public class LeaderboardPopupModel : ScriptableObject
    {
        [SerializeField] private PlayerLeaderboardView _playerLeaderboardViewPrefab;
        
        private List<LeaderboardPlayer> _sortedLeaderboardPlayersList;
        
        public async UniTask<List<PlayerLeaderboardView>> GetLeaderboardChart()
        {
            _sortedLeaderboardPlayersList = await GetLeaderboardChartData();
            SortLeaderboardPlayersListByRank();
            
            var leaderboardChart = _sortedLeaderboardPlayersList;
            var leaderboardPlayersViewList = PopulateLeaderboardPlayersList(leaderboardChart);
            
            return leaderboardPlayersViewList;
        }
        
        private  void SortLeaderboardPlayersListByRank()
        {
            _sortedLeaderboardPlayersList.Sort((a, b) => a.Score.CompareTo(b.Score));
            _sortedLeaderboardPlayersList.Reverse();
        }

        private async UniTask<List<LeaderboardPlayer>> GetLeaderboardChartData()
        {
            var leaderboardStorageSystem = new LeaderboardStorageSystem();
            var leaderboardPlayersList = await leaderboardStorageSystem.Load();

            return leaderboardPlayersList;
        }

        private List<PlayerLeaderboardView> PopulateLeaderboardPlayersList(List<LeaderboardPlayer> leaderboardChart)
        {
            var playerLeaderboardViewList = new List<PlayerLeaderboardView>();

            foreach (var player in leaderboardChart)
            {
                var playerView = Instantiate(_playerLeaderboardViewPrefab);
                playerLeaderboardViewList.Add(playerView);
                playerView.SetData(leaderboardChart.IndexOf(player) + 1, player.Name, player.Score);
            }

            return playerLeaderboardViewList;
        }
    }
}