using System;
using System.Windows.Controls;

namespace Quiz_App
{
    public partial class MinigameMenu : UserControl, IPage
    {
        public MinigameMenu()
        {
            InitializeComponent();
        }

        public event EventHandler<IPage> PageChanged;
    }
}