using Source.Infrastructure;

namespace Source.Services
{
    public interface IQuestionPickService : IService
    {
        public void Start();
        public void Stop();
        public void SetNexQuestionOrStop();
    }
}
