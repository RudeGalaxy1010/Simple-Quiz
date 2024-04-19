using Source.Constants;
using Source.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game
{
    public class StatisticsDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject _statisticsPanel;
        [SerializeField] private TMP_Text _correctAnswersText;
        [SerializeField] private Button _menuButton;

        private IScenesService _scenesService;
        
        private void OnEnable()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void Awake()
        {
            _scenesService = Infrastructure.Services.Container.Single<IScenesService>();
        }

        public void DisplayStatistics(int correctAnswersCount)
        {
            _statisticsPanel.SetActive(true);
            _correctAnswersText.text = StringConstants.CorrectAnswersCountText
                .Replace("{0}", correctAnswersCount.ToString())
                .Replace("{1}", GetCorrectWord(correctAnswersCount));
        }

        private string GetCorrectWord(int answersCount)
        {
            int lastDigit = answersCount % 10;
            int preLastDigit = (answersCount % 100 - lastDigit) / 10;

            if (preLastDigit == 1)
            {
                return StringConstants.AnswersTextFor0;
            }
            
            if (lastDigit == 1)
            {
                return StringConstants.AnswersTextFor1;
            }
                
            if (lastDigit > 1 && lastDigit < 5)
            {
                return StringConstants.AnswersTextFor2;
            }
                
            return StringConstants.AnswersTextFor0;
        }
        
        private void OnMenuButtonClick()
        {
            _scenesService.LoadMenu();
        }
    }
}
