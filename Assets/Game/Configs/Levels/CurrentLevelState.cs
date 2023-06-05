namespace Game.Configs.Levels
{
    public class CurrentLevelState
    {
        private readonly int _score;
        private readonly int _levelIndex;

        public int Score => _score;

        public int LevelIndex => _levelIndex;

        public CurrentLevelState(int score, int levelIndex)
        {
            _score = score;
            _levelIndex = levelIndex;
        }
    }
}