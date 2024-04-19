using System.Linq;
using Source.Data;

namespace Source.Extensions
{
    public static class QuestionExtensions
    {
        public static bool IsMultiAnswersQuestion(this Question question)
        {
            return question.answers.Count(a => a.correct) > 1;
        }
    }
}
