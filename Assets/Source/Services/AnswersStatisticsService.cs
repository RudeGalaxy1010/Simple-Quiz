using System.Linq;
using Source.Data;
using Source.Game;

namespace Source.Services
{
    public class AnswersStatisticsService : IAnswersStatisticsService
    {
        private int _correctAnswersCount;

        private StatisticsDisplay StatisticsDisplay => 
            Infrastructure.Services.Container.Single<ISceneObjectsProvider>().StatisticsDisplay;
        
        public void DisplayStatistics()
        {
            StatisticsDisplay.DisplayStatistics(_correctAnswersCount);
        }
        
        public void CollectAnswers(Answer[] answers)
        {
            if (answers.Any(answer => !answer.correct))
            {
                return;
            }
            
            _correctAnswersCount++;
        }

        public void Reset()
        {
            _correctAnswersCount = 0;
        }
    }
}
