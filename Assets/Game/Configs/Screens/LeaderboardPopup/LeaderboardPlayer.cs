namespace Game.Configs.Screens.LeaderboardPopup
{
    public class LeaderboardPlayer
    {
        public LeaderboardPlayer(int score, string name)
        {
            Score = score;
            Name = name;
        }

        public int Score { get; }
        public string Name { get; }
        public int Rank { get; set; }
    }
}