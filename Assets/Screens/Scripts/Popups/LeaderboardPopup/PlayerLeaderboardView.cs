using TMPro;
using UnityEngine;

namespace Screens.Scripts
{
    public class PlayerLeaderboardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerLeaderBoardStat;
        
        public void SetData(int rank, string name, int score)
        {
            _playerLeaderBoardStat.text = $"{rank}. {name}   {score}";
        }
    }
}