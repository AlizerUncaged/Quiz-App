using System;
using System.Windows;
using System.Windows.Controls;
using Quiz_App.Minigames;

namespace Quiz_App
{
    public partial class MinigameMenu : UserControl, IPage
    {
        public MinigameMenu()
        {
            InitializeComponent();
        }

        public event EventHandler<IPage> PageChanged;

        private void ReactionTime(object sender, RoutedEventArgs e) =>
            PageChanged?.Invoke(this, new ReactionTime());

        private void SequenceMemory(object sender, RoutedEventArgs e)=>
            PageChanged?.Invoke(this, new SequenceMemory());

        private void TicTacToe(object sender, RoutedEventArgs e)=>
            PageChanged?.Invoke(this, new TicTacToe());

        private void NumberMemory(object sender, RoutedEventArgs e)=>
            PageChanged?.Invoke(this, new NumberMemory());

        private void TypingText(object sender, RoutedEventArgs e)=>
            PageChanged?.Invoke(this, new TypingTest());
    }
}