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

        public async UniTask<List<PlayerLeaderboardView>> GetLeaderboardChart()
        {
            var leaderboardChart = await GetLeaderboardChartData();
            var leaderboardPlayersViewList = PopulateLeaderboardPlayersList(leaderboardChart);
            return leaderboardPlayersViewList;
        }

        private async UniTask <List<LeaderboardPlayer>> GetLeaderboardChartData()
        {
            var leaderboardStorageSystem = new LeaderboardStorageSystem();
            var leaderboardPlayersList = await leaderboardStorageSystem.Load();

            return leaderboardPlayersList;
        }

        private List<PlayerLeaderboardView> PopulateLeaderboardPlayersList(List<LeaderboardPlayer> leaderboardChart)
        {
            return null;
        }

        public PlayerLeaderboardView PlayerLeaderboardViewPrefab => _playerLeaderboardViewPrefab;
    }
}