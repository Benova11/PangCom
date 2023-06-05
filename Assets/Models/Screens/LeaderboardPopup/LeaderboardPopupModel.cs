using System.Collections.Generic;
using Screens.Scripts;
using UnityEngine;

namespace Models.Screens.LeaderboardPopup
{
    [CreateAssetMenu(fileName = "LeaderboardPopupModel", menuName = "Models/Screens/LeaderboardPopupModel")]
    public class LeaderboardPopupModel :ScriptableObject
    {
        public List<LeaderBoardPlayer> GetLeaderboardChart() //list player leaderboard view
        {
            return null;
        }
    }
}