using System;
using Core.UI.PlayerScore;

namespace Core.Mechanics
{
    [Serializable]
    public class ScoreControlMechanic
    {
        private PlayerScoreViewController _scoreViewController;
        
        public void Initialize()
        {
            _scoreViewController = ServiceLocator.Resolve<PlayerScoreViewController>();
        }

        public void Start()
        {
            _scoreViewController.ShowView();
        }

        public void OnTriggerEnter()
        {
            _scoreViewController.AddScore(1);
        }
        
        public void OnTriggerExit()
        {
            _scoreViewController.RemoveScore(1);
        }

        public void Stop()
        {
            _scoreViewController.ResetScore();
            _scoreViewController.HideView();
        }
    }
}