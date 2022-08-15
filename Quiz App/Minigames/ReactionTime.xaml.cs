using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Quiz_App.Minigames
{
    public partial class ReactionTime : UserControl, IPage
    {
        private readonly Random random = new Random();

        public ReactionTime()
        {
            InitializeComponent();
        }

        public event EventHandler<IPage> PageChanged;

        public bool IsRunning { get; set; } = true;

        private bool waitingForClick = false;

        public TaskCompletionSource<bool> userClicked =
            new TaskCompletionSource<bool>();

        private Stopwatch playerResponseMeasurer =
            new Stopwatch();

        public void StartOnBackground() =>
            Task.Run(async () =>
            {
                while (IsRunning)
                {
                    Application.Current.Dispatcher.Invoke(
                        new Action(() =>
                        {
                            GameParent.Background =
                                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#42B4A5"));
                            ClickNow.Visibility = Visibility.Collapsed;
                            StartAgainButton.Visibility = Visibility.Collapsed;
                            PlayerResponseTime.Visibility = Visibility.Collapsed;
                            Instructions.Visibility = Visibility.Visible;
                        })
                    );

                    await Task.Delay(random.Next(2000, 4000));

                    Application.Current.Dispatcher.Invoke(
                        new Action(() =>
                        {
                            ClickNow.Visibility = Visibility.Visible;
                            Instructions.Visibility = Visibility.Collapsed;
                            GameParent.Background =
                                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE185"));
                        })
                    );

                    waitingForClick = true;
                    playerResponseMeasurer.Start();
                    await userClicked.Task;

                    // Reset the click awaiter.
                    userClicked = new TaskCompletionSource<bool>();
                }
            });

        private void Rendered(object sender, RoutedEventArgs e)
        {
            StartOnBackground();
        }

        private void GameClicked(object sender, MouseButtonEventArgs e)
        {
            if (!waitingForClick) return;

            playerResponseMeasurer.Stop();

            GameParent.Background =
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#42B4A5"));
            PlayerResponseTime.Text = $"That took you {playerResponseMeasurer.ElapsedMilliseconds}ms!";
            PlayerResponseTime.Visibility = Visibility.Visible;
            StartAgainButton.Visibility = Visibility.Visible;
        }

        private void UserStart(object sender, RoutedEventArgs e)
        {
            userClicked.TrySetResult(true);
        }
    }
}