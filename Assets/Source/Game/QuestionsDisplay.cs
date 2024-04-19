using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Source.Data;
using Source.Extensions;
using Source.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game
{
    public class QuestionsDisplay : MonoBehaviour
    {
        private IAssetsProvider _assetsProvider;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _background;
        [SerializeField] private AnswerButton _answerButtonPrefab;
        [SerializeField] private Transform _answerButtonsContainer;
        [SerializeField] private Button _confirmButton;

        private List<AnswerButton> _answerButtons;
        private int _lastUsedAnswerButtonIndex;
        private TaskCompletionSource<Answer[]> _answerTaskCompletionSource;
        private List<Answer> _pickedAnswers;
        private Question _currentQuestion;

        private void Awake()
        {
            _assetsProvider = Infrastructure.Services.Container.Single<IAssetsProvider>();
            _answerButtons = new List<AnswerButton>();
            _lastUsedAnswerButtonIndex = -1;
        }

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(OnConfirmButtonClick);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
        }

        public Task<Answer[]> DisplayQuestion(Question question)
        {
            _currentQuestion = question;
            _pickedAnswers = new List<Answer>();
            _answerTaskCompletionSource = new TaskCompletionSource<Answer[]>();
            _text.text = _currentQuestion.question;
            _background.sprite = _assetsProvider.Load<Sprite>(_currentQuestion.background);

            for (int i = 0; i < _currentQuestion.answers.Length; i++)
            {
                AnswerButton answerButton = GetOrCreateAnswerButton();
                answerButton.SetData(_currentQuestion.answers[i]);
            }

            return _answerTaskCompletionSource.Task;
        }

        private AnswerButton GetOrCreateAnswerButton()
        {
            if (_lastUsedAnswerButtonIndex < _answerButtons.Count - 1)
            {
                AnswerButton answerButton = _answerButtons[_lastUsedAnswerButtonIndex];
                answerButton.gameObject.SetActive(true);
                _lastUsedAnswerButtonIndex++;
                return answerButton;
            }

            AnswerButton newAnswerButton = Instantiate(_answerButtonPrefab, _answerButtonsContainer);
            newAnswerButton.OnPick += OnAnswerPicked;
            _answerButtons.Add(newAnswerButton);
            _lastUsedAnswerButtonIndex++;
            return newAnswerButton;
        }

        private void OnAnswerPicked(Answer answer)
        {
            if (_pickedAnswers.Contains(answer))
            {
                _pickedAnswers.Remove(answer);
                return;
            }
            
            _pickedAnswers.Add(answer);

            if (!_currentQuestion.IsMultiAnswersQuestion())
            {
                ConfirmAnswer();
            }
        }

        private void OnConfirmButtonClick()
        {
            ConfirmAnswer();
        }

        private void ConfirmAnswer()
        {
            Reset();
            _answerTaskCompletionSource.TrySetResult(_pickedAnswers.ToArray());
        }

        private void Reset()
        {
            _text.text = string.Empty;
            
            for (int i = 0; i < _answerButtons.Count; i++)
            {
                _answerButtons[i].gameObject.SetActive(false);
            }

            _lastUsedAnswerButtonIndex = 0;
        }
    }
}
