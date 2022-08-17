using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Quiz_App.Annotations;
using Quiz_App.Model;

namespace Quiz_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<Model.Quiz> quizzes =
            Model.Quiz.LoadQuizzes("Quizzes.txt").ToList();

        private int currentQuizIndex = 1;

        private Random random =
            new Random();

        public Player Player { get; } = new Player();

        public MainWindow()
        {
            InitializeComponent();
            GameArea.Visibility = Visibility.Collapsed;

            var mainMenu = new MainMenu();
            mainMenu.StartQuiz += (sender, args) => { StartQuiz(); };

            SetPage(mainMenu);
        }


        public void SetPage(IPage page)
        {
            if (!(page is UserControl userControl)) return;

            page.PageChanged += (sender, newPage) => SetPage(newPage);

            if (page is QuizInstance quizInstance)
            {
                quizInstance.PointsIncrement += (sender, d) =>
                {
                    Player.CurrentPoints += d;
                    CurrentPoints.Text = $"{Player.CurrentPoints}";
                };

                quizInstance.ResultReceived += (sender, result) =>
                {
                    if (currentQuizIndex > quizzes.Count() - 1)
                    {
                        var color = Player.CurrentPoints > 0
                            ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3FB2A0"))
                            : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));
                        // player.CurrentPoints += player.CurrentPoints;

                        PointsChanged.Foreground = color;
                        QuizDone();
                        return;
                    }

                    if (result == QuizResult.Wrong)
                        Player.CurrentHealths.RemoveAt(0);

                    if (Player.CurrentHealths.Count <= 0)
                    {
                        PointsChanged.Foreground =
                            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));
                        QuizDone();
                    }

                    OnPropertyChanged(nameof(Player.CurrentHealths));
                    SetPage(new QuizInstance(quizzes[currentQuizIndex]));
                    currentQuizIndex++;
                };
            }


            if (ParentGrid.Children.Count > 0)
                ParentGrid.Children.RemoveAt(0);
            ParentGrid.Children.Add(userControl);
        }

        private void QuizDone()
        {
            Player.HighestScore =
                Player.CurrentPoints > Player.HighestScore ? Player.CurrentPoints : Player.HighestScore;
            PointsChanged.Text = $"Total {Player.CurrentPoints} Points";
            ResultDialogHost.IsOpen = true;
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
            mainMenu.HighestScoreValue = Player.HighestScore;
            mainMenu.StartQuiz += (sender, args) => { StartQuiz(); };

            SetPage(mainMenu);
        }

        private void StartQuiz()
        {
            currentQuizIndex = 1;
            GameArea.Visibility = Visibility.Visible;
            CurrentPoints.Text = $"{Player.CurrentPoints}";
            SetPage(new QuizInstance(quizzes[currentQuizIndex - 1]));
        }

        private void CloseProgram(object sender, MouseButtonEventArgs e) =>
            Application.Current.Shutdown();

        private void MinimizeProgram(object sender, MouseButtonEventArgs e) =>
            this.WindowState = System.Windows.WindowState.Minimized;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ReturnMainMenu(object sender, RoutedEventArgs e)
        {
            StartAgain(sender, e);
        }
    }
}
