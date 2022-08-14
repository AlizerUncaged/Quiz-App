using System;
using System.Windows;
using System.Windows.Controls;
using Quiz_App.Model;

namespace Quiz_App
{
    public partial class MainMenu : UserControl, IPage
    {
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



    }
}