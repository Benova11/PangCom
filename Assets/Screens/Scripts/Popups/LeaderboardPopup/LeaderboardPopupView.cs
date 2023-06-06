using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Infrastructures.Popups;
using UnityEngine;

namespace Screens.Scripts
{
    public class LeaderboardPopupView : BasePopupPrefab<List<PlayerLeaderboardView>>
    {
        [SerializeField] private Transform _playersListContainer;

        public override UniTask Show(List<PlayerLeaderboardView> playersList)
        {
            foreach (var playerView in playersList)
            {
                playerView.Transform.SetParent(_playersListContainer);
                playerView.Transform.localScale = Vector3.one;
            }

            return UniTask.CompletedTask;
        }

        public override void ClosePopup()
        {
            PopupManagerLocator.Unload();
        }
    }
}