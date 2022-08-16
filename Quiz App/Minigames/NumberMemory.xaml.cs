using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Quiz_App.Minigames
{
    public partial class NumberMemory : UserControl, IPage
    {
        private readonly Random random = new Random();

        public NumberMemory()
        {
            InitializeComponent();
            // Enumerate borders.
            var borders = PanelLists.Children.Cast<StackPanel>()
                .Select(x => x.Children)
                .SelectMany(y => y.Cast<Border>()).ToList();
            SubmitButton.Visibility = Visibility.Collapsed;

            foreach (var border in borders)
            {
                var marker = new Label()
                {
                    Content = random.Next(0, 99).ToString(),
                    Width = 50, 
                    Height = 50,
                    FontWeight = FontWeights.Bold, 
                    FontSize = 24,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff")),
                    Margin = new Thickness(0),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                border.Tag = marker.Content;
                border.Child = marker;
            }

            _ = Task.Run(async () =>
            {
                int count = 10;
                while (true)
                {
                    if (count <= 0)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            SubmitButton.Visibility = Visibility.Visible;
                            ClickNow.Text = $"Recall.";
                            foreach (var border in borders)
                            {
                                var textBox = new TextBox()
                                {
                                    MinWidth = 50,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff")),
                                    FontWeight = FontWeights.Bold, FontSize = 16, BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE185")),
                                };
                                
                                border.Child = textBox;
                            }
                        }));
                        
                        return;
                    }

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        ClickNow.Text = $"Remember ({count}s)";
                    }));

                    await Task.Delay(1000);
                    count--;
                }
            });
        }


        private void StartAgain(object sender, RoutedEventArgs e)
        {
            PageChanged.Invoke(this, new NumberMemory());
        }

        public event EventHandler<IPage> PageChanged;

        private void UserSubmitted(object sender, RoutedEventArgs e)
        {
            var borders = PanelLists.Children.Cast<StackPanel>()
                .Select(x => x.Children)
                .SelectMany(y => y.Cast<Border>()).ToList();
            foreach (var border in borders)
            {
                if (!(border.Tag.ToString().Equals((border.Child as TextBox).Text.Trim(),
                        StringComparison.InvariantCultureIgnoreCase)))
                {
                    ResultText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));
                    ResultText.Text = "Lost!";
                    ResultDialogHost.IsOpen = true;
                    return;
                }
            }  
            ResultText.Text = "Win!";
            ResultDialogHost.IsOpen = true;
        }
    }
}