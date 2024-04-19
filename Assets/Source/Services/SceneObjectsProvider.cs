using Source.Game;
using UnityEngine;

namespace Source.Services
{
    public class SceneObjectsProvider : MonoBehaviour, ISceneObjectsProvider
    {
        [SerializeField] private QuestionsDisplay _questionsDisplay;
        [SerializeField] private StatisticsDisplay _statisticsDisplay;
        [SerializeField] private AnswerResultPanel answerResultPanel;
        
        public QuestionsDisplay QuestionsDisplay => _questionsDisplay;
        public StatisticsDisplay StatisticsDisplay => _statisticsDisplay;
        public AnswerResultPanel AnswerResultPanel => answerResultPanel;
    }
}
