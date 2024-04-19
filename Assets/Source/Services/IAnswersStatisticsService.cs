using Source.Data;
using Source.Infrastructure;

namespace Source.Services
{
    public interface IAnswersStatisticsService : IService
    {
        public void DisplayStatistics();
        public void CollectAnswers(Answer[] answer);
        public void Reset();
    }
}
