using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Configs.Screens;
using Game.Screens.Popups;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Infrastructures.Popups
{
    public class PopupManager : MonoBehaviour, IPopupManager
    {
        #region Editor

        [SerializeField]
        private RectTransform _parentTransform;

        // [SerializeField]
        // private EndLevelPopup _enterLevelPopupPrefabRef;
		
        #endregion

        #region Methods

        public async UniTask<EndLevelPopup> CreateEndLevelPopup(EndLevelResult endLevelResult)
        {
            var popupInstance = await Addressables.InstantiateAsync(PopupsAddressableKeys.EndLevelPopup, _parentTransform);
            popupInstance.TryGetComponent(out EndLevelPopup endLevelPopup);

            endLevelPopup.Show(endLevelResult);
            return endLevelPopup;
        }

        #endregion
    }
}