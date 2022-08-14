using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Quiz_App.Model
{
    public class Quiz
    {
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
            var quizTest = new Quiz()
            {
                QuestionSentence = "Two angles are complementary, therefore the sum of their measures is",
                Answers = new[] { "-90°", "-10°", "-180°", "-360°" },
                CorrectAnswer = "-90°",
            };

            if (!File.Exists(path))
                File.WriteAllText(path, JsonConvert.SerializeObject(new[] { quizTest }, Formatting.Indented));


            return JsonConvert.DeserializeObject<IEnumerable<Quiz>>(File.ReadAllText(path));
        }
    }
}