using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Quiz_App.Model;

namespace Quiz_App
{
    public partial class QuizInstance : UserControl, IPage
    {
        public Quiz Quiz { get; }

        private bool userResult;

        public QuizInstance(Quiz quiz)
        {
            Quiz = quiz;
            InitializeComponent();

            if (Quiz.QuestionTime > 0)
            {
                QuizTime.Maximum = Quiz.QuestionTime;
                QuizTime.Value = Quiz.QuestionTime;
                DispatcherTimer quizTimer = new DispatcherTimer();
                quizTimer.Tick += (sender, args) =>
                {
                    if (answered)
                    {
                        quizTimer.Stop();
                        return;
                    }

                    QuizTime.Value -= 1;
                    Quiz.QuestionTime -= 1;
                    if (quiz.QuestionTime <= 7)
                        QuizTime.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));

                    if (Quiz.QuestionTime <= 0)
                    {
                        userResult = false;
                        ShowResult(userResult, "Time's out!");
                        quizTimer.Stop();
                    }
                };
                quizTimer.Interval = new TimeSpan(0, 0, 1);
                quizTimer.Start();
            }
        }

        public event EventHandler<double> PointsIncrement;


        public event EventHandler<QuizResult> ResultReceived;

        public void ShowResult(bool result, string message)
        {
            var points = this.Quiz.Points * (result ? 1 : -1);
            var color = result
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3FB2A0"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));

            ResultText.Text = message;

            PointsChanged.Text = this.Quiz.Points > 0 ? $"{points} Points" : string.Empty;
            PointsIncrement?.Invoke(this, points);

            ResultText.Foreground = color;
            PointsChanged.Foreground = color;

            ResultDialogHost.IsOpen = true;
        }

        private bool answered = false;

        private void AnswerClicked(object sender, MouseButtonEventArgs e)
        {
            answered = true;
            if (!(sender is Border border)) return;

            if (!(border.Child is TextBlock textBlock && textBlock.Text is string userAnswer)) return;
            userResult = userAnswer.Equals(this.Quiz.CorrectAnswer);

            ShowResult(userResult, userResult ? "Correct!" : "Wrong...");
        }

        public event EventHandler<IPage> PageChanged;

        private void NextQuestion(object sender, RoutedEventArgs e)
        {
            ResultReceived?.Invoke(this, userResult ? QuizResult.Correct : QuizResult.Wrong);
        }
    }
}