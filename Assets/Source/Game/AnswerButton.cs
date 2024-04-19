using System;
using Source.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game
{
    public class AnswerButton : MonoBehaviour
    {
        public event Action<Answer> OnPick;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        [SerializeField] private Image _blackout;

        private Answer _answer;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void SetData(Answer answer)
        {
            _blackout.gameObject.SetActive(false);
            _text.text = answer.text;
            _answer = answer;
        }

        private void OnButtonClick()
        {
            _blackout.gameObject.SetActive(!_blackout.gameObject.activeSelf);
            OnPick?.Invoke(_answer);
        }
    }
}
