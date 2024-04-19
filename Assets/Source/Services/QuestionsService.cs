using System;
using System.Collections.Generic;
using System.Linq;
using Source.Constants;
using Source.Data;
using UnityEngine;

namespace Source.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IReadOnlyList<Question> _questions;

        public QuestionsService(IAssetsProvider assetsProvider)
        {
            TextAsset questionsAsset = assetsProvider.Load<TextAsset>(AssetsConstants.QuestionsJsonPath);
            string questionsJson = questionsAsset.ToString();

            if (string.IsNullOrEmpty(questionsJson))
            {
                throw new Exception("Failed to read questions");
            }

            _questions = ParseQuestions(questionsJson);
        }

        public IReadOnlyList<Question> Questions => _questions;

        private IReadOnlyList<Question> ParseQuestions(string questionsJson)
        {
            QuestionsList questionsList = JsonUtility.FromJson<QuestionsList>(questionsJson);

            if (questionsList.questions == null)
            {
                throw new Exception("Failed to parse JSON");
            }

            return questionsList.questions.ToList();
        }
    }
}
