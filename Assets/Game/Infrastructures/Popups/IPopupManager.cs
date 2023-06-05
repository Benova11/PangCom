using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Screens.Popups;
using Screens.Scripts.PauseMenu;

namespace Game.Infrastructures.Popups
{
    public interface IPopupManager
    {
        UniTask<EndLevelPopup> CreateEndLevelPopup(EndLevelResult endLevelResult);
        UniTask<PauseMenuPopup> CreatePauseMenuPopup(CurrentLevelState currentLevelState);
    }
}