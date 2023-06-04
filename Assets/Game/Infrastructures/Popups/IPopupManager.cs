using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Screens.Popups;

namespace Game.Infrastructures.Popups
{
    public interface IPopupManager
    {
        UniTask<EndLevelPopup> CreateEndLevelPopup(EndLevelResult endLevelResult);
    }
}