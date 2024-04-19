using System.Linq;
using System.Threading.Tasks;
using Source.Constants;
using Source.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game
{
    public class AnswerResultPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _nextButton;

        private TaskCompletionSource<bool> _taskCompletionSource;

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(OnNextButtonClick);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(OnNextButtonClick);
        }

        public Task<bool> DisplayResult(Answer[] answers, Answer[] allAnswers)
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();
            _panel.gameObject.SetActive(true);

            bool allAnswersCorrect = answers.All(answer => answer.correct) 
                                     && answers.Length == allAnswers.Count(answer => answer.correct);
            bool allAnswersWrong = answers.All(answer => !answer.correct);

            _resultText.text = allAnswersCorrect ? StringConstants.CorrectAnswer :
                allAnswersWrong ? StringConstants.WrongAnswer : StringConstants.SemiCorrectAnswer;

            return _taskCompletionSource.Task;
        }
        
        private void OnNextButtonClick()
        {
            _panel.gameObject.SetActive(false);
            _taskCompletionSource.TrySetResult(true);
        }
    }
}
