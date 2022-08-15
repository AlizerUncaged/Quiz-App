using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Quiz_App.Annotations;
using Quiz_App.Model;

namespace Quiz_App
{
    public partial class MainMenu : UserControl, IPage, INotifyPropertyChanged
    {
        private double _highestScoreValue = 0;

        public MainMenu()
        {
            InitializeComponent();
        }

        public event EventHandler StartQuiz;
        public event EventHandler<IPage> PageChanged;

        private void QuizStart(object sender, RoutedEventArgs e)
        {
            StartQuiz?.Invoke(this, EventArgs.Empty);
        }


        private void CloseProgram(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();

        public double HighestScoreValue
        {
            get => _highestScoreValue;
            set
            {
                _highestScoreValue = value;
                OnPropertyChanged(nameof(HighestScoreValue));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MinigameStart(object sender, RoutedEventArgs e) => PageChanged.Invoke(this, new MinigameMenu());
    }
}