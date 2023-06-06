using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Screens.Popups;
using Models.Screens.LeaderboardPopup;
using Screens.Scripts;
using Screens.Scripts.PauseMenu;

namespace Game.Infrastructures.Popups
{
    public interface IPopupManager
    {
        UniTask<EndLevelPopup> CreateEndLevelPopup(EndLevelResult endLevelResult);
        UniTask<PauseMenuPopup> CreatePauseMenuPopup(CurrentLevelState currentLevelState);
        UniTask<LeaderboardPopupPresenter> CreateLeaderboardPopup(LeaderboardPopupModel leaderboardPopupModel); }
}