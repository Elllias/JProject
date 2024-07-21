using TMPro;
using UnityEngine;

namespace Core.UI.PlayerScore
{
    public class PlayerScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        
        public void SetScoreText(string score)
        {
            _scoreText.text = score;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}