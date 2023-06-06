using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Screens.Scripts
{
    public class LeaderboardPopupView : BasePopupPrefab<List<PlayerLeaderboardView>>
    {
        public override UniTask Show(List<PlayerLeaderboardView> playersList)
        {
            Debug.Log(playersList);
            return UniTask.CompletedTask;
        }

        public override void ClosePopup()
        {
            throw new System.NotImplementedException();
        }
    }
}