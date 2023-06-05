using System.IO;
using Game.Configs.Screens.LeaderboardPopup;
using UnityEngine;

namespace Utils.Storage
{
    public class LeaderboardStorageSystem
    {
        #region Fields
        private const string LEADERBOARD_FILE_NAME = "leaderboard_data.dat";
        
        private readonly string _targetFileName;
        private readonly string _defaultLeaderboardDataSerialized;
        private readonly LeaderboardDataSnapshot _defaultLeaderboardData = new ();

        #endregion

        #region Constructors

        public LeaderboardStorageSystem()
        {
            _targetFileName = Path.Combine(Application.persistentDataPath, LEADERBOARD_FILE_NAME);
            _targetFileName = Path.GetFullPath(_targetFileName);
            
            _defaultLeaderboardDataSerialized = JsonUtility.ToJson(_defaultLeaderboardData);
        }

        #endregion
        
        #region Methods
        
        public void Save(LeaderboardPlayer leaderboardPlayer)
        {
            var currentSnapshot = Load();
            
            currentSnapshot ??= new LeaderboardDataSnapshot();
            currentSnapshot.Players.Add(leaderboardPlayer);
            
            var serializedSnapshot = JsonUtility.ToJson(currentSnapshot);
            SaveInternal(serializedSnapshot);
        }
        
        private void SaveInternal(string serializedSnapshot)
        {
            File.WriteAllText(_targetFileName, serializedSnapshot);
        }
        
        public LeaderboardDataSnapshot Load()
        {
            var serializedSnapshot = LoadInternal();
            return JsonUtility.FromJson<LeaderboardDataSnapshot>(serializedSnapshot);
        }

        private string LoadInternal()
        {
            ValidateFileExists();
            return File.ReadAllText(_targetFileName);
        }

        private void ValidateFileExists()
        {
            if (File.Exists(_targetFileName))
            {
                return;
            }
            
            SaveInternal(_defaultLeaderboardDataSerialized);
        }

        #endregion
    }
}