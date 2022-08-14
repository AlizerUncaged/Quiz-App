using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Quiz_App.Model;

namespace Quiz_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<Model.Quiz> quizzes =
            Model.Quiz.LoadQuizzes("Quizzes.json").ToList();

        private int currentQuizIndex = 1;

        private Random random =
            new Random();

        public MainWindow()
        {
            InitializeComponent();
            GameArea.Visibility = Visibility.Collapsed;

            var mainMenu = new MainMenu();
            mainMenu.StartQuiz += (sender, args) =>
            {
                GameArea.Visibility = Visibility.Visible;
                SetPage(new QuizInstance(quizzes[currentQuizIndex - 1]));
            };
            
            SetPage(mainMenu);
        }

        private double currentPoints = 100;

        public void SetPage(IPage page)
        {
            if (!(page is UserControl userControl)) return;

            page.PageChanged += (sender, newPage) => SetPage(newPage);

            if (page is QuizInstance quizInstance)
            {
                quizInstance.PointsIncrement += (sender, d) =>
                {
                    currentPoints += d;
                    CurrentPoints.Text = $"{currentPoints}";
                };

                quizInstance.ResultReceived += (sender, result) =>
                {
                    SetPage(new QuizInstance(quizzes[currentQuizIndex]));
                    currentQuizIndex++;
                };
            }


            if (ParentGrid.Children.Count > 0)
                ParentGrid.Children.RemoveAt(0);
            ParentGrid.Children.Add(userControl);
        }

        private void MainWindowClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
    }
}