namespace Game.Configs.Levels
{
    public class LevelStatsUpdatedArgs
    {
        private readonly int _score;
        private readonly int _remainingTime;

        public int Score => _score;
        public int RemainingTime => _remainingTime;

        public LevelStatsUpdatedArgs(int score, int remainingTime)
        {
            _score = score;
            _remainingTime = remainingTime;
        }
    }
}