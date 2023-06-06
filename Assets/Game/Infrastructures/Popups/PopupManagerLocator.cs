using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Configs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Game.Infrastructures.Popups
{
    public static class PopupManagerLocator
    {
        private static IPopupManager _popupManager;

        public static async UniTask<IPopupManager> Get()
        {
            if (_popupManager != null)
            {
                return _popupManager;
            }

            await SceneManager.LoadSceneAsync(SystemSceneIndexes.POPUP_MANAGER_BUILD_ID, LoadSceneMode.Additive);
            var manager = Object.FindObjectsOfType<MonoBehaviour>().OfType<IPopupManager>().FirstOrDefault();
            _popupManager = null;
            
            return manager;
        }
    }
}