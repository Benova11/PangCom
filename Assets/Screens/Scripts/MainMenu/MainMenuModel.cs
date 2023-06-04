using Game.Models;
using UnityEngine;

namespace Screens.Scripts.MainMenu
{
    [CreateAssetMenu(fileName = "MainMenuModel", menuName = "Models/Screens/MainMenuModel")]
    public class MainMenuModel : ScriptableObject
    {
        [SerializeField] private GameConfigModel _gameConfigModel;
    }
}