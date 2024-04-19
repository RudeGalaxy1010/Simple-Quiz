using Source.Game;
using Source.Infrastructure;

namespace Source.Services
{
    public interface ISceneObjectsProvider : IService
    {
        public QuestionsDisplay QuestionsDisplay { get; }
        public StatisticsDisplay StatisticsDisplay { get; }
        public AnswerResultPanel AnswerResultPanel { get; }
    }
}
