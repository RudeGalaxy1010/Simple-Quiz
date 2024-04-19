using System.Collections.Generic;
using Source.Data;
using Source.Infrastructure;

namespace Source.Services
{
    public interface IQuestionsService : IService
    {
        public IReadOnlyList<Question> Questions { get; }
    }
}
