using Cysharp.Threading.Tasks;
using Game.Infrastructures.Popups;
using Models.Screens.LeaderboardPopup;

namespace Game.Scripts.Flows
{
    public class ShowLeaderboardPopupFlow
    {
        private readonly LeaderboardPopupModel _leaderboardPopupModel;
        
        public ShowLeaderboardPopupFlow(LeaderboardPopupModel leaderboardPopupModel)
        {
            _leaderboardPopupModel = leaderboardPopupModel;
        }
        
        public async UniTask Execute()
        {
            var popupManager = await PopupManagerLocator.Get();
            await popupManager.CreateLeaderboardPopup(_leaderboardPopupModel);
        }
    }
}