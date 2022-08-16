using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Quiz_App.Model
{
    public class Quiz
    {
        private static readonly Random random = new Random();
        public int Points { get; set; } = 20;

        public string QuestionSentence { get; set; }

        public string CorrectAnswer { get; set; }

        public IEnumerable<string> Answers { get; set; }

        /// <summary>
        /// The length of time the player has to answer the question; if less than zero, it signifies limitless.
        /// </summary>
        public double QuestionTime { get; set; } = -1d;

        public static IEnumerable<Quiz> LoadQuizzes(string path)
        {
            var fileContent = File.ReadAllText(path);
            var quizQuestions = fileContent.Split(new string[] { "\r\n\r\n" },
                StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

            List<Quiz> quizzes = new List<Quiz>();
            foreach (var rawQuestions in quizQuestions)
            {
                var splitted = rawQuestions.Split('\n');

                var question = splitted[0]
                    .Split(new string[] { "." }, 2, StringSplitOptions.None)
                    .Last()
                    .Trim();

                var answer = splitted[1]
                    .Split(new string[] { "." }, 2, StringSplitOptions.None)
                    .Last()
                    .Trim()
                    .ToLower();

                var answers = splitted[2]
                    .Split(new string[] { ".", "  " }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(x => x.Count() > 1)
                    .Select(c => c.Trim());

                Quiz quiz = new Quiz()
                {
                    QuestionTime = 20,
                    QuestionSentence = question,
                    CorrectAnswer = answer,
                    Answers = answers
                };

                quizzes.Add(quiz);
            }

            return quizzes.OrderBy(x => random.Next());
        }
    }
}