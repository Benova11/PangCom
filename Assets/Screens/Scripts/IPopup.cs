using Cysharp.Threading.Tasks;

namespace Screens.Scripts
{
    public interface IPopup<T>
    {
        UniTask Show(T data);
    }
}