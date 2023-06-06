using Models.Screens.LeaderboardPopup;

namespace Screens.Scripts
{
    public class LeaderboardPopupPresenter
    {
        private readonly LeaderboardPopupView _view;
        private readonly LeaderboardPopupModel _model;

        public LeaderboardPopupPresenter(LeaderboardPopupView view, LeaderboardPopupModel model)
        {
            _view = view;
            _model = model;

            ShowPopup();
        }

        private async void ShowPopup()
        {
            _view.Show(await _model.GetLeaderboardChart());
        }
    }
}