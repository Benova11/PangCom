namespace Game.Configs.Levels
{
    public class EndLevelResult
    {
        private readonly int _score;
        private readonly int _levelIndex;
        private readonly bool _isSuccess;
        public int Score => _score;
        public bool IsSuccess => _isSuccess;
        public int LevelIndex => _levelIndex;

        public EndLevelResult(int score, bool isSuccess, int levelIndex)
        {
            _score = score;
            _isSuccess = isSuccess;
        }
    }
}