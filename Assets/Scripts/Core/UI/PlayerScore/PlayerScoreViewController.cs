namespace Core.UI.PlayerScore
{
    public class PlayerScoreViewController
    {
        private const string SCORE_PHRASE_FORMAT = "Score: {0}";
        
        private readonly PlayerScoreView _view;

        private int _currentScore;
        
        public PlayerScoreViewController(PlayerScoreView view)
        {
            _view = view;
            
            ResetScore();
        }

        public void ShowView()
        {
            _view.Show();
        }
        
        public void HideView()
        {
            _view.Hide();
        }

        public void AddScore(int score)
        {
            _currentScore += score;
            
            _view.SetScoreText(string.Format(SCORE_PHRASE_FORMAT, _currentScore));
        }

        public void RemoveScore(int score)
        {
            _currentScore -= score;
            
            _view.SetScoreText(string.Format(SCORE_PHRASE_FORMAT, _currentScore));
        }

        public void ResetScore()
        {
            _currentScore = 0;
            
            _view.SetScoreText(string.Format(SCORE_PHRASE_FORMAT, 0));
        }
    }
}