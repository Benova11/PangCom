using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Configs.Screens;
using Game.Screens.Popups;
using Screens.Scripts.PauseMenu;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Infrastructures.Popups
{
    public class PopupManager : MonoBehaviour, IPopupManager
    {
        #region Editor

        [SerializeField] private RectTransform _parentTransform;

        #endregion

        #region Methods

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public async UniTask<EndLevelPopup> CreateEndLevelPopup(EndLevelResult endLevelResult)
        {
            var popupInstance = await Addressables.InstantiateAsync(PopupsAddressableKeys.EndLevelPopup, _parentTransform);
            popupInstance.TryGetComponent(out EndLevelPopup endLevelPopup);

            endLevelPopup.Show(endLevelResult);
            return endLevelPopup;
        }
        
        public async UniTask<PauseMenuPopup> CreatePauseMenuPopup(CurrentLevelState currentLevelState)
        {
            var popupInstance = await Addressables.InstantiateAsync(PopupsAddressableKeys.PauseMenuPopup, _parentTransform);
            popupInstance.TryGetComponent(out PauseMenuPopup endLevelPopup);

            endLevelPopup.Show(currentLevelState);
            return endLevelPopup;
        }
        
        #endregion
    }
}