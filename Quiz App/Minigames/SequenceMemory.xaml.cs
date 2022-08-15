using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Quiz_App.Minigames
{
    public partial class SequenceMemory : UserControl, IPage
    {
        private readonly Random random = new Random();

        public int MaxMemoryLength { get; set; } = 4;

        private List<int> Sequence;

        public SequenceMemory()
        {
            InitializeComponent();
            Sequence = Enumerable.Range(0, MaxMemoryLength).Select(x => random.Next(0, 8)).ToList();

            // Enumerate borders.
            var borders = PanelLists.Children.Cast<StackPanel>()
                .Select(x => x.Children)
                .SelectMany(y => y.Cast<Border>()).ToList();

            foreach (var border in borders)
            {
                border.Tag = borders.IndexOf(border);
                border.MouseDown += (sender, args) =>
                {
                    ClickNow.Visibility = Visibility.Hidden;
                    var currentSeq = Sequence[currentIndex];
                    if (border.Tag is int borderValue && borderValue == currentSeq)
                    {
                        ClickNow.Text = "Correct.";
                        ClickNow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));

                        if (currentIndex >= Sequence.Count - 1)
                        {
                            ResultDialogHost.IsOpen = true;
                        }
                    }
                    else
                    {
                        ClickNow.Visibility = Visibility.Visible;
                        ClickNow.Text = "Wrong!";
                        ClickNow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));

                        recallDone.TrySetResult(true);
                        StartOnBackground();

                        currentMax = 0;
                    }

                    currentMax--;
                    currentIndex++;
                    
                    if (currentMax < 0)
                        recallDone.TrySetResult(true);

                    args.Handled = true;
                };
            }
        }

        public Border GetBorderFromIndex(int index)
        {
            var borders = PanelLists.Children.Cast<StackPanel>()
                .Select(x => x.Children)
                .SelectMany(y => y.Cast<Border>()).ToList();

            return borders[index];
        }

        private void SetBordersClickable(bool isEnabled) =>
            PanelLists.Children.Cast<StackPanel>()
                .Select(x => x.Children)
                .SelectMany(y => y.Cast<Border>()).Select(x => x.IsEnabled = isEnabled);


        private Stopwatch playerResponseMeasurer =
            new Stopwatch();

        public TaskCompletionSource<bool> recallDone =
            new TaskCompletionSource<bool>();

        private int currentIndex = 0;
        private int currentMax = 0;

        public void StartOnBackground() =>
            Task.Run(async () =>
            {
                for (var i = 0; i < MaxMemoryLength; i++)
                {
                    currentMax = i;

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ClickNow.Visibility = Visibility.Visible;
                    }));

                    await Task.Delay(1000);
                    for (var k = 0; k <= i; k++)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            SetBordersClickable(false);
                            ClickNow.Visibility = Visibility.Hidden;
                            var sequence = Sequence[k];

                            var border = GetBorderFromIndex(sequence);

                            // Yellow first.
                            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE185"));

                            var animation = new ColorAnimation()
                            {
                                To = (Color)ColorConverter.ConvertFromString("#42B4A5"),
                                Duration = new Duration(TimeSpan.FromMilliseconds(300))
                            };
                            border.Background.BeginAnimation(SolidColorBrush.ColorProperty,
                                animation);
                        }));

                        await Task.Delay(400);
                    }

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ClickNow.Text = "Recall.";
                        ClickNow.Visibility = Visibility.Visible;
                        SetBordersClickable(true);
                    }));

                    await recallDone.Task;
                    recallDone = new TaskCompletionSource<bool>();
                    currentIndex = 0;
                    currentMax = i + 1;
                }
            });


        public event EventHandler<IPage> PageChanged;

        private void Rendered(object sender, RoutedEventArgs e)
        {
            ClickNow.Text = "Recall.";
            ClickNow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            ClickNow.Visibility = Visibility.Hidden;
            StartOnBackground();
        }

        private void StartAgain(object sender, RoutedEventArgs e)
        {
            PageChanged.Invoke(this, new SequenceMemory());
        }
    }
}