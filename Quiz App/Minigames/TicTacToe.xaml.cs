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
                .Select(y => y.Cast<Border>());

            for (int x = 0; x < 3; x++)
            {
                var top = borders.ToList()[x];
                if (top.First().Child is null) continue;

                bool isMarked = true;

                string markType = (top.First().Child as PackIcon).Kind.ToString();

                // Do horizontal.
                for (int y = 0; y < 3; y++) // X -> 2
                {
                    var bottom = top.ToList()[y];
                    if (bottom.Child is null)
                    {
                        isMarked = false;
                        break;
                    }

                    var mark = (bottom.Child as PackIcon).Kind.ToString();

                    if (!mark.Equals(markType))
                    {
                        isMarked = false;
                        break;
                    }
                }

                // Do vertical.
                if (!isMarked)
                {
                    isMarked = true;
                    string markType2 = null;
                    for (var y = 0; y < 3; y++)
                    {
                        var first = borders.ToList()[y].ToList()[x];

                        if (first.Child is null)
                        {
                            isMarked = false;
                            continue;
                        }

                        if (markType2 is null)
                            markType2 = (first.Child as PackIcon).Kind.ToString();

                        var mark = (first.Child as PackIcon).Kind.ToString();

                        if (!mark.Equals(markType))
                        {
                            isMarked = false;
                            break;
                        }
                    }
                }

                if (isMarked)
                {
                    // Found a win!
                    if (markType.Equals("Remove"))
                    {
                        ResultDialogHost.IsOpen = true;
                    }
                    else
                    {
                        ClickNow.Text = "Lose.";
                        ResultText.Text = "Lose.";
                        ClickNow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));
                        ResultDialogHost.IsOpen = true;
                    }
                }
            }
            
            // No one won yet?
            if (!PanelLists.Children.Cast<StackPanel>()
                    .Select(x => x.Children)
                    .SelectMany(y => y.Cast<Border>()).Where(x => x.Child is null).Any())
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