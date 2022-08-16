using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Quiz_App.Minigames
{
    public partial class TypingTest : UserControl, IPage
    {
        private string text =
            File.ReadAllText("TypeText.txt");

        public TypingTest()
        {
            InitializeComponent();

            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }

            _ = Task.Run(async () =>
            {
                while (IsRunning)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { WordsPerMinute.Text = $"WPM: {words}"; }));
                    await Task.Delay(1000 * 60);
                }
            });
        }

        public bool IsRunning { get; set; } = true;

        public event EventHandler<IPage> PageChanged;

        private char[] excludedCharacters = new[] { '\n', '\r' };

        private void RenderText(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).KeyDown += KeyboardPressed;
            var startingSentence = text.Substring(0, maxLetters);
            foreach (var character in startingSentence)
            {
                TypeContent.Inlines.Add(new Run(character.ToString()));
            }
        }

        private void RotateSentence()
        {
            var startingSentence = text.Substring(currentLetter + 1 + peek, currentLetter + maxLetters);
            foreach (var character in startingSentence)
            {
                TypeContent.Inlines.Add(new Run(character.ToString()));
            }

            TextView.ScrollToBottom();
        }

        private int maxLetters = 25;
        private int peek = 5;
        private int mistakes = 0;
        private int currentLetter = 0;
        private int words = 0;
        private bool justSpaced = false;

        private void KeyboardPressed(object sender, KeyEventArgs e)
        {
            var userCharacter = GetCharFromKey(e.Key);
            var inlines = TypeContent.Inlines.ToList();
            var currentInline = inlines[currentLetter] as Run;

            if (TypeContent.Inlines.LastInline == inlines[currentLetter + peek])
            {
                RotateSentence();
            }

            var currentCharacter = new TextRange(currentInline.ContentStart, currentInline.ContentEnd).Text;

            if (currentCharacter == " ")
            {
                currentInline.Text = "_";
                WordsPerMinute.Text = $"WPM: {words}";
                if (!justSpaced)
                    words++;

                justSpaced = true;
            }
            else
                justSpaced = false;

            if (currentCharacter.Equals(userCharacter.ToString()))
            {
                currentInline.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff"));
            }
            else
            {
                mistakes++;
                MistakesCounter.Text = $"Mistakes: {mistakes}";
                currentInline.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E94F4F"));
            }

            currentLetter++;
        }

        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }

        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)]
            StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

        public static char GetCharFromKey(Key key)
        {
            char ch = ' ';

            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            byte[] keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
            StringBuilder stringBuilder = new StringBuilder(2);

            int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                {
                    ch = stringBuilder[0];
                    break;
                }
                default:
                {
                    ch = stringBuilder[0];
                    break;
                }
            }

            return ch;
        }

        private void StartAgain(object sender, RoutedEventArgs e)
        {
            IsRunning = false;
            PageChanged.Invoke(this, new TypingTest());
        }
    }
}