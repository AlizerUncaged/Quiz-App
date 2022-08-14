using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
                StartQuiz();
            };
            
            SetPage(mainMenu);
        }

        // the points of the player
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
                    if (currentQuizIndex > quizzes.Count - 1)
                    {
                        var color = currentPoints > 0
                            ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3FB2A0"))
                            : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));
                        currentPoints += currentPoints;
                    
                        PointsChanged.Foreground = color;
                        PointsChanged.Text = $"Total {currentPoints} Points";
                        ResultDialogHost.IsOpen = true;
                        return;
                    }

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

        private void StartAgain(object s, RoutedEventArgs e)
        {
            GameArea.Visibility = Visibility.Collapsed;
            ResultDialogHost.IsOpen = false;
            var mainMenu = new MainMenu();
            mainMenu.StartQuiz += (sender, args) =>
            {
                StartQuiz();
            };
            
            SetPage(mainMenu);
        }

        private void StartQuiz()
        {
            currentQuizIndex = 1;
            GameArea.Visibility = Visibility.Visible;
            CurrentPoints.Text = $"{currentPoints}";
            SetPage(new QuizInstance(quizzes[currentQuizIndex - 1]));
        }

        private void CloseProgram(object sender, MouseButtonEventArgs e) =>
            Application.Current.Shutdown();

        private void MinimizeProgram(object sender, MouseButtonEventArgs e) =>
            this.WindowState = System.Windows.WindowState.Minimized;
    }
}