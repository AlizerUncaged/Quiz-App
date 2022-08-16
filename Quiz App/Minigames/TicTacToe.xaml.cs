using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace Quiz_App.Minigames
{
    public partial class TicTacToe : UserControl, IPage
    {
        private readonly Random random = new Random();

        public TicTacToe()
        {
            InitializeComponent();

            // Enumerate borders.
            var borders = PanelLists.Children.Cast<StackPanel>()
                .Select(x => x.Children)
                .SelectMany(y => y.Cast<Border>()).ToList();

            foreach (var border in borders)
            {
                var marker = new PackIcon()
                {
                    Kind = PackIconKind.Close, Width = 50, Height = 50,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                border.MouseDown += (sender, args) =>
                {
                    args.Handled = true;
                    if (!(border.Child is null))
                        return;


                    border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE185"));

                    border.Child = marker;
                    border.UpdateLayout();

                    ComputerTurn();

                    if (IsGameOver())
                    {
                    }
                };
            }
        }

        private bool IsGameOver()
        {
            var borders = PanelLists.Children.Cast<StackPanel>()
                .Select(x => x.Children)
                .SelectMany(y => y.Cast<Border>()).ToList()
                .Select(x => x.Child is null ? "" : (x.Child as PackIcon).Kind.ToString())
                .ToList();
            /*
             * 0 1 2
             * 3 4 5
             * 6 7 8
             */

            // Boi.
            string answer = "?";
            if (borders[0] == borders[1] && borders[1] == borders[2])
            {
                answer = borders[0];
            }

            if (borders[3] == borders[4] && borders[4] == borders[5])
            {
                answer = borders[3];
            }

            if (borders[6] == borders[7] && borders[7] == borders[8])
            {
                answer = borders[6];
            }

            if (borders[0] == borders[3] && borders[3] == borders[6])
            {
                answer = borders[0];
            }

            if (borders[1] == borders[4] && borders[4] == borders[7])
            {
                answer = borders[1];
            }

            if (borders[2] == borders[5] && borders[5] == borders[8])
            {
                answer = borders[2];
            }

            if (borders[0] == borders[4] && borders[4] == borders[8])
            {
                answer = borders[0];
            }

            if (borders[2] == borders[4] && borders[4] == borders[6])
            {
                answer = borders[2];
            }


            if (answer != "?")
            {
                if (answer == "Remove")
                {
                    ResultDialogHost.IsOpen = true;
                }
                else if (!string.IsNullOrWhiteSpace(answer))
                {
                    ClickNow.Text = "Lose.";
                    ResultText.Text = "Lose.";
                    ClickNow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));
                    ResultDialogHost.IsOpen = true;
                }
            }

            // No one won yet?
            if (!PanelLists.Children
                    .Cast<StackPanel>()
                    .Select(x => x.Children).SelectMany(y => y.Cast<Border>()).Any(x => x.Child is null))
            {
                ClickNow.Text = "Draw.";
                ResultText.Text = "Draw.";
                ClickNow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff"));
                ResultDialogHost.IsOpen = true;
            }

            return false;
        }

        private void ComputerTurn()
        {
            var borders = PanelLists.Children.Cast<StackPanel>()
                .Select(x => x.Children)
                .SelectMany(y => y.Cast<Border>()).Where(x => x.Child is null).ToList();

            if (!borders.Any()) return;

            var marker = new PackIcon()
            {
                Kind = PackIconKind.CircleOutline, Width = 50, Height = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var chosenBorder = borders[random.Next(borders.Count)];
            chosenBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));
            chosenBorder.Child = marker;
        }

/*
 * <materialDesign:PackIcon Kind="Close" HorizontalAlignment="Center" VerticalAlignment="Center" Width="50" Height="50"></materialDesign:PackIcon>
 */
        private void StartAgain(object sender, RoutedEventArgs e)
        {
            PageChanged.Invoke(this, new TicTacToe());
        }

        public event EventHandler<IPage> PageChanged;
    }
}