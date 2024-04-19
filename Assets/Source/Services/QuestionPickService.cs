using Source.Data;
using Source.Game;

namespace Source.Services
{
    public class QuestionPickService : IQuestionPickService
    {
        private readonly IQuestionsService _questionsService;
        private readonly IAnswersStatisticsService _answersStatisticsService;

        private int _currentQuestionIndex;
        private bool _isStopped;

        public QuestionPickService(IQuestionsService questionsService, 
            IAnswersStatisticsService answersStatisticsService)
        {
            _questionsService = questionsService;
            _answersStatisticsService = answersStatisticsService;
        }
        
        private QuestionsDisplay QuestionsDisplay => 
            Infrastructure.Services.Container.Single<ISceneObjectsProvider>().QuestionsDisplay;
        private AnswerResultPanel AnswerResultPanel =>
            Infrastructure.Services.Container.Single<ISceneObjectsProvider>().AnswerResultPanel;
        
        public void Start()
        {
            _isStopped = false;
            _currentQuestionIndex = -1;
            _answersStatisticsService.Reset();
            SetNexQuestionOrStop();
        }

        public void Stop()
        {
            _isStopped = true;
        }

        public async void SetNexQuestionOrStop()
        {
            _currentQuestionIndex++;

            if (_currentQuestionIndex >= _questionsService.Questions.Count)
            {
                _isStopped = true;
                _answersStatisticsService.DisplayStatistics();
                return;
            }
            
            Question currentQuestion = _questionsService.Questions[_currentQuestionIndex];
            Answer[] answers = await QuestionsDisplay.DisplayQuestion(currentQuestion);

            if (answers.Length > 0)
            {
                _answersStatisticsService.CollectAnswers(answers);
                await AnswerResultPanel.DisplayResult(answers, currentQuestion.answers);
            }

            if (!_isStopped)
            {
                SetNexQuestionOrStop();
            }
        }
    }
}
