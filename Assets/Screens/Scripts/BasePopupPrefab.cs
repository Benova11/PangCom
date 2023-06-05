using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Screens.Scripts
{
    public abstract class BasePopupPrefab<T> : MonoBehaviour, IPopup<T>
    {
        public abstract UniTask Show(T data);

        public abstract void ClosePopup();
    }
}