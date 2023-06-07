using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Game.Configs.Screens.LeaderboardPopup;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils.Storage
{
    public class LeaderboardStorageSystem
    {
        #region Fields

        private const string LEADERBOARD_FILE_NAME = "leaderboard_data.txt";

        private readonly string _targetFilePath;
        private readonly string _defaultDataSerialized;
        private List<LeaderboardPlayer> _players = new();

        #endregion

        #region Constructors

        public LeaderboardStorageSystem()
        {
            _targetFilePath = Path.Combine(Application.persistentDataPath, LEADERBOARD_FILE_NAME);
            _defaultDataSerialized = JsonConvert.SerializeObject(_players);
        }

        #endregion

        #region Methods

        public async UniTask Save(LeaderboardPlayer leaderboardPlayer)
        {
            var leaderboardPlayers = await Load();
            leaderboardPlayers ??= new();
            leaderboardPlayers.Add(leaderboardPlayer);

            var serializedSnapshot = JsonConvert.SerializeObject(leaderboardPlayers);
            SaveInternal(serializedSnapshot);
        }

        private void SaveInternal(string serializedSnapshot)
        {
            File.WriteAllText(_targetFilePath, serializedSnapshot);
        }

        public async UniTask<List<LeaderboardPlayer>> Load()
        {
            ValidateFileExists();
#if UNITY_ANDROID && !UNITY_EDITOR
            await LeaderboardFileFromAndroid(_targetFilePath);
#else
            await LoadChatFileFromIOS(_targetFilePath);
#endif
            return _players;
        }

        private async UniTask LeaderboardFileFromAndroid(string filePath)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(filePath);

            await uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.ConnectionError)
            {
                throw new UnityException(uwr.error);
            }

            _players = JsonConvert.DeserializeObject<List<LeaderboardPlayer>>(uwr.downloadHandler.text);
        }

        private async UniTask LoadChatFileFromIOS(string filePath)
        {
            string chatConfigFile = await File.ReadAllTextAsync(Path.Combine(filePath));

            _players = JsonConvert.DeserializeObject<List<LeaderboardPlayer>>(chatConfigFile);
        }

        private void ValidateFileExists()
        {
            if (File.Exists(_targetFilePath))
            {
                return;
            }

            SaveInternal(_defaultDataSerialized);
        }

        #endregion
    }
}