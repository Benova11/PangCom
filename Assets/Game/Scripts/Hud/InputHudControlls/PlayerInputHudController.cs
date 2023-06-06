using Game.Models;
using UnityEngine;

namespace Game.Scripts
{
    public class PlayerInputHudController : MonoBehaviour
    {
        [SerializeField] private GameManagerModel _gameManagerModel;
        [SerializeField] private GameObject[] _playerInputHudObjects;

        private void Start()
        {
            for(int i = 1; i <= _playerInputHudObjects.Length; i++)
            {
                var gameMode = (int)_gameManagerModel.GameMode;
                _playerInputHudObjects[i-1].SetActive(i <= gameMode);
            }
        }
    }
}